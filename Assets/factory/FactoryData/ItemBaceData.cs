using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>アイテムのベースデータ</summary>
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemData")]
public class ItemBaceData : ScriptableObject
{
	[SerializeField] List<ItemData> itemDatas = new();
	public List<ItemData> ItemDatas => itemDatas;
}

[Serializable]
public class ItemData
{
	[SerializeField] string itemName;

	public string ItemName => itemName;
}