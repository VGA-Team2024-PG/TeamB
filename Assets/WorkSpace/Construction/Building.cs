using UnityEngine;

[CreateAssetMenu(menuName = "BuildingData")]

//仮で作成した施設のデータ

public class Building : ScriptableObject
{
    [SerializeField, Tooltip("建物名")] string _name;
    [SerializeField, Tooltip("施工に必要な金額")] int _price;
    [SerializeField, Tooltip("製作可能数")] int _craftableNumber;
    [SerializeField, Tooltip("必要な施工時間")] int _workTime;
    [SerializeField, Tooltip("設置するオブジェクト")] GameObject _prefab;
    public string Name => _name;
    public int Price => _price;
    public int CraftableNumber => _craftableNumber;
    public int WorkTime => _workTime;
    public GameObject Prefab => _prefab;
}