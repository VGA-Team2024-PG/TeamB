using TMPro;
using UnityEngine;

public class ButtonTextSetter : MonoBehaviour
{
    [SerializeField] TMP_Text _buttonText;
    /// <summary>
    /// ボタンに紐づけられた施設データ
    /// </summary>
    Facility _facility;
    public Facility Facility { set {  _facility = value; } }
    void Start()
    {

    }
    /// <summary>
    /// ボタンの表示を変える関数
    /// </summary>
    /// <param name="_facilityStock"></param>
    public void SetText(int _facilityStockId)
    {
        //引数をもう少し検討する
        _buttonText.text = $"{_facility.name} {FacilityManager.Instance.FacilityStock[_facilityStockId]}/{_facility.FacilityStock}";
    }
}
