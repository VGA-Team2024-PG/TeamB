using UnityEngine;
[CreateAssetMenu(menuName = "FacilityData")]
public class Facility : ScriptableObject
{
    [SerializeField, Tooltip("施設の判別用enum")] FacilityEnum _facilityEnum;
    [SerializeField, Tooltip("建物名")] string _name;
    [SerializeField, Tooltip("施工に必要な金額")] int _price;
    [SerializeField, Tooltip("製作可能数")] int _facilityStock;
    [SerializeField, Tooltip("必要な施工時間(s)")] int _workTime;
    [SerializeField, Tooltip("設置するオブジェクト")] GameObject _prefab;
    public FacilityEnum FacilityEnum => _facilityEnum;
    public string Name => _name;
    public int Price => _price;
    public int FacilityStock => _facilityStock;
    public int WorkTime => _workTime;
    public GameObject Prefab => _prefab;
}