using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConstructionManager : MonoBehaviour
{
    /// <summary>
    /// 施設の設置
    /// </summary>
    /// <param name="buildingIndex">建築する建物のインデックス</param>
    void Build(int buildingIndex)
    {
        if (!BuildingsManager.Instance.IsBuilding)
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = RectTransformUtility.ScreenPointToRay(Camera.main, mousePos);
            RaycastHit hit;
            GameObject building = Instantiate(BuildingsManager.Instance.BuildingsList[buildingIndex].Prefab);
            if (Physics.Raycast(ray, out hit))//, layerMask()))
            {

            }
        }
    }
}
