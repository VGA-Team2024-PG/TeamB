using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionState : MonoBehaviour
{
    [SerializeField, Tooltip("現在の建設状態")] FacilityState _currentState = FacilityState.NotInstalled;

    void Start()
    {
        
    }

    void Update()
    {
        if (_currentState == FacilityState.Construction)
        {
            // ステートを変移させていく

        }
    }

    // 建設開始　をBuildingManagerで呼ぶ
    public void StartConstruction()
    {
        _currentState = FacilityState.Construction;
    }

    public enum FacilityState
    {
        NotInstalled,   // 未設置
        Construction,   // 建築中
        InOperation,    // 稼働中
    }

    public FacilityState GetFacilityState()
    {
        return _currentState;
    }
}
