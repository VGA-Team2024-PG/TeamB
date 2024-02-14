using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance { get => _instance; }

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
}
