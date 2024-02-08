using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateFactoryDataAsset")]
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
	[SerializeField] float prime;

	public string FactoryName => factoryName;
	public float MoneyPerSecond => moneyPerSecond;
	public float Prime => prime;
}