using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// <para>ゲーム画面内のUIを切り替える</para>
/// </summary>
public class UIStateChanger : MonoBehaviour
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
    /// 建築物の位置を決めているときのUI
    /// </summary>
    [SerializeField] GameObject _buildingUI;
    UIBuildingContentSetter _buildingContentSetter;
    DataManager _dataManager;
    void Start()
    {
        _dataManager = DataManager.Instance;
        _buildingContentSetter = UIBuildingContentSetter.Instance;
        ChangeUINormal();        
    }
    /// <summary>
    /// 通常画面へ推移
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
    /// 建築位置の選択中の画面へ推移
    /// </summary>
    public void ChangeUIBuilding()
    {
        _normalUI.SetActive(false);
        _facilityListUI.SetActive(false);
        _isChoiceFacilityUI = false;
        _buildingUI.SetActive(true);
    }
}