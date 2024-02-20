using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 鉱山(施設)の効果
/// </summary>
public class MineFunction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("ゴールドが増える間隔(s)")] float _span = 1f;
    [SerializeField, Tooltip("n秒で増えるゴールド量")] int _goldIncreaseAmount = 10;
    [SerializeField, Tooltip("貯蔵できるゴールドの上限")] float _storageLimit = 1000f;

    DataManager _dataManager;
    ConstructionState _constructionState;

    // 経過時間(_spanでリセット)
    float _currentTime = 0;
    // 現在の貯蔵量
    int _currentGold = 0;

    void Start()
    {
        _dataManager = DataManager.Instance;
        _constructionState = GetComponent<ConstructionState>();
    }

    void Update()
    {
        // 建設状態が稼働中になったら
        if (_constructionState.GetFacilityState() == ConstructionState.FacilityState.Working)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _span)
            {
                if (_currentGold < _storageLimit)
                {
                    _currentGold += _goldIncreaseAmount;
                    _currentTime = 0;
                }
                else
                {
                    _currentTime = 0;
                }
            }
        }

    }

    /// <summary>
    /// 施設をクリックしたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _dataManager.ChangeGold(_currentGold);
        _currentGold = 0;
    }
}
