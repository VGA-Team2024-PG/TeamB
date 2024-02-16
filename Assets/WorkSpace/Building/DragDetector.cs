using UnityEngine;
/// <summary>
/// <para>建築中のオブジェクトの状態を伝達する</para>
/// 施設のプレハブにアタッチする
/// </summary>
public class DragDetector : MonoBehaviour
{
    BuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = FindAnyObjectByType<BuildingManager>();
    }
    void OnMouseDrag()
    {
        _buildingManager.IsDragging = true;   
    }
    void OnMouseUp()
    {
        _buildingManager.IsDragging = false;
    }
    void OnTriggerEnter(Collider other)
    {
        _buildingManager.IsPlacable = false;
    }
    void OnTriggerExit(Collider other)
    {
        _buildingManager.IsPlacable = true;
    }
}
