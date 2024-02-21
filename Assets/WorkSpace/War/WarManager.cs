using System;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    static WarManager _instance;
    public static WarManager Instance { get => _instance; }

    public event Action OnWin;
    public event Action OnDefeat;

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

        OnWin += Win;
        OnDefeat += Defeat;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWar()
    {
        if (DataManager.Instance.Resource > DataManager.Instance.EnemyResource)
        {
            OnWin?.Invoke();
        }
        else
        {
            OnDefeat?.Invoke();
        }
    }

    public void Win()
    {
        DataManager.Instance.ChangeResource(-(DataManager.Instance.EnemyResource / 2));
        DataManager.Instance.ChangeEnemyResource(1);
    }

    public void Defeat()
    {
        DataManager.Instance.ChangeGold(-10 * (DataManager.Instance.EnemyResource - DataManager.Instance.Resource));
        DataManager.Instance.ChangeResource(-DataManager.Instance.Resource);
    }
}
