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
	public ulong MoneyPerSecond;
	public float factoryManey;

	public Factory(string name, ulong addmoney, float NeedManey, int level = 1)
	{
		factoryName = name;
		factoryLevel = level;
		MoneyPerSecond = addmoney;
		factoryManey = NeedManey;
	}
}

public class Facility : MonoBehaviour
{
	static private Facility _instance;
	public static Facility Instance => _instance;
	
	[SerializeField] List<Factory> factories = new List<Factory>();
	[SerializeField] private List<BaseFactoryData> _factoryDatas;
	//テキストの設定を変更したい場合
	[SerializeField] private TextMeshProUGUI _factoryText;

	private void Awake()
	{
	}

	private void Start()
	{
		StartCoroutine(AddCookieOpe());
	}

	public void AddFactories(string name, ulong cookiesPerSecond, float needManey)
	{
		var wherefact = factories.Where(x => x.factoryName == name);
		foreach (var factory in wherefact)
		{
			++factory.factoryLevel;
			factory.MoneyPerSecond *= 2;
			factory.factoryManey *= 1.15f;
		} //施設があった場合レベルと増えるクッキーの値を増やす

		if (wherefact.Count() <= 0)
		{
			factories.Add(new Factory(name, cookiesPerSecond, needManey));
			TextMeshProUGUI _text = Instantiate(new GameObject().AddComponent<TextMeshProUGUI>(), transform);
			_text.text = name;
		} //なかったらリストに追加してテキストを自分の子オブジェクトに出す

		//ここにリソースを減らす処理を書く
	}

	IEnumerator AddCookieOpe()
	{
		while (true)
		{
			float addCookie = 0;
			foreach (var factory in factories)
			{
				addCookie += factory.MoneyPerSecond;
			}
			ResourceManager.Instance.AddResorce(1);
			yield return new WaitForSeconds(1f / addCookie);
		}
	}
}