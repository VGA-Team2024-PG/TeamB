using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionSetter : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _buttonText;
    BuildingManager _buildingManager;
    DataManager _dataManager;
    /// <summary>
    /// ボタンに紐づけられた施設データ
    /// </summary>
    Facility _facility;
    public Facility Facility { set {  _facility = value; } }
    void Awake()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _buildingManager = FindObjectOfType<BuildingManager>();
    }
    void Start()
    {
        this.name = $"UIBuyButton{_facility.Name}";
    }
    public void SetOnClick()
    {
        _button.onClick.AddListener(() => _buildingManager.BuildStart(_facility.FacilityEnum));
    }
    /// <summary>
    /// ボタンの表示を変える関数
    /// </summary>
    /// <param name="_facilityStock"></param>
    public void SetText()
    {
        _buttonText.text = $"{_facility.name} {_dataManager.FacilityCount[(int)_facility.FacilityEnum]}/{_facility.FacilityStock}";
    }
}
