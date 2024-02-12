using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kawaguthi
{
	[System.Serializable]
	public class CurrentItemData
	{
		[SerializeField] private int time = 2;
		[SerializeField] private int prime = 100;

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
		public CurrentItemData(int Prime = 100, int Time = 2)
		{
			time = Time;
			prime = Prime;
		}
	}
	
	///<summary>アイテムｎ</summary>
	public class ItemManager : MonoBehaviour
	{
		private static ItemManager instance;
		public static ItemManager Instance => instance;
		Dictionary<string, CurrentItemData> _itemDic = new();
		public Dictionary<string, CurrentItemData> ItemDatas => _itemDic;
		private Click _click;
		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			_click = FindObjectOfType<Click>();
			_itemDic.Add("カーソル　クリック", new CurrentItemData());
		}
		
		/// <summary>クリックで増えるリソースを２倍にする</summary>
		public void TwiceClickRecourcee(string name)
		{
			foreach (var item in _itemDic.Where(x => name.Contains(x.Key.Split()[1])))
			{
				if (ResourceManager.Instance.Resorce >= (long)Mathf.Ceil(_itemDic[name].Prime))
				{
					_click.ClickTwoTime(name);
				}
			}//クリックのリソース量を増やす
			foreach (var item in _itemDic.Where(x => name.Contains(x.Key.Split()[0])))
			{
				if (ResourceManager.Instance.Resorce >= (long)Mathf.Ceil(_itemDic[name].Prime))
				{
					ResourceManager.Instance.UseResorce(_itemDic[name].Prime);
					Facility.Instance.UpdateItem(name);
				}
			}
		}
	}
}