using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// <para>建築タブのアクティブを切り替える</para>
/// メインキャンバスの建築ボタンにアタッチする
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UIの推移を管理するステート
    /// </summary>
    public enum UIState
    {
        Normal,
        ChoiceFacility,
        Building
    }
}