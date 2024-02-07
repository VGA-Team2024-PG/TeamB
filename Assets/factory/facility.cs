using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public class Factory
{
	public string FactoryName;
	public int FactoryLevel;
	public float cookiesPerSecond;

	public Factory(string name, int level, float cookie)
	{
		FactoryName = name;
		FactoryLevel = level;
		cookiesPerSecond = cookie;
	}
}

public class Facility : MonoBehaviour
{
	[SerializeField] List<Factory> factories = new List<Factory>();
	[SerializeField] Text _texts;
	
	
	void AddFactories(string name, float cookiesPerSecond)
	{
		foreach (var factory in factories)
		{
			if (factory.FactoryName == name)
			{
				++factory.FactoryLevel;
			}//施設があった場合レベルと増えるクッキーの値を増やす
			else
			{
				factories.Add(new Factory(name, 1, cookiesPerSecond));
				Instantiate(_texts, transform);
			}//なかったらリストに追加してテキストを自分の子オブジェクトに出す
		}
	}

	private void Start()
	{
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
