using UnityEngine;
/// <summary>
/// <para>建築中に施設を動かす</para>
/// 施設のプレハブにアタッチする
/// </summary>
public class FacilityMover : MonoBehaviour
{
    [SerializeField] private Vector3 _castBoxCenter;
    [SerializeField] Vector3 _castBoxSize = Vector3.one;
    [SerializeField] private LayerMask _castLayerMask;
    [SerializeField] private LayerMask _floorLayerMask;
    BuildingSpawnManager _buildingSpawnManager;
    private ConstructionState _constructionState;
    
    void Start()
    {
        _buildingSpawnManager = BuildingSpawnManager.Instance;
        _constructionState = GetComponent<ConstructionState>();
    }
    /// <summary>
    /// 設置中の施設をドラッグしているかを確認して、動かす
    /// </summary>
    void OnMouseDrag()
    {
        if (_constructionState.GetFacilityState() != FacilityState.NotInstalled)
        {
            Destroy(this);
            return;
        }
        
        Vector3 mousePos = Input.mousePosition;
        Ray ray = RectTransformUtility.ScreenPointToRay(Camera.main, mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _buildingSpawnManager._maxRayDistance , _floorLayerMask))
        {
            transform.position = hit.point;
        }
    }
    /// <summary>
    /// ボックスキャストを行い施設が設置可能かを判断する
    /// </summary>
    void OnMouseUp()
    {
        _buildingSpawnManager.IsPlacable = !Physics.CheckBox(transform.position + _castBoxCenter, _castBoxSize * 0.5f, Quaternion.identity, _castLayerMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + _castBoxCenter, _castBoxSize);
    }
}
