using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIManager : MonoBehaviour
{
    BuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = FindObjectOfType<BuildingManager>();
    }
    void Update()
    {
        
    }
}
