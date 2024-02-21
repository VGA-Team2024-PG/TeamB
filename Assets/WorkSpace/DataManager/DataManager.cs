using System;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance { get => _instance; }

    [SerializeField] FacilityDataBase _facilitydata;
    [SerializeField] TMP_Text _goldText;
    [SerializeField] TMP_Text _resourceText;
    private int _gold;
    private int _resource;
    private int _enemyresource;
    private int[] _facilitycount = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
    private int[] _facilitystock = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
    public event Action<int> Onchangegold;
    public event Action<int> Onchangeresource;
    public event Action<int> Onchangeenemyresource;

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

        Onchangegold += n => _goldText.text = n.ToString();
        Onchangeresource += n => _resourceText.text = n.ToString();
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

    public int EnemyResource
    {
        get => _enemyresource;

        private set
        {
            _enemyresource = value;
            Onchangeenemyresource?.Invoke(_enemyresource);
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

    public void ChangeEnemyResource(int value)
    {
        EnemyResource += value;
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
