using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

/// <summary>
/// 施設のスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "BaceFactoryData", menuName = "ScriptableObjects/CreateFactoryDataAsset")]
public class BaseFacilityData : ScriptableObject
{
	[SerializeField] List<FacilityData> facilityDatas = new ();
	public List<FacilityData> FacilityDatas => facilityDatas;
}

[Serializable]
public class FacilityData
{
	[FormerlySerializedAs("factoryName")] [SerializeField] string facilityName;
	[SerializeField] float moneyPerSecond;
	[SerializeField] int prime;

	public string FacilityName => facilityName;
	public float MoneyPerSecond => moneyPerSecond;
	public int Prime => prime;
}