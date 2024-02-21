using UnityEngine;
/// <summary>
/// <para>建築中にオブジェクトの状態を伝達する</para>
/// 施設のプレハブにアタッチする
/// </summary>
public class FacilityMover : MonoBehaviour
{
    BuildingSpawnManager _buildingSpawnManager;
    Vector3 _diffFromParent;
    GameObject _parent;
    void Start()
    {
        _parent = transform.parent.gameObject;
        _diffFromParent = transform.localPosition;
        _diffFromParent.y = 0;
        _buildingSpawnManager = FindObjectOfType<BuildingSpawnManager>();
    }
    /// <summary>
    /// 設置中の施設をドラッグしているかを確認して、動かす
    /// </summary>
    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = RectTransformUtility.ScreenPointToRay(Camera.main, mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _buildingSpawnManager._maxRayDistance , LayerMask.GetMask("Floor")))
        {
            _parent.transform.position = hit.point - _diffFromParent;
        }
    }
    void OnMouseUp()
    {
        if (!Physics.BoxCast(transform.position, transform.localScale / 2, -transform.up, Quaternion.identity, _buildingSpawnManager._maxRayDistance, LayerMask.GetMask("Facility")))
        {
            _buildingSpawnManager.IsPlacable = true;
        }
        else
        {
            _buildingSpawnManager.IsPlacable = false;
        }
    }
}
