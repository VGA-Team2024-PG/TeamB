using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance { get => _instance; }

    [SerializeField] FacilityDataBase _facilitydata;
    private int _gold;
    private int _resource;
    private int[] _facilitycount = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
    private int[] _facilitystock = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
    public event Action<int> Onchangegold;
    public event Action<int> Onchangeresource;

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

    private void Start()
    {
        for (int i = 0;  i < _facilitydata.FacilityData.Count; i++)
        {
            _facilitystock[i] = _facilitydata.FacilityData[i].FacilityStock;
        }
    }

    public int Gold
    {
        get => _gold;

        private set
        {
            _gold = value;
            Onchangegold?.Invoke(_gold);
        }
    }

    public int Resource
    {
        get => _resource;

        private set
        {
            _resource = value;
            Onchangeresource?.Invoke(_resource);
        }
    }

    public int[] FacilityCount => _facilitycount;
    public int[] Facilitystock => _facilitystock;

    public void ChangeGold(int value)
    {
        Gold += value;
    }

    public void ChangeResource(int value)
    {
        Resource += value;
    }

    public void AddFacility(FacilityEnum facilityEnum)
    {
        _facilitycount[(int)facilityEnum]++;
    }

    /// <summary>
    /// ストックを減らす処理
    /// </summary>
    /// <param name="facilityEnum"></param>
    public void DecreaseFacilityStock(FacilityEnum facilityEnum)
    {
        _facilitystock[(int)facilityEnum]--;
    }


    /// <summary>
    /// FacilityDataBase内のリストから引数をインデックスとしてデータを返す
    /// </summary>
    public Facility GetFacilitydata(int index)
    {
        return _facilitydata.FacilityData[index];
    }
}
