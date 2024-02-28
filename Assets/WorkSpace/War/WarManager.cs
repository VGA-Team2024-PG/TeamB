using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WarManager : MonoBehaviour
{
    [SerializeField] private GameObject _warPanel;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _defeatText;
    [SerializeField] private TMP_Text _resourceText;
    [SerializeField] private TMP_Text _enemyResourceText;
    [SerializeField] private GameObject _closeButton;
    
    static WarManager _instance;
    public static WarManager Instance { get => _instance; }
    public Queue<GameObject> Soldiers = new Queue<GameObject>();

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
        
        _warPanel.SetActive(false);
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
        StartCoroutine(WarUITransition(true, DataManager.Instance.Resource, DataManager.Instance.EnemyResource, DataManager.Instance.EnemyResource / 2));
        DataManager.Instance.ChangeResource(-(DataManager.Instance.EnemyResource / 2));
        DataManager.Instance.ChangeEnemyResource(1);
    }

    public void Defeat()
    {
        StartCoroutine(WarUITransition(false, DataManager.Instance.Resource, DataManager.Instance.EnemyResource, Soldiers.Count));
        DataManager.Instance.ChangeGold(-10 * (DataManager.Instance.EnemyResource - DataManager.Instance.Resource));
        DataManager.Instance.ChangeResource(-DataManager.Instance.Resource);
    }

    IEnumerator WarUITransition(bool win, int resource, int enemyResource, int decreasingSoldiers)
    {
        _warPanel.SetActive(true);
        _enemyResourceText.text = enemyResource.ToString();
        int countResource = 0;
        _resourceText.text = countResource.ToString();
        
        yield return new WaitForSeconds(1);

        DOTween.To(() => 0,
            x => countResource = x,
            resource,
            2.5f)
            .OnUpdate(() => _resourceText.text = countResource.ToString());

        yield return new WaitForSeconds(3);

        if (win)
        {
            _winText.SetActive(true);
        }
        else
        {
            _defeatText.SetActive(true);
        }

        _closeButton.SetActive(true);

        for (int i = 0; i < decreasingSoldiers; i++)
        {
            Destroy(Soldiers.Dequeue());
        }
    }
}
