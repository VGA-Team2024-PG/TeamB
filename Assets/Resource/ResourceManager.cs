using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// リソースを管理する
/// </summary>
public class ResourceManager : MonoBehaviour
{
    [SerializeField] TMP_Text _resorceText;

    static ResourceManager _instance;
    public static ResourceManager Instance { get => _instance; }
    public Action<ulong> OnResorceChanged;

    ulong _resorce = 0ul;
    float _fraction = 0;
    /// <summary>テキスト反映用リソース量</summary>
    ulong _textResorce = 0;

    public ulong Resorce
    {
        get => _resorce;
        private set
        {
            _resorce = value;
            OnResorceChanged.Invoke(_resorce);
        }
    }

    /// <summary>
    /// リソースを追加する
    /// </summary>
    /// <param name="value"></param>
    public void AddResorce(float value)
    {
        _fraction += value;
        Resorce += (ulong)(_fraction / 1f);
        _fraction %= 1f;
    }

    /// <summary>
    /// リソースを消費する
    /// </summary>
    /// <param name="value"></param>
    public void UseResorce(int value)
    {
        Resorce -= (ulong)value;
    }

    /// <summary>
    /// 現在のリソース量をテキストに反映する
    /// </summary>
    /// <param name="value"></param>
    void ReflectText(ulong value)
    {
        DOTween.To(() => _textResorce,
            x => _textResorce = x,
            _resorce,
            0.5f)
            .SetEase(Ease.Unset)
            .OnUpdate(() => _resorceText.text = _textResorce.ToString());
    }

    private void Awake()
    {
        if (_instance) Destroy(gameObject);
        else _instance = this;

        OnResorceChanged += ReflectText;
    }

    private void Start()
    {
        // 初期値をテキストに反映
        OnResorceChanged.Invoke(Resorce);
    }
}
