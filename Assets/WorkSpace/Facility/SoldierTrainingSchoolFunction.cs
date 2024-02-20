using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 兵士育成所(施設)の効果
/// </summary>
public class SoldierTrainingSchoolFunction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("消費ゴールド")] int _price = 100;
    [SerializeField, Tooltip("兵士のプレハブ")] GameObject _soldierPrefab;

    ResourceManager _resourceManager;

    void Start()
    {
        _resourceManager = ResourceManager.Instance;
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
        if (_resourceManager.Resorce > _price)
        {
            //if (兵士の作成上限数に達していなければ)
            {
                _resourceManager.UseResorce(_price);
                Instantiate(_soldierPrefab, this.transform.position, Quaternion.identity);

                // 兵士のカウントを増やす関数が欲しい
            }
        }
        else
        {
            Debug.Log("ゴールドが足りません");
        }
    }
}
