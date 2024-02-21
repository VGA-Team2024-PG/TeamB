using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionState : MonoBehaviour
{
    [SerializeField, Tooltip("���݂̌��ݏ��")] FacilityState _currentState = FacilityState.NotInstalled;

    // ���̃I�u�W�F�N�g�̎{�݂̎��(�f�[�^���擾���Ă���)
    Facility _facilityType;
    // �{�݂̌��݂ɕK�v�Ȏ{�H����(�f�[�^���擾���Ă���)
    float _workTime = 0;
    // ���݌o�ߎ���
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
    /// �{�݂̌��ݏ�Ԃ����ݒ��ɕς���
    /// BuildingManager�ŌĂ�
    /// </summary>
    public void StartConstruction()
    {
        _currentState = FacilityState.Constructing;
    }

    /// <summary>
    /// �{�݂̌��ݏ�Ԃ��Ǘ�����enum
    /// </summary>
    public enum FacilityState
    {
        NotInstalled,   // ���ݒu
        Constructing,   // ���z��
        Working,        // �ғ���
    }

    /// <summary>
    /// ���݂̎{�݂̌��ݏ�Ԃ��擾���邽�߂̊֐�
    /// </summary>
    /// <returns></returns>
    public FacilityState GetFacilityState()
    {
        return _currentState;
    }
}
