using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionState : MonoBehaviour
{
    /// <summary>
    /// 施設の種類
    /// </summary>
    [SerializeField] FacilityEnum _facilityEnum;

    /// <summary>
    /// 現在の建設状態
    /// </summary>
    [SerializeField] FacilityState _currentState = FacilityState.NotInstalled;
    /// <summary>
    /// 建設に必要な施工時間
    /// </summary>
    [SerializeField] float _workTime = 0;
    DataManager _dataManager;
    Facility _facilityType;
    float _elapsedTime = 0;

    void Start()
    {
        _dataManager = DataManager.Instance;
        _facilityType = _dataManager.GetFacilitydata((int)_facilityEnum);
        _workTime = _facilityType.WorkTime;
    }

    void Update()
    {
        if (_currentState == FacilityState.Constructing)
        {
            _elapsedTime = Time.deltaTime;
            if (_elapsedTime >= _workTime)
            {
                _currentState = FacilityState.Working;
            }
        }
    }

    /// <summary>
    /// 施設の建設状態を建設中に変える
    /// BuildingManagerで呼ぶ
    /// </summary>
    public void StartConstruction()
    {
        _currentState = FacilityState.Constructing;
    }

    /// <summary>
    /// 施設の建設状態を管理するenum
    /// </summary>
    public enum FacilityState
    {
        NotInstalled,   // 未設置
        Constructing,   // 建築中
        Working,        // 稼働中
    }

    /// <summary>
    /// 現在の施設の建設状態を取得するための関数
    /// </summary>
    /// <returns></returns>
    public FacilityState GetFacilityState()
    {
        return _currentState;
    }
}
