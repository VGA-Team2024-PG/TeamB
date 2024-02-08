using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 施設のスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "BaceFactoryData", menuName = "ScriptableObjects/CreateFactoryDataAsset")]
public class BaseFactoryData : ScriptableObject
{
	[SerializeField] List<FactoryData> factoryDatas = new List<FactoryData>();
	public List<FactoryData> FactoryDatas => factoryDatas;
}

[Serializable]
public class FactoryData
{
	[SerializeField] string factoryName;
	[SerializeField] float moneyPerSecond;
	[SerializeField] int prime;

	public string FactoryName => factoryName;
	public float MoneyPerSecond => moneyPerSecond;
	public int Prime => prime;
}