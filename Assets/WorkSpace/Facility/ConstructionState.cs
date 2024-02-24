using UnityEngine;

/// <summary>
/// 建設状態を管理する
/// </summary>
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
    Facility _facilityType;
    float _elapsedTime = 0;

    /// <summary> Dataから特定の状態にする </summary>
    public void InitializeFacilityData(FacilitySaveData facilitySaveData)
    {
        _facilityType = DataManager.Instance.GetFacilitydata((int)_facilityEnum);
        transform.position = facilitySaveData.Position;
        _currentState = facilitySaveData.FacilityState;
        _elapsedTime = facilitySaveData.BuildingTime;
        DataManager.Instance.AddFacilityData(this);

        if (_facilityEnum == FacilityEnum.Mine)
        {
            GetComponent<MineFunction>().CurrentGold = facilitySaveData.MineStorage;
        }

        if (_currentState == FacilityState.Constructing)
        {
            DataManager.Instance.ChangeFactoryWorker(-1);
        }
    }

    void Update()
    {
        if (_currentState == FacilityState.Constructing)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _facilityType.WorkTime)
            {
                _currentState = FacilityState.Working;
                DataManager.Instance.ChangeFactoryWorker(1);
                DataManager.Instance.AddFacilityCount(_facilityEnum);
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
    /// 現在の施設の建設状態を取得するための関数
    /// </summary>
    /// <returns></returns>
    public FacilityState GetFacilityState()
    {
        return _currentState;
    }

    /// <summary> 現在の状態をセーブ出来る形に変える </summary>
    public FacilitySaveData GetFacilitySaveData()
    {
        FacilitySaveData facilitySaveData = new FacilitySaveData(_facilityEnum, transform.position, _currentState, _elapsedTime);

        if (_facilityEnum == FacilityEnum.Mine)
        {
            facilitySaveData.MineStorage = GetComponent<MineFunction>().CurrentGold;
        }
        
        return facilitySaveData;
    }
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