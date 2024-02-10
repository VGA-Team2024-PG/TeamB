using System;
using TMPro;
using UnityEngine;

/// <summary>
/// ���\�[�X���Ǘ�����
/// </summary>
public class ResourceManager : MonoBehaviour
{
    [SerializeField] TMP_Text _resorceText;

    static ResourceManager _instance;
    public static ResourceManager Instance { get => _instance; }
    public Action<ulong> OnResorceChanged;

    ulong _resorce = 0ul;
    float _fraction = 0;

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
    /// ���\�[�X��ǉ�����
    /// </summary>
    /// <param name="value"></param>
    public void AddResorce(float value)
    {
        _fraction += value;
        Resorce += (ulong)(_fraction / 1f);
        _fraction %= 1f;
    }

    /// <summary>
    /// ���\�[�X�������
    /// </summary>
    /// <param name="value"></param>
    public void UseResorce(int value)
    {
        Resorce -= (ulong)value;
    }

    /// <summary>
    /// ���݂̃��\�[�X�ʂ��e�L�X�g�ɔ��f����
    /// </summary>
    /// <param name="value"></param>
    void ReflectText(ulong value)
    {
        _resorceText.text = Resorce.ToString();
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
}
