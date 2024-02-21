using UnityEngine;

/// <summary>
/// ���ݏ�Ԃ��Ǘ�����
/// </summary>
public class ConstructionState : MonoBehaviour
{
    /// <summary>
    /// �{�݂̎��
    /// </summary>
    [SerializeField] FacilityEnum _facilityEnum;
    /// <summary>
    /// ���݂̌��ݏ��
    /// </summary>
    [SerializeField] FacilityState _currentState = FacilityState.NotInstalled;
    /// <summary>
    /// ���݂ɕK�v�Ȏ{�H����
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
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _workTime)
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
