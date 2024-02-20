using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionState : MonoBehaviour
{
    [SerializeField, Tooltip("���݂̌��ݏ��")] FacilityState _currentState = FacilityState.NotInstalled;

    void Start()
    {
        
    }

    void Update()
    {
        if (_currentState == FacilityState.Construction)
        {
            // �X�e�[�g��ψڂ����Ă���

        }
    }

    // ���݊J�n�@��BuildingManager�ŌĂ�
    public void StartConstruction()
    {
        _currentState = FacilityState.Construction;
    }

    public enum FacilityState
    {
        NotInstalled,   // ���ݒu
        Construction,   // ���z��
        InOperation,    // �ғ���
    }

    public FacilityState GetFacilityState()
    {
        return _currentState;
    }
}
