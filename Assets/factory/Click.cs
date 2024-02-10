using System;
using Kawaguthi;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;


/// <summary>クリックのクラス</summary>
public class Click : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private Ease _pointUpEase;
	[SerializeField] private Ease _clickEase;
	private int _clickAddResource = 1;

	private Vector3 _transform;

	private void Start()
	{
		_transform = transform.localScale;
	}

	public int ClickTwoTime(string name)
	{
		_clickAddResource *= ItemManager.Instance.ItemDatas[name].Time;
		return _clickAddResource;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		transform.DOScale(_transform, 0.15f).SetEase(_clickEase);
	}

	/// <summary>
	/// クリックされたときにリソースを増やす
	/// </summary>
	/// <param name="eventData"></param>
	/// <returns></returns>
	public void OnPointerUp(PointerEventData eventData)
	{
		ResourceManager.Instance.AddResorce(_clickAddResource);
		transform.DOScale(_transform * 1.2f, 0.15f).SetEase(_clickEase);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		transform.DOScale(_transform * 1.2f, 0.5f).SetEase(_pointUpEase);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		transform.DOScale(_transform , 0.5f).SetEase(_pointUpEase);
	}
}