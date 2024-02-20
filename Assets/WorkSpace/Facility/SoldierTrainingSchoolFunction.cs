using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 兵士育成所(施設)の効果
/// </summary>
public class SoldierTrainingSchoolFunction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("消費ゴールド")] int _price = 100;
    [SerializeField, Tooltip("兵士のプレハブ")] GameObject _soldierPrefab;
    [SerializeField, Tooltip("クリックで増える兵士の数")] int _addResource = 1;

    DataManager _dataManager;
    ConstructionState _constructionState;

    void Start()
    {
        _dataManager = DataManager.Instance;
        _constructionState = GetComponent<ConstructionState>();
    }

    void Update()
    {
        // 兵士の作成上限数を取得できる関数が欲しい
    }

    /// <summary>
    /// 施設をクリックしたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 建設状態が稼働中になったら
        if (_constructionState.GetFacilityState() == ConstructionState.FacilityState.InOperation)
        {
            if (_dataManager.Gold > _price)
            {
                //if (兵士の作成上限数に達していなければ)
                {
                    _dataManager.ChangeGold(_price);
                    Instantiate(_soldierPrefab, this.transform.position, Quaternion.identity);

                    _dataManager.ChangeResource(_addResource);
                }
            }
            else
            {
                Debug.Log("ゴールドが足りません");
            }
        }

    }
}
