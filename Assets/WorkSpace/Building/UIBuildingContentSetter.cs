using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para>施設リストの表示を更新する</para>
/// </summary>
public class UIBuildingContentSetter : MonoBehaviour
{
    static UIBuildingContentSetter _instance;
    public static UIBuildingContentSetter Instance => _instance;
    /// <summary>
    /// ScrollView内のボタンを表示するcontent
    /// </summary>
    [SerializeField] RectTransform _content;
    /// <summary>
    /// 表示するボタンのプレハブ
    /// </summary>
    [SerializeField] GameObject _buttonPrefab;
    DataManager _dataManager;
    private void Awake()
    {
        if(_instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        _dataManager = DataManager.Instance;
    }
    /// <summary>
    /// ボタンの並びを直す
    /// </summary>
    public void ResetButtonsList()
    {
        int contentCnt = _content.childCount;
        if (contentCnt == 0)
        {
            int _buttonKinds = _dataManager.Facilitystock.Length;
            for (int i = 0; i < _buttonKinds; i++)
            {
                GameObject button = Instantiate(_buttonPrefab, _content);
                ButtonActionSetter buttonContentSetter = button.GetComponent<ButtonActionSetter>();
                //ボタンに建築する施設のデータを結びつける
                buttonContentSetter.Facility = _dataManager.GetFacilitydata(i);
                //ボタンの機能を追加する処理を呼び出す
                buttonContentSetter.SetOnClick();
                //ボタンプレハブ内のテキストを変える処理を呼び出す
                buttonContentSetter.SetText();
            }
        }
        else
        {
            for (int i = 0; i < contentCnt; i++)
            {
                GameObject button = _content.transform.GetChild(i).gameObject;
                ButtonActionSetter buttonContentSetter = button.GetComponent<ButtonActionSetter>();
                //ボタンプレハブ内のテキストを変える処理を呼び出す
                buttonContentSetter.SetText();
            }
        }
    }
}
