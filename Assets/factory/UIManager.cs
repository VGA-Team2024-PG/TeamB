using System;
using System.Collections.Generic;
using System.Linq;
using Kawaguthi;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace factory
{
	[RequireComponent(typeof(ItemManager))]
	[RequireComponent(typeof(Facility))]
	public class UIManager : MonoBehaviour
	{
		private static UIManager instance;
		public static UIManager Instance => instance;

		[SerializeField] private TextMeshProUGUI[] _factoryTexts;
		[SerializeField, Header("施設を買うボタン")] private Button[] _facilityButton;
		[SerializeField, Header("アイテムを買うボタン")] private Button[] _itemButton;
		[SerializeField] private BaseFacilityData baseFacilityData;

		private Click _click;


		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			_click = FindObjectOfType<Click>();
			ResourceManager.Instance.OnResorceChanged += PossibleButton;
			ReflectStartText();
		}

		///<summary>テキストの初期化</summary>
		void ReflectStartText()
		{
			int i = 0;
			foreach (var factory in baseFacilityData.FacilityDatas)
			{
				_facilityButton[i].GetComponentInChildren<TextMeshProUGUI>().text =
					$"{factory.FacilityName} {factory.Prime}";
				_factoryTexts[i].text = $"{factory.FacilityName}";
				i++;
			}

			_itemButton[0].GetComponentInChildren<TextMeshProUGUI>().text = "クリック " + 100;
		}

		///<summary>施設を買ったらテキストを書き換える</summary>
		public void ReflectShop(string name)
		{
			foreach (var text in _factoryTexts.Where(x => x.text.Contains(name)))
			{
				text.gameObject.SetActive(true);
				text.text = $"{name} {Facility.Instance.BuyedFacilities[name].facilityLevel}";
#if UNITY_EDITOR
				Debug.Log($"{name}施設レベル{Facility.Instance.BuyedFacilities[name].facilityLevel}");
				Debug.Log($"{name}合計生産量" +
				          Facility.Instance.BuyedFacilities[name].resourceValue *
				          Facility.Instance.BuyedFacilities[name].facilityLevel);
				Debug.Log($"{name} 価格{Facility.Instance.BuyedFacilities[name].prime}");
#endif
			}

			foreach (var button in _facilityButton.Where(x =>
				         x.GetComponentInChildren<TextMeshProUGUI>().text.Contains(name)))
			{
				button.GetComponentInChildren<TextMeshProUGUI>().text =
					$"{name} {Mathf.Ceil(Facility.Instance.BuyedFacilities[name].prime)}";
			}
		}

		///<summary>ボタンが押せるかどうか</summary>
		void PossibleButton(long value)
		{
			for (int i = 0; i < baseFacilityData.FacilityDatas.Count; i++)
			{
				if (!Facility.Instance.BuyedFacilities.Keys.Contains(baseFacilityData.FacilityDatas[i].FacilityName))
				{
					if (ResourceManager.Instance.Resorce >= (long)Mathf.Ceil(baseFacilityData.FacilityDatas[i].Prime))
						_facilityButton[i].interactable = true;
					else
						_facilityButton[i].interactable = false;
				}
				else
				{
					if (ResourceManager.Instance.Resorce >= (long)Mathf.Ceil(Facility.Instance
						    .BuyedFacilities[baseFacilityData.FacilityDatas[i].FacilityName].prime))
						_facilityButton[i].interactable = true;
					else
						_facilityButton[i].interactable = false;
				}
			}

			int j = 0;
			foreach (var ItemValue in ItemManager.Instance.ItemDatas.Values)
			{
				if (ResourceManager.Instance.Resorce >= (long)Mathf.Ceil(ItemValue.Prime))
					_itemButton[j].interactable = true;
				else
					_itemButton[j].interactable = false;
				j++;
			}
		}


	}
}