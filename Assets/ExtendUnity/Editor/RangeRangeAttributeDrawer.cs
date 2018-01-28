using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(RangeRangeAttribute))]
public class RangeRangeAttributeDrawer : PropertyDrawer {

    private RangeRangeAttribute m_attributeValue = null;
    private RangeRangeAttribute AttributeValue {
        get {
            if (m_attributeValue == null)
                m_attributeValue = (RangeRangeAttribute)attribute;
            return m_attributeValue;
        }
    }

    

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        SerializedProperty value = property;

        var labelWidth = EditorGUIUtility.labelWidth;

        EditorGUI.LabelField (
            new Rect (position.x, position.y, labelWidth, position.height), label
        );

        var rect = new Rect (
            position.x + labelWidth, position.y, 
            position.width - labelWidth, position.height
        );

        if (property.type == typeof(RangeF).Name || property.type == typeof(RangeInt).Name) {

        } else {
            WrongTypeLabel (position, property);
            return;
        }

        var minProperty = property.FindPropertyRelative ("min");
        var maxProperty = property.FindPropertyRelative ("max");

        bool isInt = minProperty.propertyType == SerializedPropertyType.Integer;

        float min, max;

        if (isInt) {
            min = minProperty.intValue;
            max = maxProperty.intValue;
        } else {
            min = minProperty.floatValue;
            max = maxProperty.floatValue;
        }



        MinMaxSlider (
            rect, ref min, ref max, AttributeValue.min, AttributeValue.max
        );

        if (isInt) {
            minProperty.intValue = Mathf.RoundToInt(min);
            maxProperty.intValue = Mathf.RoundToInt(max);
        } else {
            minProperty.floatValue = min;
            maxProperty.floatValue = max;
        }
    }

    public static void MinMaxSlider (Rect position, ref float minValue, ref float maxValue, float minLimit, float maxLimit) {
    
        float fieldWidth = 48;
        float spacingWidth = 4;

        var rect = new Rect (
            position.x + (fieldWidth + spacingWidth), 
            position.y, 
            position.width - (fieldWidth + spacingWidth) * 2, 
            position.height
        );

		int indentTmp = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        minValue = EditorGUI.FloatField (
            new Rect(position.x, position.y, fieldWidth, position.height),
            minValue
        );
        maxValue = EditorGUI.FloatField (
            new Rect(position.x + position.width - fieldWidth, position.y, fieldWidth, position.height),
            maxValue
        );

        EditorGUI.MinMaxSlider (
            rect, ref minValue, ref maxValue, minLimit, maxLimit
        );

        EditorGUI.indentLevel = indentTmp;
    }

    public static void WrongTypeLabel (Rect position, SerializedProperty property) {
    
        EditorGUI.LabelField (
            position, "Wrong type"
        );

    }
}