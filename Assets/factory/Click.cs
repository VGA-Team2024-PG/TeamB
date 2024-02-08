using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Click : MonoBehaviour, IPointerClickHandler
{
	private int addResource = 1;

	/// <summary>
	/// クリックで増えるリソースを２倍にする
	/// </summary>
	/// <returns></returns>
	public void TwiceClickRecourcee()
	{
		addResource *= 2;
	}
	/// <summary>
	/// クリックされたときにリソースを増やす
	/// </summary>
	/// <param name="eventData"></param>
	/// <returns></returns>
	public void OnPointerClick(PointerEventData eventData)
	{
		ResourceManager.Instance.AddResorce(addResource);
	}
}
