using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 鉱山(施設)の効果
/// </summary>
public class MineFunction : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// ゴールドが増える間隔(s)
    /// </summary>
    [SerializeField] float _span = 1f;
    /// <summary>
    /// n秒で増えるゴールド量
    /// </summary>
    [SerializeField] int _goldIncreaseAmount = 10;
    /// <summary>
    /// 貯蔵できるゴールドの上限
    /// </summary>
    [SerializeField] float _storageLimit = 1000f;
    DataManager _dataManager;
    WarManager _warManager;
    ConstructionState _constructionState;
    float _elapsedTime = 0;
    int _currentGold = 0;
    private event Action<int> OnChangedGold;

    public int CurrentGold
    {
        get => _currentGold;
        set
        {
            _currentGold = value;
            OnChangedGold?.Invoke(_currentGold);
        }
    }

    void Start()
    {
        _dataManager = DataManager.Instance;
        _warManager = WarManager.Instance;
        _warManager.OnDefeat += LoseGold;
        _constructionState = GetComponent<ConstructionState>();
    }

    void Update()
    {
        if (_constructionState.GetFacilityState() == FacilityState.Working)
        {
            if (CurrentGold < _storageLimit)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= _span)
                {
                    CurrentGold += _goldIncreaseAmount;
                    _elapsedTime = 0;
                }
            }
            else
            {
                _elapsedTime = 0;
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
        CurrentGold = 0;
    }

    public void LoseGold()
    {
        CurrentGold = 0;
    }
}
