using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ���\�[�X���Ǘ�����
/// </summary>
public class ResourceManager : MonoBehaviour
{
    [SerializeField] TMP_Text _resorceText;

    static ResourceManager _instance;
    public static ResourceManager Instance { get => _instance; }
    public Action<long> OnResorceChanged;

    long _resorce = 0l;
    float _fraction = 0;
    /// <summary>�e�L�X�g���f�p���\�[�X��</summary>
    long _textResorce = 0;

    public long Resorce
    {
        get => _resorce;
        private set
        {
            _resorce = value;

            if (_resorce < 0)
            {
                _resorce = 0;
            }

            OnResorceChanged.Invoke(_resorce);
        }
    }

    /// <summary>
    /// ���\�[�X��ǉ�����
    /// </summary>
    /// <param name="value"></param>
    public void AddResorce(float value)
    {
        _fraction += value;
        Resorce += (long)(_fraction / 1f);
        _fraction %= 1f;
    }

    /// <summary>
    /// ���\�[�X�������
    /// </summary>
    /// <param name="value"></param>
    public void UseResorce(int value)
    {
        Resorce -= (long)value;
    }

    /// <summary>
    /// ���݂̃��\�[�X�ʂ��e�L�X�g�ɔ��f����
    /// </summary>
    /// <param name="value"></param>
    void ReflectText(long value)
    {
        DOTween.To(() => _textResorce,
            x => _textResorce = x,
            (long)_resorce,
            0.5f)
            .SetEase(Ease.Linear)
            .OnUpdate(() => _resorceText.text = _textResorce.ToString());
        Debug.Log(_resorce);
    }

    private void Awake()
    {
        if (_instance) Destroy(gameObject);
        else _instance = this;

        OnResorceChanged += ReflectText;
    }

    private void Start()
    {
        // �����l���e�L�X�g�ɔ��f
        OnResorceChanged.Invoke(Resorce);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddResorce(10);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            UseResorce(10);
        }
    }
}
