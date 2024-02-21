using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 兵士育成所(施設)の効果
/// </summary>
public class SoldierTrainingSchoolFunction : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 消費ゴールド
    /// </summary>
    [SerializeField] int _price = 100;
    /// <summary>
    /// 兵士のプレハブ
    /// </summary>
    [SerializeField] GameObject _soldierPrefab;
    /// <summary>
    /// 1クリックで増える兵士の数
    /// </summary>
    [SerializeField] int _addResource = 1;
    /// <summary>
    /// 生成上限を決める施設の種類(キャンプ)
    /// </summary>
    [SerializeField] FacilityEnum _facilityEnum = FacilityEnum.Camp;
    /// <summary>
    /// 施設の数(キャンプ)
    /// </summary>
    [SerializeField] int _campCount = 0;
    /// <summary>
    /// 兵士の生成上限
    /// </summary>
    [SerializeField] int _soldierSpawnLimit;
    DataManager _dataManager;
    ConstructionState _constructionState;

    void Start()
    {
        _dataManager = DataManager.Instance;
        _constructionState = GetComponent<ConstructionState>();
    }

    void Update()
    {
        _campCount = _dataManager.FacilityCount[(int)_facilityEnum];
        _soldierSpawnLimit = _campCount * 50;
    }

    /// <summary>
    /// 施設をクリックしたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_constructionState.GetFacilityState() == ConstructionState.FacilityState.Working)
        {
            if (_dataManager.Gold >= _price)
            {
                if (_dataManager.Resource <= _soldierSpawnLimit)
                {
                    _dataManager.ChangeGold(-_price);
                    // 生成位置変更予定
                    Instantiate(_soldierPrefab, this.transform.position, Quaternion.identity);
                    _dataManager.ChangeResource(_addResource);
                }
                else
                {
                    Debug.Log("兵士の生成数が上限に達しました");
                }
            }
            else
            {
                Debug.Log("ゴールドが足りません");
            }
        }
    }
}
