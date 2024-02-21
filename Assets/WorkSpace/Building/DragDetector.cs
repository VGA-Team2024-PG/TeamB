using UnityEngine;
/// <summary>
/// <para>建築中にオブジェクトの状態を伝達する</para>
/// 施設のプレハブにアタッチする
/// </summary>
public class DragDetector : MonoBehaviour
{
    BuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = FindObjectOfType<BuildingManager>();
    }
    void OnMouseDrag()
    {
        _buildingManager.IsDragging = true;   
    }
    void OnMouseUp()
    {
        _buildingManager.IsDragging = false;
    }
}
