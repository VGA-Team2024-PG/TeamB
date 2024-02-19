using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 施設のデータ管理を行う
/// </summary>
public class FacilityDataManager : MonoBehaviour
{
    /// <summary>
    /// 施設のデータ
    /// </summary>
    [SerializeField] FacilityDataBase _facilityData;
    public FacilityDataBase FacilityDataBase => _facilityData;
    /// <summary>
    /// 施設の生産数
    /// </summary>
    int[] _facilityStock;
    public int[] FacilityStock => _facilityStock;
    void Awake()
    {
        _facilityStock = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
    }
    /// <summary>
    /// 施設の生産数を増やす
    /// </summary>
    /// <param name="building">施設の名前</param>
    public void IncreaseFacilityStock(FacilityEnum facilityEnum)
    {
        _facilityStock[(int)facilityEnum]++;
    }
    /// <summary>
    /// FacilityDataBase内のリストから与えられたenumを持つ施設データを返す/存在しない場合nullを返す
    /// </summary>
    /// <param name="facilityEnum">探したいenum</param>
    /// <returns></returns>
    public Facility SearchFacility(FacilityEnum facilityEnum)
    {
        foreach(Facility facility in _facilityData.FacilityData)
        {
            if(facility.FacilityEnum == facilityEnum)
            {
                return facility;
            }
        }
        return null;
    }
    /// <summary>
    /// FacilityDataBase内のリストから引数をインデックスとしてデータを返す
    /// </summary>
    public Facility GetFacilityData(int index)
    {
        return _facilityData.FacilityData[index];
    }
}