using UnityEngine;
using UnityEngine.EventSystems;

public class NoButtonUI : MonoBehaviour, IPointerClickHandler
{
    //�{�^�����g�p���Ȃ�UI
    [SerializeField] string name;
    public void OnPointerClick(PointerEventData eventData)
    {
        Facility.Instance.AddFacilities(name);
        Debug.Log("�����ꂽ��");
    }
}
