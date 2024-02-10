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
public class CurrentFactoryData
{
	/// <summary>施設のレベル</summary>
	public int factoryLevel;

	/// <summary>リソースの増える量</summary>
	public float resourceValue;

	/// <summary>施設の価格</summary>
	public float prime;

	/// <summary>初期化用</summary>
	/// <param name="resourceValue"></param>
	/// <param name="Prime"></param>
	/// <param name="level"></param>
	public CurrentFactoryData(float resourceValue, float Prime, int level = 1)
	{
		factoryLevel = level;
		this.resourceValue = resourceValue;
		prime = Prime;
	}
}

/// <summary>施設のクラス</summary>
public class Facility : MonoBehaviour
{
	private static Facility instance;
	public static Facility Instance => instance;
	//全施設のデータ
	[SerializeField] private BaseFactoryData _factoryDatas;
	Dictionary<string, FactoryData> _factoryDatasDic = new ();
	public Dictionary<string, FactoryData> FactoryDatasDic => _factoryDatasDic;

	//買った施設
	private Dictionary<string, CurrentFactoryData> buyedFactories = new();
	public Dictionary<string, CurrentFactoryData> BuyedFactories => buyedFactories;

	float resourcePerSeconds = 0.0f;

	
	private void Awake()
	{
		instance = this;
	}

	
	private void Start()
	{
		ResourceManager.Instance.AddResorce(200);
		_factoryDatas.FactoryDatas.ForEach(x => _factoryDatasDic.Add(x.FactoryName, x));
	}

	
	private void Update()
	{
		AddCookieOpe();
	}

	/// <summary>
	/// 施設を追加する。あったらレベルを上げる（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void AddFactories(string name)
	{
		if (buyedFactories == null || buyedFactories.ContainsKey(name) == false)
		{
				//リソースが足りなかったら買えない
				if (ResourceManager.Instance.Resorce >= (ulong)Mathf.Ceil(_factoryDatasDic[name].Prime))
				{
					ResourceManager.Instance.UseResorce(_factoryDatasDic[name].Prime);
					buyedFactories.Add(name, new CurrentFactoryData(_factoryDatasDic[name].MoneyPerSecond, _factoryDatasDic[name].Prime));
					buyedFactories.OrderBy(x => x.Value.prime);
					buyedFactories[name].prime *= 1.15f;
					UIManager.Instance.ReflectShop(name);
				}
		} //なかったらリストに追加してテキストを自分の子オブジェクトに出す
		else if (ResourceManager.Instance.Resorce >= (ulong)Mathf.CeilToInt(buyedFactories[name].prime))
		{
			ResourceManager.Instance.UseResorce((int)Mathf.Ceil(buyedFactories[name].prime));
			++buyedFactories[name].factoryLevel;
			buyedFactories[name].prime *= 1.15f;
			UIManager.Instance.ReflectShop(name);
		} //施設があった場合施設レベルと増えるクッキーの値を増やす
	}

	/// <summary>
	/// アップデートアイテム（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void UpdateItem(string name)
	{
		if (buyedFactories.ContainsKey(name) && ResourceManager.Instance.Resorce >= buyedFactories[name].prime)
		{
			ResourceManager.Instance.UseResorce((int)Mathf.Ceil(buyedFactories[name].prime));
			buyedFactories[name].resourceValue *= 2;
		}
	}

	

	void AddCookieOpe()
	{
		float resourcePerSeconds = 0.0f;
		foreach (var factory in buyedFactories.Values)
		{
			for (int i = 0; i < factory.factoryLevel; i++)
			{
				resourcePerSeconds += factory.resourceValue;
			}
		}
		ResourceManager.Instance.AddResorce((int)(resourcePerSeconds * Time.deltaTime));
	}

	void ShopButtonColor()
	{
		
	}
}