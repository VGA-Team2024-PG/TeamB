using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// <para>施設を建築中に管理する</para>
/// </summary>
public class BuildingSpawnManager : MonoBehaviour
{
    static BuildingSpawnManager _instance;
    public static BuildingSpawnManager Instance => _instance;
    /// <summary>
    /// 建築中か区別するbool
    /// </summary>
    bool _isBuilding = false;
    bool _isPlacable = false;
    public  bool IsPlacable { set { _isPlacable = value; } }
    /// <summary>
    /// rayが届く最大距離
    /// </summary>
    [SerializeField] public float _maxRayDistance = 30f;
    /// <summary>
    /// 施設を設置する床
    /// </summary>
    [SerializeField] GameObject _floor;
    /// <summary>
    /// 施設が最初に置かれる場所
    /// </summary>
    [SerializeField] GameObject _spawnPos;
    /// <summary>
    /// 現在設置している施設
    /// </summary>
    GameObject _buildingFacilityObj;
    /// <summary>
    /// 現在設置している施設の価格
    /// </summary>
    int _priceBuildingFacilityObj;
    Facility _buildingFacility;
    UIStateChanger _UIManager;
    private void Awake()
    {
        if (_instance)
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
        _UIManager = FindObjectOfType<UIStateChanger>();
    }
    /// <summary>
    /// 施設の設置を開始する関数
    /// </summary>
    /// <param name="facilityId">建築する建物のenum</param>
    public void BuildStart(FacilityEnum facilityEnum)
    {
        //建築可能な工員がいるか確認
        if(DataManager.Instance.FactoryWorkerController.NPCMoveState == NPCMoveState.RandomWalk)
        {
            //生成する施設のデータを取得
            _buildingFacility = DataManager.Instance.GetFacilitydata((int)facilityEnum);
            _buildingFacilityObj = _buildingFacility.Prefab;
            //ストック残数の確認
            if (DataManager.Instance.Facilitystock[(int)facilityEnum] > 0)
            {
                //ここで現在持っているリソース量を確認する
                _priceBuildingFacilityObj = _buildingFacility.Price;
                //施工に必要な金額を持っているなら施設を生成する
                if (DataManager.Instance.Gold >= _priceBuildingFacilityObj)
                {
                    _UIManager.ChangeUIBuilding();
                    _buildingFacilityObj = Instantiate(_buildingFacility.Prefab);
                    _buildingFacilityObj.GetComponent<ConstructionState>()
                        .InitializeFacilityData(new FacilitySaveData(_buildingFacility.FacilityEnum, _spawnPos.transform.position));
                    _isBuilding = true;
                }
                else
                {
                    Debug.Log("施設を建築するための施工費が不足しています");
                }
            }
            else
            {
                Debug.Log("施設の最大設置可能数を超えています");
            }
        }
        else
        {
            Debug.Log("全ての工員が建築中です");
        }
    }
    /// <summary>
    /// 施設の設置を決定する関数
    /// </summary>
    public void FinishBuilding()
    {
        if (_isBuilding && _isPlacable && DataManager.Instance.FactoryWorkerController.NPCMoveState == NPCMoveState.RandomWalk)
        {
            _isBuilding = false;
            _buildingFacilityObj.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Facility");
            _buildingFacilityObj.layer = LayerMask.NameToLayer("Facility");
            DataManager.Instance.DecreaseFacilityStock(_buildingFacility.FacilityEnum);
            Destroy(_buildingFacilityObj.GetComponentInChildren<FacilityMover>());
            //ここで施工金額を現在のゴールドから減らす
            DataManager.Instance.ChangeGold(-_priceBuildingFacilityObj);
            //施設の状態を推移するメソッドを呼ぶ
            _buildingFacilityObj.GetComponentInChildren<ConstructionState>().StartConstruction();
            _UIManager.ChangeUINormal();
        }
        else
        {
            Debug.Log("オブジェクトが重なっている、または工員が足りない");
        }
    }
    /// <summary>
    /// 施設の設置を取り消す関数
    /// </summary>
    public void CancelBuilding()
    {
        _priceBuildingFacilityObj = 0;
        _isBuilding = false;
        Destroy(_buildingFacilityObj);
    }
}