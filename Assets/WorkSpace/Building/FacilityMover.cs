using UnityEngine;
/// <summary>
/// <para>建築中にオブジェクトの状態を伝達する</para>
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
    RaycastHit _hit;
    void Start()
    {
        _parent = transform.parent.gameObject;
        _diffFromParent = transform.localPosition;
        _diffFromParent.y = 0;
        _buildingSpawnManager = FindObjectOfType<BuildingSpawnManager>();
    }
    private void Update()
    {
        //debug用
        Physics.BoxCast(transform.position + Vector3.up * 10, _castBoxSize / 2, -transform.up, out _hit, Quaternion.identity, _buildingSpawnManager._maxRayDistance, LayerMask.GetMask("Facility"));
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
        if (!Physics.BoxCast(transform.position + Vector3.up * 10, _castBoxSize / 2, -transform.up, out _hit,  Quaternion.identity, _buildingSpawnManager._maxRayDistance, LayerMask.GetMask("Facility")))
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _castBoxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_hit.point, _castBoxSize);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
