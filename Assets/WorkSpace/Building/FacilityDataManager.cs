using System;
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
    int[] _facilityCount;
    public int[] FacilityCount => _facilityCount;
    void Awake()
    {
        _facilityCount = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
    }
    /// <summary>
    /// 施設の生産数を増やす
    /// </summary>
    /// <param name="building">施設の名前</param>
    public void IncreaseFacilityCount(FacilityEnum facilityEnum)
    {
        _facilityCount[(int)facilityEnum]++;
    }
    /// <summary>
    /// FacilityDataBase内のリストから引数をインデックスとしてデータを返す
    /// </summary>
    public Facility GetFacilityData(int index)
    {
        return _facilityData.FacilityData[index];
    }
}