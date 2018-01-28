using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(SelectorAttribute))]
public class SelectorAttributeDrawer : PropertyDrawer {

    private SelectorAttribute m_attributeValue = null;
    private SelectorAttribute AttributeValue {
        get {
            if (m_attributeValue == null)
                m_attributeValue = (SelectorAttribute)attribute;
            return m_attributeValue;
        }
    }

    

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        SerializedProperty value = property;

        var width = EditorGUIUtility.labelWidth;
        var padding = 12;

        EditorGUI.LabelField (
            new Rect (position.x, position.y, width, position.height), label
        );

        var rect = new Rect (
            width + padding, position.y, 
            position.width - (width + padding), position.height
        );

        int currentIndex = -1;

        switch(property.propertyType) {
            case SerializedPropertyType.Integer:
            case SerializedPropertyType.String:
            case SerializedPropertyType.Float:
                break;
            default:
                WrongTypeLabel (rect, property);
                return;
        }

        currentIndex = EditorGUI.Popup (rect, currentIndex, AttributeValue.keys);


        switch(property.propertyType) {
            case SerializedPropertyType.Integer:
                property.intValue = (int)AttributeValue.values[currentIndex];
                break;
            case SerializedPropertyType.Float:
                property.floatValue = (float)AttributeValue.values[currentIndex];
                break;
            case SerializedPropertyType.String:
                break;
        }
    }

    public static void WrongTypeLabel (Rect position, SerializedProperty property) {
    
        EditorGUI.LabelField (
            position, "Wrong type"
        );
    }
}