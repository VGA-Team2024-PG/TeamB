using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public class Factory
{
	public string factoryName;
	public int factoryLevel;
	public float resourcePerSecond;
	public float prime;

	public Factory(string name, float resourcePerSecond, float Prime, int level = 1)
	{
		factoryName = name;
		factoryLevel = level;
		this.resourcePerSecond = resourcePerSecond;
		prime = Prime;
	}
}

public class Facility : MonoBehaviour
{
	static private Facility _instance;
	public static Facility Instance => _instance;
	
	//買った施設
	List<Factory> buyedFactories = new List<Factory>();
	//全部の施設のデータ
	[SerializeField] private BaseFactoryData _factoryDatas;
	//テキストの設定を変更したい場合
	[SerializeField] private TextMeshProUGUI _factoryText;

	private void Awake()
	{
	}

	private void Start()
	{
		AddFactories("あああ");
		StartCoroutine(AddCookieOpe());
	}

	/// <summary>
	/// 施設を追加する。あったらレベルを上げる（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void AddFactories(string name)
	{
		var wherefact = buyedFactories.Where(x => x.factoryName == name);
		var factData = _factoryDatas.FactoryDatas.Where(x => x.FactoryName == name);
		if (wherefact.Count() <= 0)
		{
			foreach (var factory in factData)
			{
				//リソースが足りなかったら買えない
				if (ResourceManager.Instance.Resorce >= (ulong)factory.Prime)
				{
					buyedFactories.Add(new Factory(name, factory.MoneyPerSecond, factory.Prime));
					TextMeshProUGUI _text = Instantiate(new GameObject().AddComponent<TextMeshProUGUI>(), transform);
					_text.text = name;
					ResourceManager.Instance.UseResorce(factory.Prime);
					break;
				}
			}
		} //なかったらリストに追加してテキストを自分の子オブジェクトに出す
		else
		{
			foreach (var factory in wherefact)
			{
				//リソースが足りなかったら買えない
				if (ResourceManager.Instance.Resorce >= (ulong)factory.prime)
				{
					++factory.factoryLevel;
					factory.resourcePerSecond *= 2;
					factory.prime *= 1.15f;
					ResourceManager.Instance.UseResorce((int)factory.prime);
					break;
				}
			} //施設があった場合施設レベルと増えるクッキーの値を増やす
		}
		
	}

	IEnumerator AddCookieOpe()
	{
		while (true)
		{
			float addCookie = 0;
			foreach (var factory in buyedFactories)
			{
				addCookie += factory.resourcePerSecond;
			}
			ResourceManager.Instance.AddResorce(1);
			yield return new WaitForSeconds(1f / addCookie);
		}
	}
}