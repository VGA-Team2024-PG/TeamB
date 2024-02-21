using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionState : MonoBehaviour
{
    [SerializeField, Tooltip("現在の建設状態")] FacilityState _currentState = FacilityState.NotInstalled;

    // このオブジェクトの施設の種類(データを取得してくる)
    Facility _facilityType;
    // 施設の建設に必要な施工時間(データを取得してくる)
    float _workTime = 0;
    // 建設経過時間
    float _elapsedTime = 0;

    void Start()
    {

    }

    void Update()
    {
        if (_currentState == FacilityState.Constructing)
        {
            _elapsedTime = Time.deltaTime;
            if (_elapsedTime > _workTime)
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
