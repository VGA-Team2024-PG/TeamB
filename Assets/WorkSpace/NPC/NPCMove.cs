using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCMove : MonoBehaviour
{
    [SerializeField, MinMaxRange(10f, 20f)] private Vector2 _intervalRange;
    [SerializeField, Tooltip("動ける範囲を四角形とし、対角線の2点を指定する")] private Vector4 _moveRange;

    private NavMeshAgent _navAgent;
    private Vector3 _targetPos;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        MoveToRandomPos();
    }

    void MoveToRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(_moveRange.x, _moveRange.z), 0,
            Random.Range(_moveRange.y, _moveRange.w));
        NavMesh.SamplePosition(randomPos, out NavMeshHit navMeshHit, 5, 1);
        _navAgent.destination = navMeshHit.position;
        Invoke(nameof(MoveToRandomPos), Random.Range(_intervalRange.x, _intervalRange.y));
    }

    private void OnDrawGizmos()
    {
        Vector3 verA = new Vector3(_moveRange.x, 0, _moveRange.y);
        Vector3 verB = new Vector3(_moveRange.x, 0, _moveRange.w);
        Vector3 verC = new Vector3(_moveRange.z, 0, _moveRange.w);
        Vector3 verD = new Vector3(_moveRange.z, 0, _moveRange.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(verA, verB);
        Gizmos.DrawLine(verB, verC);
        Gizmos.DrawLine(verC, verD);
        Gizmos.DrawLine(verD, verA);
    }
}