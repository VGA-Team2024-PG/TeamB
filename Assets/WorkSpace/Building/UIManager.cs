using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// <para>建築タブのアクティブを切り替える</para>
/// メインキャンバスの建築ボタンにアタッチする
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 通常時のUI
    /// </summary>
    [SerializeField] GameObject _normalUI;
    /// <summary>
    /// 建築する施設の選択時のUI
    /// </summary>
    [SerializeField] GameObject _facilityListUI;
    bool _isChoiceFacilityUI = false;
    /// <summary>
    /// 建築する位置を決めているときのUI
    /// </summary>
    [SerializeField] GameObject _buildingUI;
    UIBuildingContentSetter _buildingContentSetter;
    void Start()
    {
        _buildingContentSetter = FindObjectOfType<UIBuildingContentSetter>();
        ChangeUINormal();        
    }
    /// <summary>
    /// 通常画面へ偏移
    /// </summary>
    public void ChangeUINormal()
    {
        _normalUI.SetActive(true);
        _facilityListUI.SetActive(false);
        _isChoiceFacilityUI = false;
        _buildingUI.SetActive(false);
    }
    /// <summary>
    /// 施設リストの表示を切り替える
    /// </summary>
    public void ChangeUIChoiceFacility()
    {
        _isChoiceFacilityUI = !_isChoiceFacilityUI;
        _facilityListUI.SetActive(_isChoiceFacilityUI);
        if(_isChoiceFacilityUI == true)
        {
            _buildingContentSetter.ResetButtonsList();
        }
    }
    /// <summary>
    /// 建築位置の選択中の画面へ偏移
    /// </summary>
    public void ChangeUIBuilding()
    {
        _normalUI.SetActive(false);
        _facilityListUI.SetActive(false);
        _isChoiceFacilityUI = false;
        _buildingUI.SetActive(true);
    }
}