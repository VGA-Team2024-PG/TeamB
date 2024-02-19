using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="FacilityDataBase")]
public class FacilityDataBase : ScriptableObject
{
    [SerializeField] List<Facility> _facilityData;
    public List<Facility> FacilityData => _facilityData;
}
