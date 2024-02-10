using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[Serializable]
public class CurrentItemData
{
	[SerializeField] private int addResource = 1;
	[SerializeField] private int time;
	[SerializeField] private int prime;

	public int AddResource
	{
		get => addResource;
		set => addResource = value;
	}

	///<summary>倍率</summary>
	public int Time
	{
		get => time;
		set => time = value;
	}

	///<summary>価格</summary>
	public int Prime
	{
		get => prime;
		set => prime = value;
	}

	///<summary>初期化用</summary>
	public CurrentItemData( int Prime, int Time = 2)
	{
		time = Time;
		prime = Prime;
	}
}

/// <summary>クリックのクラス</summary>
public class Click : MonoBehaviour, IPointerClickHandler
{
	private Dictionary<string, CurrentItemData> _itemDic = new();
	
	private void Start()
	{
		_itemDic.Add("クリック", new CurrentItemData(100));
	}

	/// <summary>クリックで増えるリソースを２倍にする</summary>
	public void TwiceClickRecourcee(string name)
	{
		if (ResourceManager.Instance.Resorce >= (ulong)Mathf.Ceil(_itemDic[name].Prime))
		{
			_itemDic[name].AddResource *= _itemDic[name].Time;
			ResourceManager.Instance.UseResorce(_itemDic[name].Prime);
			Facility.Instance.UpdateItem(name);
		}
	}

	/// <summary>
	/// クリックされたときにリソースを増やす
	/// </summary>
	/// <param name="eventData"></param>
	/// <returns></returns>
	public void OnPointerClick(PointerEventData eventData)
	{
		ResourceManager.Instance.AddResorce(_itemDic["クリック"].AddResource);
	}
}