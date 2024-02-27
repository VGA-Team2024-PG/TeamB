using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 工員の動きを管理する
/// </summary>
public class FactoryWorkerController : NPCMove
{
    [Space(10)][Header("FactoryWorkerController")][Space(5)]
    [SerializeField, Tooltip("施設に到着とする最大距離")] private float _arrivalDistanceFacility;
    
    protected override void Awake()
    {
        // 初期はランダムウォークに設定する
        ChangeMoveState(NPCMoveState.RandomWalk);
        OnArrivedTarget += ArrivedFacility;
    }

    /// <summary> 施設の建設を開始する </summary>
    public void SetWork(Vector3 facilityPos)
    {
        NavMesh.SamplePosition(facilityPos, out NavMeshHit hit, _arrivalDistanceFacility, 1);
        ChangeMoveState(NPCMoveState.SetTarget, hit.position);
    }

    /// <summary> 施設に到着した時の処理 </summary>
    void ArrivedFacility()
    {
        // アニメーション
        _animator.SetBool("IsUnderConstruction", true);
    }

    /// <summary> 施設建設が終わったとき </summary>
    public void FinishBuildingFacility()
    {
        // ランダムウォークに戻る
        ChangeMoveState(NPCMoveState.RandomWalk);
        _animator.SetBool("IsUnderConstruction", false);
    }
}
