using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{//仮の施設管理のスクリプト
    static BuildingsManager _instance;
    public static BuildingsManager Instance => _instance;
    
    bool _isBuilding = false;
    public bool IsBuilding
    {
        get { return _isBuilding; }
        set { _isBuilding = value; }
    }
        /// <summary>
        /// 仮の建物のデータ
        /// </summary>
        [SerializeField] List<Building> _buildingsList;
    public List<Building> BuildingsList => _buildingsList;
    /// <summary>
    /// 建築物の生産数
    /// </summary>
    int[] _buildingsCnts;
    public int[] BuildingsCnt => _buildingsCnts;
    /// <summary>
    /// 建築物の生産数を増やす
    /// </summary>
    /// <param name="building">建築物の名前</param>
    public void SetBuildingsCnt(Building building)
    {
        //_buildingsCnts[_buildingsDictionary[building]]++;
    }
    private void Awake()
    {
        _buildingsCnts = new int[_buildingsList.Count];
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}