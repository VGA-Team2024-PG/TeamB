using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Click : MonoBehaviour, IPointerClickHandler
{
	private int addResource = 1;

	public void TwiceClickRecourcee()
	{
		addResource *= 2;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		ResourceManager.Instance.AddResorce(addResource);
	}
}
