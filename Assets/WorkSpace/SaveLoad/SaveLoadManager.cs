using System;
using UnityEngine;
using System.IO;

/// <summary>
/// ゲームデータのセーブとロードを管理する
/// </summary>
public class SaveLoadManager : MonoBehaviour
{
    [SerializeField, Tooltip("初期データ")] private SaveGameData _initialData;
    /// <summary> データをセーブするファイルのパス </summary>
    private string _saveDataFilePath;

    /// <summary> ゲーム起動時にゲームデータを取得する </summary>
    public SaveGameData GetInitialData()
    {
        // ファイルパスの初期セット
        _saveDataFilePath = Application.dataPath + "/SaveGameDataJson.json";
        
        if (!File.Exists(_saveDataFilePath)) // ファイルが存在していなかったら初期状態をセーブする
        {
            _initialData._facilityCount = new int[Enum.GetValues(typeof(FacilityEnum)).Length];
            // 施設データから初期のストックを取得する
            _initialData._facilityStock = DataManager.Instance.Facilitystock;

            // 現存する施設だけストックを減らす
            foreach (FacilitySaveData dataFacilitySaveData in _initialData._facilitySaveDatas)
            {
                _initialData._facilityStock[(int)dataFacilitySaveData.FacilityEnum]--;
            }
            
            SaveData(_initialData);
        }

        // セーブデータのロード
        return LoadData(_saveDataFilePath);
    }
    
    /// <summary> データを保存する </summary>
    public void SaveData(SaveGameData saveGameData)
    {
        // json型のstringに変更
        string json = JsonUtility.ToJson(saveGameData);
        // 書き込み用にファイルを開く
        StreamWriter writer = new StreamWriter(_saveDataFilePath, false);
        writer.WriteLine(json);
        writer.Close();   
    }

    /// <summary> ファイルパスのデータをロードする </summary>
    /// <returns> ロードされたデータ </returns>
    SaveGameData LoadData(string saveDataFilePath)
    {
        StreamReader reader = new StreamReader(saveDataFilePath);
        string jsonData = reader.ReadToEnd();
        reader.Close();
                                                                
        return JsonUtility.FromJson<SaveGameData>(jsonData);    
    }
}

/// <summary>
/// セーブするゲームデータの型
/// </summary>
[Serializable]
public class SaveGameData
{
    public int _gold;
    public int _resource;
    public int _enemyResource;
    public int[] _facilityCount;
    public int[] _facilityStock;
    public FacilitySaveData[] _facilitySaveDatas;

    public SaveGameData(int gold, int resource, int enemyResource, int[] facilityCount, int[] facilityStock, FacilitySaveData[] facilitySaveDatas)
    {
        _gold = gold;
        _resource = resource;
        _enemyResource = enemyResource;
        _facilityCount = facilityCount;
        _facilityStock = facilityStock;
        _facilitySaveDatas = facilitySaveDatas;
    }
}

/// <summary>
/// セーブする施設データの型
/// </summary>
[Serializable]
public class FacilitySaveData
{
    public FacilityEnum FacilityEnum;
    /// <summary> 施設のポジション </summary>
    public Vector3 Position;
    public FacilityState FacilityState = FacilityState.NotInstalled;
    public float BuildingTime = 0;
    public int MineStorage = 0;

    public FacilitySaveData(FacilityEnum facilityEnum, Vector3 pos, FacilityState state, float buildingTime)
    {
        FacilityEnum = facilityEnum;
        Position = pos;
        FacilityState = state;
        BuildingTime = buildingTime;
    }

    public FacilitySaveData(FacilityEnum fEnum, Vector3 pos)
    {
        FacilityEnum = fEnum;
        Position = pos;
    }
}