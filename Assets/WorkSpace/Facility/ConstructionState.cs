using UnityEngine;

/// <summary>
/// Œšİó‘Ô‚ğŠÇ—‚·‚é
/// </summary>
public class ConstructionState : MonoBehaviour
{
    /// <summary>
    /// {İ‚Ìí—Ş
    /// </summary>
    [SerializeField] FacilityEnum _facilityEnum;
    /// <summary>
    /// Œ»İ‚ÌŒšİó‘Ô
    /// </summary>
    [SerializeField] FacilityState _currentState = FacilityState.NotInstalled;
    /// <summary>
    /// Œšİ‚É•K—v‚È{HŠÔ
    /// </summary>
    [SerializeField] float _workTime = 0;
    DataManager _dataManager;
    Facility _facilityType;
    float _elapsedTime = 0;

    void Start()
    {
        _dataManager = DataManager.Instance;
        _facilityType = _dataManager.GetFacilitydata((int)_facilityEnum);
        _workTime = _facilityType.WorkTime;
    }

    void Update()
    {
        if (_currentState == FacilityState.Constructing)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _workTime)
            {
                _currentState = FacilityState.Working;
            }
        }
    }

    /// <summary>
    /// {İ‚ÌŒšİó‘Ô‚ğŒšİ’†‚É•Ï‚¦‚é
    /// BuildingManager‚ÅŒÄ‚Ô
    /// </summary>
    public void StartConstruction()
    {
        _currentState = FacilityState.Constructing;
    }

    /// <summary>
    /// {İ‚ÌŒšİó‘Ô‚ğŠÇ—‚·‚éenum
    /// </summary>
    public enum FacilityState
    {
        NotInstalled,   // –¢İ’u
        Constructing,   // Œš’z’†
        Working,        // ‰Ò“­’†
    }

    /// <summary>
    /// Œ»İ‚Ì{İ‚ÌŒšİó‘Ô‚ğæ“¾‚·‚é‚½‚ß‚ÌŠÖ”
    /// </summary>
    /// <returns></returns>
    public FacilityState GetFacilityState()
    {
        return _currentState;
    }
}
