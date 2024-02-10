using UnityEngine;
using UnityEngine.EventSystems;

public class NoButtonUI : MonoBehaviour, IPointerClickHandler
{
    //ボタンを使用しないUI
    [SerializeField] string name;
    public void OnPointerClick(PointerEventData eventData)
    {
        Facility.Instance.AddFacilities(name);
        Debug.Log("押されたよ");
    }
}
