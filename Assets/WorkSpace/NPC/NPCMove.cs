using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/// <summary>
/// NavMeshでNPCの動きを管理する
/// </summary>
public class NPCMove : MonoBehaviour
{
    [Header("NPCMove")][Space(5)]
    [SerializeField] private NavMeshAgent _navAgent;
    [SerializeField] protected Animator _animator;
    [SerializeField, MinMaxRange(10f, 20f), Tooltip("ランダムウォーク目的地再セットの時間範囲")] private Vector2 _randomWalkIntervalRange;
    [SerializeField, Tooltip("動ける範囲を四角形とし、対角線の2点を指定する")] private Vector4 _radomWalkMoveRange;
    [SerializeField, Tooltip("目標地点到着とする距離")] private float _arrivalDistance;

    private NPCMoveState _moveState = NPCMoveState.RandomWalk;
    private Vector3 _targetPos;
    private bool _isMoving; // SetTargetでしか使用していない　どちらでも到着判定を行う場合、Actionを分けるなどする必要がある
    /// <summary> ターゲットに到着したときのAction </summary>
    protected event Action OnArrivedTarget;

    public NPCMoveState NPCMoveState => _moveState;

    protected virtual void Awake()
    {
        ChangeMoveState(NPCMoveState.RandomWalk);
    }

    /// <summary> NPCの動き方を変更する </summary>
    /// <param name="targetPos"> SetTargetの場合、targetPosを指定する </param>
    protected void ChangeMoveState(NPCMoveState moveState, Vector3 targetPos = new Vector3())
    {
        _moveState = moveState;
        
        if (moveState == NPCMoveState.RandomWalk)
        {
            MoveToRandomPos();
        }
        else
        {
            CancelInvoke(nameof(MoveToRandomPos));
            SetTargetPosition(targetPos);
        }
    }

    /// <summary> 移動可能なランダムな地点に移動する </summary>
    /// <remarks> ランダムな時間で目的地を再設定する </remarks>
    void MoveToRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(_radomWalkMoveRange.x, _radomWalkMoveRange.z), 0,
            Random.Range(_radomWalkMoveRange.y, _radomWalkMoveRange.w));
        NavMesh.SamplePosition(randomPos, out NavMeshHit navMeshHit, 5, 1);
        _navAgent.destination = navMeshHit.position;
        Invoke(nameof(MoveToRandomPos), Random.Range(_randomWalkIntervalRange.x, _randomWalkIntervalRange.y));
    }

    /// <summary> 目的地を指定する</summary>
    void SetTargetPosition(Vector3 targetPos)
    {
        _isMoving = true;
        _navAgent.destination = targetPos;
        _targetPos = targetPos;
    }

    public void RandomSpawn()
    {
        Vector3 randomPos = new Vector3(Random.Range(_radomWalkMoveRange.x, _radomWalkMoveRange.z), 0,
            Random.Range(_radomWalkMoveRange.y, _radomWalkMoveRange.w));
        NavMesh.SamplePosition(randomPos, out NavMeshHit navMeshHit, 5, 1);
        transform.position = navMeshHit.position;
    }

    private void Update()
    {
        // SetTarget時のTargetへの到着判定を行う
        if (_moveState == NPCMoveState.SetTarget && _isMoving)
        {
            // 目標地点に到着していた場合
            if (Vector3.Distance(_targetPos, transform.position) < _arrivalDistance)
            {
                _isMoving = false;
                _navAgent.ResetPath(); // 移動パスを削除する
                OnArrivedTarget?.Invoke();
            }
        }
        
        // animator param
        _animator.SetFloat("Speed", _navAgent.velocity.magnitude);
    }

    private void OnDrawGizmos()
    {
        Vector3 verA = new Vector3(_radomWalkMoveRange.x, 0, _radomWalkMoveRange.y);
        Vector3 verB = new Vector3(_radomWalkMoveRange.x, 0, _radomWalkMoveRange.w);
        Vector3 verC = new Vector3(_radomWalkMoveRange.z, 0, _radomWalkMoveRange.w);
        Vector3 verD = new Vector3(_radomWalkMoveRange.z, 0, _radomWalkMoveRange.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(verA, verB);
        Gizmos.DrawLine(verB, verC);
        Gizmos.DrawLine(verC, verD);
        Gizmos.DrawLine(verD, verA);
    }
}

public enum NPCMoveState
{
    RandomWalk,
    SetTarget // 目的位置を設定し、移動する
}