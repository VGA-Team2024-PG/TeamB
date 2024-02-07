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
	public float cookiesPerSecond;
	public float factoryManey;
	
	public Factory(string name, int level, float cookie, float NeedManey)
	{
		factoryName = name;
		factoryLevel = level;
		cookiesPerSecond = cookie;
		factoryManey = NeedManey;
	}
}

public class Facility : MonoBehaviour
{
	[SerializeField] List<Factory> factories = new List<Factory>();
	//テキストの設定を変更したい場合
	[SerializeField] private TextMeshProUGUI _factoryText;
	
	void AddFactories(string name, float cookiesPerSecond, float needManey)
	{
		var wherefact = factories.Where(x => x.factoryName == name);
		foreach (var factory in wherefact)
		{
			++factory.factoryLevel;
			factory.cookiesPerSecond *= 2;
			factory.factoryManey *= 1.15f;
		}//施設があった場合レベルと増えるクッキーの値を増やす

		if(wherefact.Count() <= 0)
		{
			factories.Add(new Factory(name, 1, cookiesPerSecond, needManey));
			TextMeshProUGUI _text = Instantiate(new GameObject().AddComponent<TextMeshProUGUI>(), transform);
			_text.text = name;
		}//なかったらリストに追加してテキストを自分の子オブジェクトに出す
	}

	private void Start()
	{
		//AddFactories("aaa", 1f, 1);
		StartCoroutine(AddCookieOpe());
	}

	IEnumerator AddCookieOpe()
	{
		while (true)
		{
			foreach (var factory in factories)
			{
				//クッキーを増やす処理をここに書く
			}
			yield return new WaitForSeconds(1f);
		}
	}

}
