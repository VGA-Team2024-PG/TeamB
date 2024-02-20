using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 鉱山(施設)の効果
/// </summary>
public class MineFunction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("リソースが増える間隔(s)")] float _span = 1f;
    [SerializeField, Tooltip("n秒で増えるリソース量")] float _resourceIncreaseAmount = 10f;
    [SerializeField, Tooltip("貯蔵できるリソースの上限")] float _storageLimit = 1000f;

    ResourceManager _resourceManager;

    // 経過時間(_spanでリセット)
    float _currentTime = 0;
    // 現在の貯蔵量
    float _currentResource = 0;

    void Start()
    {
        //_resourceManager = ResourceManager.Instance;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _span)
        {
            if (_currentResource < _storageLimit)
            {
                _currentResource += _resourceIncreaseAmount;
                _currentTime = 0;
            }
            else
            {
                _currentTime = 0;
            }
        }
    }

    /// <summary>
    /// 施設をクリックしたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _resourceManager.AddResorce(_currentResource);
        _currentResource = 0;
    }
}
