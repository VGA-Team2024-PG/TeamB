using UnityEngine;
/// <summary>
/// <para>建築中に施設を動かす</para>
/// 施設のプレハブにアタッチする
/// </summary>
public class FacilityMover : MonoBehaviour
{
    /// <summary>
    /// 重複判定の大きさ
    /// </summary>
    [SerializeField] Vector3 _castBoxSize = Vector3.one;
    BuildingSpawnManager _buildingSpawnManager;
    Vector3 _diffFromParent;
    GameObject _parent;
    void Start()
    {
        _parent = transform.parent.gameObject;
        _diffFromParent = transform.localPosition;
        _diffFromParent.y = 0;
        _buildingSpawnManager = BuildingSpawnManager.Instance;
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
    /// <summary>
    /// ボックスキャストを行い施設が設置可能かを判断する
    /// </summary>
    void OnMouseUp()
    {
        if (!Physics.BoxCast(transform.position + Vector3.up * 10, _castBoxSize / 2, -transform.up,  Quaternion.identity, _buildingSpawnManager._maxRayDistance, LayerMask.GetMask("Facility")))
        {
            _buildingSpawnManager.IsPlacable = true;
        }
        else
        {
            _buildingSpawnManager.IsPlacable = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, _castBoxSize);
    }
}
