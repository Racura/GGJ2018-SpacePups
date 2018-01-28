using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(OnlyShowIfAttribute))]
public class OnlyShowIfAttributeDrawer : PropertyDrawer {

    private OnlyShowIfAttribute m_attributeValue = null;
    private OnlyShowIfAttribute AttributeValue {
        get {
            if (m_attributeValue == null)
                m_attributeValue = (OnlyShowIfAttribute)attribute;
            return m_attributeValue;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

        var clone = property.Copy ();
        clone.Reset ();

        var first = true;

        while (clone.Next (first))
        {
            if (AttributeValue.IsProperty (clone.name))
            {
                bool show = false;

                switch (clone.propertyType) {
                    case SerializedPropertyType.Boolean:
                        show = clone.boolValue == (bool)AttributeValue.value;
                        break;

                    case SerializedPropertyType.String:
                        show = clone.stringValue == (string)AttributeValue.value;
                        break;

                    case SerializedPropertyType.Float:
                        show = clone.floatValue == (float)AttributeValue.value;
                        break;

                    case SerializedPropertyType.Enum:
                    case SerializedPropertyType.Integer:
                        show = clone.intValue == (int)AttributeValue.value;
                        break;
                }

                if (!show) return 0f;
            }

            first = false;
        }

        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        
        if (position.height <= 0) return;

        EditorGUI.PropertyField (position, property, label);
    }
}