using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>施設を建築中に管理する</para>
/// </summary>
public class BuildingManager : MonoBehaviour
{
    /// <summary>
    /// 施設を設置する床
    /// </summary>
    [SerializeField] GameObject _floor;
    void Update()
    {
        
    }
    /// <summary>
    /// 施設の設置をする関数
    /// </summary>
    /// <param name="facilityId">建築する建物のID</param>
    void Build(int facilityId)
    {
        if (!FacilityManager.Instance.IsBuilding)
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = RectTransformUtility.ScreenPointToRay(Camera.main, mousePos);
            RaycastHit hit;
            GameObject facility = Instantiate(FacilityManager.Instance.GetFacilityData(facilityId).Prefab);
            //後で地面のみを読み取るようにLayerMaskを設定
            if (Physics.Raycast(ray, out hit))//, layerMask()))
            {

            }
        }
    }
}
