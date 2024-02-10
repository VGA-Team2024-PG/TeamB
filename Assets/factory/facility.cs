using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using factory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

[Serializable]
public class CurrentFacilityData
{
	/// <summary>施設のレベル</summary>
	public int facilityLevel;

	/// <summary>リソースの増える量</summary>
	public float resourceValue;

	/// <summary>施設の価格</summary>
	public float prime;

	/// <summary>初期化用</summary>
	/// <param name="resourceValue"></param>
	/// <param name="Prime"></param>
	/// <param name="level"></param>
	public CurrentFacilityData(float resourceValue, float Prime, int level = 1)
	{
		facilityLevel = level;
		this.resourceValue = resourceValue;
		prime = Prime;
	}
}

/// <summary>施設のクラス</summary>
public class Facility : MonoBehaviour
{
	private static Facility _instance;
	public static Facility Instance => _instance;
	//全施設のデータ
	[SerializeField] private BaseFacilityData facilityDatas;
	Dictionary<string, FacilityData> _facilityDatasDic = new ();
	public Dictionary<string, FacilityData> FacilityDatasDic => _facilityDatasDic;

	//買った施設
	private Dictionary<string, CurrentFacilityData> buyedFacilities = new();
	public Dictionary<string, CurrentFacilityData> BuyedFacilities => buyedFacilities;

	float resourcePerSeconds = 0.0f;

	
	private void Awake()
	{
		_instance = this;
	}

	
	private void Start()
	{
		ResourceManager.Instance.AddResorce(200);
		facilityDatas.FacilityDatas.ForEach(x => _facilityDatasDic.Add(x.FacilityName, x));
	}

	
	private void Update()
	{
		AddResourceOpe();
	}

	/// <summary>
	/// 施設を追加する。あったらレベルを上げる（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void AddFacilities(string name)
	{
		if (buyedFacilities == null || buyedFacilities.ContainsKey(name) == false)
		{
				//リソースが足りなかったら買えない
				if (ResourceManager.Instance.Resorce >= (ulong)Mathf.Ceil(_facilityDatasDic[name].Prime))
				{
					ResourceManager.Instance.UseResorce(_facilityDatasDic[name].Prime);
					buyedFacilities.Add(name, new CurrentFacilityData(_facilityDatasDic[name].MoneyPerSecond, _facilityDatasDic[name].Prime));
					buyedFacilities.OrderBy(x => x.Value.prime);
					buyedFacilities[name].prime *= 1.15f;
					UIManager.Instance.ReflectShop(name);
				}
		} //なかったらリストに追加してテキストを自分の子オブジェクトに出す
		else if (ResourceManager.Instance.Resorce >= (ulong)Mathf.CeilToInt(buyedFacilities[name].prime))
		{
			ResourceManager.Instance.UseResorce((int)Mathf.Ceil(buyedFacilities[name].prime));
			++buyedFacilities[name].facilityLevel;
			buyedFacilities[name].prime *= 1.15f;
			UIManager.Instance.ReflectShop(name);
		} //施設があった場合施設レベルと増えるクッキーの値を増やす
	}

	/// <summary>
	/// アップデートアイテム（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void UpdateItem(string name)
	{
		if (buyedFacilities.ContainsKey(name) && ResourceManager.Instance.Resorce >= buyedFacilities[name].prime)
		{
			ResourceManager.Instance.UseResorce((int)Mathf.Ceil(buyedFacilities[name].prime));
			buyedFacilities[name].resourceValue *= 2;
		}
	}


	///<summary>１フレームごとのリソース量</summary>
	void AddResourceOpe()
	{
		float resourcePerSeconds = 0.0f;
		foreach (var factory in buyedFacilities.Values)
		{
			for (int i = 0; i < factory.facilityLevel; i++)
			{
				resourcePerSeconds += factory.resourceValue;
			}
		}
		ResourceManager.Instance.AddResorce((int)(resourcePerSeconds * Time.deltaTime));
	}
}