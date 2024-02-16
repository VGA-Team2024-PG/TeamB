using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para>施設リストの表示を更新する</para>
/// </summary>
public class BuildingListSetter : MonoBehaviour
{

    /// <summary>
    /// ScrollView内のボタンを表示するcontent
    /// </summary>
    [SerializeField] RectTransform _content;
    /// <summary>
    /// 表示するボタンのプレハブ
    /// </summary>
    [SerializeField] GameObject _buttonPrefab;
    FacilityDataManager _facilityDataManager;
    Vector2 _prefabSize;
    int _buttonKinds;
    float _layoutSpacing;
    void Start()
    {
        _facilityDataManager = FindAnyObjectByType<FacilityDataManager>();
        Vector2 _prefabSize = _buttonPrefab.GetComponent<RectTransform>().sizeDelta;
        int _buttonKinds = _facilityDataManager.FacilityDataBase.FacilityData.Count;
        float _layoutSpacing = _content.GetComponent<VerticalLayoutGroup>().spacing;
        SetButtons();
    }
    void Update()
    {

    }
    void SetButtons()
    {
        Vector2 _prefabSize = _buttonPrefab.GetComponent<RectTransform>().sizeDelta;
        int _buttonKinds = _facilityDataManager.FacilityDataBase.FacilityData.Count;
        float _layoutSpacing = _content.GetComponent<VerticalLayoutGroup>().spacing;
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, _prefabSize.y * _buttonKinds + _layoutSpacing * (_buttonKinds - 1));
        for (int i = 0; i < _buttonKinds; i++)
        {
            GameObject button = Instantiate(_buttonPrefab, _content);
            ButtonContentSetter buttonContentSetter = button.GetComponent<ButtonContentSetter>();
            //ボタンに建築する施設のデータを結びつける
            buttonContentSetter.Facility = _facilityDataManager.GetFacilityData(i);
            //ボタンの機能を追加する処理を呼び出す
            buttonContentSetter.SetOnClick();
            //ボタンプレハブ内のテキストを変える処理を呼び出す
            buttonContentSetter.SetText();
        }
    }
}
