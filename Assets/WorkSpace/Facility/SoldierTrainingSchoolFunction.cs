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
    ConstructionState _constructionState;
    int _soldierSpawnLimit;

    void Start()
    {
        _constructionState = GetComponent<ConstructionState>();
        DataManager.Instance.OnChangedFacilityCount += LimitUpdate;
        LimitUpdate(DataManager.Instance.FacilityCount);
    }

    void LimitUpdate(int[] countList)
    {
        _soldierSpawnLimit = countList[(int)_facilityEnum] * 50;
    }

    /// <summary>
    /// 施設をクリックしたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_constructionState.GetFacilityState() == FacilityState.Working)
        {
            if (DataManager.Instance.Gold >= _price)
            {
                if (DataManager.Instance.Resource < _soldierSpawnLimit)
                {
                    DataManager.Instance.ChangeGold(-_price);
                    // 生成位置変更予定
                    if (_soldierPrefab != null)
                    {
                        WarManager.Instance.Soldiers.Enqueue(Instantiate(_soldierPrefab, this.transform.position, Quaternion.identity));
                        DataManager.Instance.ChangeResource(_addResource);
                    }
                    else
                    {
                        Debug.LogWarning("兵士のプレハブがありません");
                    }
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
