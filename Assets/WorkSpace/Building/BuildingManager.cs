using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>施設を建築中に管理する</para>
/// </summary>
public class BuildingManager : MonoBehaviour
{
    FacilityDataManager _facilityDataManager;
    /// <summary>
    /// 建築中か区別するbool
    /// </summary>
    bool _isBuilding = false;
    bool _isDragging = false;
    public  bool IsDragging { set { _isDragging = value; } }
    bool _isPlacable = false;
    public bool IsPlacable { set { _isPlacable = value; } }
    /// <summary>
    /// rayが届く最大距離
    /// </summary>
    [SerializeField] float _maxRayDistance = 30f;
    /// <summary>
    /// 施設を設置する床
    /// </summary>
    [SerializeField] GameObject _floor;
    /// <summary>
    /// 施設が最初に置かれる場所
    /// </summary>
    [SerializeField] GameObject _spawnPos;
    /// <summary>
    /// 現在設置している施設
    /// </summary>
    GameObject _buildingFacilityObj;
    Facility _buildingfacility;
    BoxCollider _colliderFacility;
    void Start()
    {
        _facilityDataManager = FindAnyObjectByType<FacilityDataManager>();
    }
    void Update()
    {
        if(_isBuilding && _isDragging)
        {
            MoveFacility();
        }
    }
    /// <summary>
    /// 施設の設置を開始する関数
    /// </summary>
    /// <param name="facilityId">建築する建物のenum</param>
    public void BuildStart(FacilityEnum facilityEnum)
    {

        _buildingfacility = _facilityDataManager.SearchFacility(facilityEnum);
        _buildingFacilityObj = Instantiate(_buildingfacility.Prefab, _spawnPos.transform.position, _floor.transform.rotation);
        _colliderFacility = _buildingFacilityObj.GetComponent<BoxCollider>();
        _colliderFacility.isTrigger = true;
        _isBuilding = true;
    }

    /// <summary>
    /// 設置中の施設をドラッグしているかを確認して、動かす関数
    /// </summary>
    void MoveFacility()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = RectTransformUtility.ScreenPointToRay(Camera.main, mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxRayDistance, layerMask: 3, QueryTriggerInteraction.Ignore))
        {
            _buildingFacilityObj.transform.position = hit.point;
        }
    }
    /// <summary>
    /// 施設の設置を終了する関数
    /// </summary>
    public void FinishBuilding()
    {
        if(_isPlacable)
        {
            _colliderFacility.isTrigger = false;
            _isBuilding = false;
            _facilityDataManager.SetFacilityStock(_buildingfacility.FacilityEnum);
            _buildingFacilityObj.GetComponent<DragDetector>().enabled = false;


            //ここに施設が持つ機能を起動する関数などを呼び出す
            //_buildingFacility.GetComponent<>().
        }
        else
        {
            //施設が他の施設と重なっている時に呼ばれる
            //audioを鳴らすなどをここに記述する
        }
    }
}
