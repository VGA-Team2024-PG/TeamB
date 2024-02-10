using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 施設のスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "BaceFactoryData", menuName = "ScriptableObjects/CreateFactoryDataAsset")]
public class BaseFactoryData : ScriptableObject
{
	[SerializeField] List<FacilityData> factoryDatas = new ();
	public List<FacilityData> FactoryDatas => factoryDatas;
}

[Serializable]
public class FacilityData
{
	[SerializeField] string factoryName;
	[SerializeField] float moneyPerSecond;
	[SerializeField] int prime;

	public string FactoryName => factoryName;
	public float MoneyPerSecond => moneyPerSecond;
	public int Prime => prime;
}