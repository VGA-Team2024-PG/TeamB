using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para>施設リストの表示を更新する</para>
/// </summary>
public class FacilityListSetter : MonoBehaviour
{

    /// <summary>
    /// ScrollView内のボタンを表示するcontent
    /// </summary>
    [SerializeField] RectTransform _content;
    /// <summary>
    /// 表示するボタンのプレハブ
    /// </summary>
    [SerializeField] GameObject _buttonPrefab;
    FacilityManager _facilityManager;
    Vector2 _prefabSize;
    int _buttonKinds;
    float _layoutSpacing;
    void Awake()
    {
        _facilityManager = FindObjectOfType<FacilityManager>();
    }
    void Start()
    {
        Vector2 _prefabSize = _buttonPrefab.GetComponent<RectTransform>().sizeDelta;
        int _buttonKinds = _facilityManager.FacilityStock.Length;
        float _layoutSpacing = _content.GetComponent<VerticalLayoutGroup>().spacing;
        SetButtons();
    }
    void Update()
    {

    }
    void SetButtons()
    {
        Vector2 _prefabSize = _buttonPrefab.GetComponent<RectTransform>().sizeDelta;
        int _buttonKinds = _facilityManager.FacilityStock.Length;
        float _layoutSpacing = _content.GetComponent<VerticalLayoutGroup>().spacing;
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, _prefabSize.y * _buttonKinds + _layoutSpacing * (_buttonKinds - 1));
        for (int i = 0; i < _buttonKinds; i++)
        {
            GameObject button = Instantiate(_buttonPrefab, _content);
            ButtonTextSetter buttonTextSetter = button.GetComponent<ButtonTextSetter>();
            //ボタンに建築する施設のデータを結びつける（IDか何かを持たせたほうが良いかも）
            buttonTextSetter.Facility = _facilityManager.GetFacilityData(i);
            //ボタンプレハブ内の関数でテキストを変える処理を呼び出す
            buttonTextSetter.SetText(i);
        }
    }
}
