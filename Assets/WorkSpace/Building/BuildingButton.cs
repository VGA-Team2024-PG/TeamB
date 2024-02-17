using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// <para>建築タブのアクティブを切り替える</para>
/// メインキャンバスの建築ボタンにアタッチする
/// </summary>
public class BuildingButton : MonoBehaviour
{
    /// <summary>
    /// メインキャンバスの建築タブ
    /// </summary>
    [SerializeField] GameObject _buildingTab;
    Button _button;
    bool _enabled;
    void Start()
    {
        _button = GetComponent<Button>();
        _enabled = _buildingTab.activeInHierarchy;
        _button.onClick.AddListener(() => ActiveChange());
    }
    /// <summary>
    /// 
    /// </summary>
    void ActiveChange()
    {
        _enabled = !_enabled;
        _buildingTab.SetActive(_enabled);
    }
}