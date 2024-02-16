using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour
{
    static FacilityManager _instance;
    public static FacilityManager Instance => _instance;
    
    bool _isBuilding = false;
    public bool IsBuilding
    {
        get { return _isBuilding; }
        set { _isBuilding = value; }
    }
    /// <summary>
    /// 仮の建物のデータ
    /// </summary>
    [SerializeField] FacilityDataBase _facilityData;
    //要らない？
    ///public BuildingDataBase BuildingsData => _buildingsData;
    
    /// <summary>
    /// 建築物の生産数
    /// </summary>
    int[] _facilityStock;
    public int[] FacilityStock => _facilityStock;
    /// <summary>
    /// 建築物の生産数を増やす
    /// </summary>
    /// <param name="building">建築物の名前</param>
    public void SetBuildingsCnt(Facility building)
    {
        //_buildingsCnts[_buildingsDictionary[building]]++;
    }
    /// <summary>
    /// 
    /// </summary>
    public Facility GetFacilityData(int id)
    {
        return _facilityData.FacilityData[id];
    }
    private void Awake()
    {
        _facilityStock = new int[_facilityData.FacilityData.Count];
    }
    void Start()
    {

    }
    void Update()
    {
        
    }
}