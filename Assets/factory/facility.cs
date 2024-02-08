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
	public int factoryLevel;
	public float resourcePerSecond;
	public float prime;

	public Factory(float resourcePerSecond, float Prime, int level = 1)
	{
		factoryLevel = level;
		this.resourcePerSecond = resourcePerSecond;
		prime = Prime;
	}
}

/// <summary>
/// 施設のクラス
/// </summary>
public class Facility : MonoBehaviour
{
	static private Facility _instance;
	public static Facility Instance => _instance;

	//全施設のデータ
	[SerializeField] private BaseFactoryData _factoryDatas;
	[SerializeField] private TextMeshProUGUI[] _factoryTexts;
	[SerializeField, Header("施設を買うボタン")] private TextMeshProUGUI[] _buttonTeext;

	//買った施設
	private Dictionary<string, Factory> buyedFactories = new Dictionary<string, Factory>();

	private Action _action;

	private void Awake()
	{
		_action += ReflectText;
	}

	private void Start()
	{
		StartCoroutine(AddCookieOpe());
	}

	/// <summary>
	/// 施設を追加する。あったらレベルを上げる（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void AddFactories(string name)
	{
		if (buyedFactories == null || buyedFactories.ContainsKey(name) == false)
		{
			var factData = _factoryDatas.FactoryDatas.Where(x => x.FactoryName == name);
			foreach (var factory in factData)
			{
				//リソースが足りなかったら買えない
				if (ResourceManager.Instance.Resorce >= (ulong)factory.Prime)
				{
					buyedFactories.Add(name, new Factory(factory.MoneyPerSecond, factory.Prime));
					ResourceManager.Instance.UseResorce(factory.Prime);
					_action();
					break;
				}

			}
		} //なかったらリストに追加してテキストを自分の子オブジェクトに出す
		else if (ResourceManager.Instance.Resorce >= (ulong)buyedFactories[name].prime)
		{
				++buyedFactories[name].factoryLevel;
				buyedFactories[name].resourcePerSecond *= buyedFactories[name].factoryLevel;
				buyedFactories[name].prime *= 1.15f;
				ResourceManager.Instance.UseResorce((int)buyedFactories[name].prime);
				_action();
		} //施設があった場合施設レベルと増えるクッキーの値を増やす
	}

	/// <summary>
	/// アップデートアイテム（ボタンで呼び出す用）
	/// </summary>
	/// <param name="name"></param>
	public void UpdateItem(string name)
	{
		if (ResourceManager.Instance.Resorce >= buyedFactories[name].prime)
		{
			buyedFactories[name].resourcePerSecond *= 2;
			ResourceManager.Instance.UseResorce((int)buyedFactories[name].prime);
		}
	}

	void ReflectText()
	{
		int i = 0;
		foreach (var buyedFactory in buyedFactories)
		{
			_factoryTexts[i].gameObject.SetActive(true);
			_factoryTexts[i].text = $"{buyedFactory.Key} {buyedFactory.Value.factoryLevel}";
			++i;
		}
	}

	IEnumerator AddCookieOpe()
	{
		while (true)
		{
			float addCookie = 0.0000001f;
			foreach (var factory in buyedFactories.Values)
			{
				addCookie += factory.resourcePerSecond;
			}
			ResourceManager.Instance.AddResorce(1);
			yield return new WaitForSeconds(1f / addCookie);
		}
	}
}