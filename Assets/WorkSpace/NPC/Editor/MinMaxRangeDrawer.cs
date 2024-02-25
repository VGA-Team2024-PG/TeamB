using System.ComponentModel.Composition.Hosting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
public class MinMaxRangeDrawer : PropertyDrawer
{
    private const float _kPerfixPaddingRight = 2;
    private const float _kSpacing = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        
        EditorGUI.BeginChangeCheck();

        MinMaxRangeAttribute range = attribute as MinMaxRangeAttribute;
        float minValue = property.vector2Value.x;
        float maxValue = property.vector2Value.y;

        Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        EditorGUI.LabelField(labelPosition, label);

        Rect sliderPosition = new Rect(
            position.x + EditorGUIUtility.labelWidth + _kPerfixPaddingRight + EditorGUIUtility.fieldWidth + _kSpacing,
            position.y,
            position.width - EditorGUIUtility.labelWidth - 2 * (EditorGUIUtility.fieldWidth + _kSpacing) - _kPerfixPaddingRight,
            position.height
        );
        EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, range.min, range.max);

        Rect minPosition = new Rect(position.x + EditorGUIUtility.labelWidth + _kPerfixPaddingRight, position.y,
            EditorGUIUtility.fieldWidth, position.height);
        minValue = EditorGUI.FloatField(minPosition, minValue);
        Rect maxPosition = new Rect(position.xMax - EditorGUIUtility.fieldWidth, position.y,
            EditorGUIUtility.fieldWidth, position.height);
        maxValue = EditorGUI.FloatField(maxPosition, maxValue);

        if (EditorGUI.EndChangeCheck())
        {
            property.vector2Value = new Vector2(minValue, maxValue);
        }

        EditorGUI.EndProperty();
    }
}
