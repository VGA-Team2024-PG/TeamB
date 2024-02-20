using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance { get => _instance; }

    private int _gold;
    private int _resource;
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

    public void ChangeGold(int value)//ƒS[ƒ‹ƒh‚ğ‘‚â‚·
    {
        _gold += value;
    }

    public void ChangeResource(int value)//•º—Í‚ğ‘‚â‚·
    {
        _resource += value;
    }
}
