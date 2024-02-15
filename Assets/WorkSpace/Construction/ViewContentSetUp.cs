using UnityEngine;
using UnityEngine.UI;

public class ViewContentSetUp : MonoBehaviour
{
    /// <summary>
    /// ScrollView内のボタンを表示するcontent
    /// </summary>
    [SerializeField] RectTransform _content;
    /// <summary>
    /// 表示するボタンのプレハブ
    /// </summary>
    [SerializeField] GameObject _buttonPrefab;
    BuildingsManager _buildingsManager;
    void Awake()
    {
        _buildingsManager = FindObjectOfType<BuildingsManager>();
    }
    void Start()
    {
        Vector2 prefabSize = _buttonPrefab.GetComponent<RectTransform>().sizeDelta;
        int buttonKinds = _buildingsManager.BuildingsList.Count;
        float layoutSpacing = _content.GetComponent<VerticalLayoutGroup>().spacing;
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, prefabSize.y * buttonKinds + layoutSpacing * (buttonKinds - 1));
        for(int i = 0; i < buttonKinds; i++)
        {
            GameObject button = Instantiate(_buttonPrefab, _content);
            //ボタンのテキストを変える処理
            //ボタンプレハブ内の関数でテキストを変える処理を作る
        }
    }
    void Update()
    {
        
    }
}
