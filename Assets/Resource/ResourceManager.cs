using System;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] TMP_Text _resorceText;

    static ResourceManager _instance;
    public static ResourceManager Instance { get => _instance; }
    public Action<ulong> OnResorceChanged;

    ulong _resorce = 0ul;

    public ulong Resorce
    {
        get => _resorce;
        private set
        {
            _resorce = value;
            OnResorceChanged.Invoke(_resorce);
        }
    }

    public void AddResorce(int value)
    {
        Resorce += (ulong)value;
    }

    public void UseResorce(int value)
    {
        Resorce -= (ulong)value;
    }

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
        OnResorceChanged.Invoke(Resorce);
    }
}
