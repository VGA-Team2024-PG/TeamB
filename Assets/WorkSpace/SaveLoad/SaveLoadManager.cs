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

    private void Awake()
    {
        _initialData = GetInitialData();
    }

    /// <summary> ゲーム起動時にゲームデータを取得する </summary>
    SaveGameData GetInitialData()
    {
        _saveDataFilePath = Application.dataPath + "/SaveGameDataJson.json";
        
        if (!File.Exists(_saveDataFilePath)) // ファイルが存在していなかったら初期状態をセーブする
        {
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
    public FacilitySaveData[] _facilitySaveDatas;

    public SaveGameData(int gold, int resource, int enemyResource, FacilitySaveData[] facilitySaveDatas)
    {
        _gold = gold;
        _resource = resource;
        _enemyResource = enemyResource;
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
    public Vector3 Position = Vector3.zero;
    public ConstructionState.FacilityState FacilityState = ConstructionState.FacilityState.NotInstalled;
    public float BuildingTime = 0;
    public int MineStorage = 0;

    public FacilitySaveData(FacilityEnum facilityEnum)
    {
        FacilityEnum = facilityEnum;
    }
}