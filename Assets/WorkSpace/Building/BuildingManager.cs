using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    bool _isPlacable = true;
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
    /// 施設を設置する時に浮かす高さの係数(施設の高さ/２に係数をかけた数浮上する)
    /// </summary>
    [SerializeField]float _adjustSpawnYPos = 2.001f; 
    /// <summary>
    /// 現在設置している施設
    /// </summary>
    GameObject _buildingFacilityObj;
    Rigidbody _buildingFacilityObjRb;
    /// <summary>
    /// 現在設置している施設の価格
    /// </summary>
    int _priceBuildingFacilityObj;
    Facility _buildingFacility;
    BoxCollider _colliderFacility;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _facilityDataManager = FindObjectOfType<FacilityDataManager>();
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
        //生成する施設のデータを取得
        _buildingFacility = _facilityDataManager.SearchFacility(facilityEnum);
        _buildingFacilityObj = _buildingFacility.Prefab;
        //ストック残数の確認
        if (_buildingFacility.FacilityStock > _facilityDataManager.FacilityStock[(int)facilityEnum])
        {
            //ここで現在持っているリソース量を確認する
            _priceBuildingFacilityObj = _buildingFacility.Price;
            //施工に必要な金額を持っているなら施設を生成する
            //if ( >= _priceBuildingFacilityObj)
            {
                _buildingFacilityObj = Instantiate(_buildingFacility.Prefab, _spawnPos.transform.position + _floor.transform.up * (_buildingFacilityObj.transform.localScale.y / (2f - _adjustSpawnYPos)), _floor.transform.rotation);
                _buildingFacilityObjRb= _buildingFacilityObj.AddComponent<Rigidbody>();
                _buildingFacilityObjRb.useGravity = false;
                _colliderFacility = _buildingFacilityObj.GetComponent<BoxCollider>();
                _colliderFacility.isTrigger = true;
                _isBuilding = true;
            }
            //else
            //{
            //    Debug.Log("施設を建築するための施工費が不足しています");
            //}
        }
        else
        {
            Debug.Log("施設の最大設置可能数を超えています");
        }
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
            _buildingFacilityObj.transform.position = hit.point + _floor.transform.up * (_buildingFacilityObj.transform.localScale.y / (2f - _adjustSpawnYPos));
        }
    }
    /// <summary>
    /// 施設の設置を決定する関数
    /// </summary>
    public void FinishBuilding()
    {
        if(_isPlacable)
        {
            _colliderFacility.isTrigger = false;
            _isBuilding = false;
            _facilityDataManager.IncreaseFacilityStock(_buildingFacility.FacilityEnum);
            Destroy(_buildingFacilityObjRb);
            Destroy(_buildingFacilityObj.GetComponent<DragDetector>());
            GameObject.Find($"BuyUI{_buildingFacility.Name}").GetComponent<ButtonContentSetter>().SetText();
            //ここで施工金額を現在のリソースから減らす
            //リソースを減らす関数(_priceBuildingFacilityObj);

            //ここに施設が持つ機能を起動する関数などを呼び出す
            //_buildingFacility.GetComponent<>().
        }
        else
        {
            Debug.Log("オブジェクトが重なっています");
        }
    }
    /// <summary>
    /// 施設の設置を取り消す関数
    /// </summary>
    public void CancelBuilding()
    {
        _priceBuildingFacilityObj = 0; 
        _colliderFacility.isTrigger = false;
        _isBuilding = false;
        Destroy(_buildingFacilityObj);
    }
}