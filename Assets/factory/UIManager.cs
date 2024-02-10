using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace factory
{
	public class UIManager : MonoBehaviour
	{
		private static UIManager instance;
		public static UIManager Instance => instance;

		[SerializeField] private TextMeshProUGUI[] _factoryTexts;
		[SerializeField, Header("施設を買うボタン")] private Button[] _facilityButton;
		[SerializeField, Header("アイテムを買うボタン")] private Button[] _itemButton;
		[SerializeField] private BaseFactoryData _baseFactoryData;


		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			ReflectStartText();
		}

		private void Update()
		{
			PossibleButton();
		}

		///<summary>テキストの初期化</summary>
		void ReflectStartText()
		{
			int i = 0;
			foreach (var factory in _baseFactoryData.FactoryDatas)
			{
				_facilityButton[i].GetComponentInChildren<TextMeshProUGUI>().text =
					$"{factory.FactoryName} {factory.Prime}";
				_factoryTexts[i].text = $"{factory.FactoryName}";
				i++;
			}
		}

		///<summary>テキストを書き換える</summary>
		public void ReflectShop(string name)
		{
			foreach (var text in _factoryTexts.Where(x => x.text.Contains(name)))
			{
				text.gameObject.SetActive(true);
				text.text = $"{name} {Facility.Instance.BuyedFactories[name].factoryLevel}";
#if UNITY_EDITOR
				Debug.Log($"{name}施設レベル{Facility.Instance.BuyedFactories[name].factoryLevel}");
				Debug.Log($"{name}合計生産量" +
				          Facility.Instance.BuyedFactories[name].resourceValue *
				          Facility.Instance.BuyedFactories[name].factoryLevel);
				Debug.Log($"{name} 価格{Facility.Instance.BuyedFactories[name].prime}");
#endif
			}

			foreach (var button in _facilityButton.Where(x =>
				         x.GetComponentInChildren<TextMeshProUGUI>().text.Contains(name)))
			{
				button.GetComponentInChildren<TextMeshProUGUI>().text =
					$"{name} {Mathf.Ceil(Facility.Instance.BuyedFactories[name].prime)}";
			}
		}

		///<summary>ボタンが押せるかどうか</summary>
		void PossibleButton()
		{
			for (int i = 0; i < _baseFactoryData.FactoryDatas.Count; i++)
			{
				if (!Facility.Instance.BuyedFactories.Keys.Contains(_baseFactoryData.FactoryDatas[i].FactoryName))
				{
					if (ResourceManager.Instance.Resorce >= (ulong)Mathf.Ceil(_baseFactoryData.FactoryDatas[i].Prime))
					{
						_facilityButton[i].interactable = true;
					}
					else
					{
						_facilityButton[i].interactable = false;
					}
				}
				else
				{
					if (ResourceManager.Instance.Resorce >= (ulong)Mathf.Ceil(Facility.Instance
						    .BuyedFactories[_baseFactoryData.FactoryDatas[i].FactoryName].prime))
					{
						_facilityButton[i].interactable = true;
					}
					else
					{
						_facilityButton[i].interactable = false;
					}
				}
			}
		}
	}
}