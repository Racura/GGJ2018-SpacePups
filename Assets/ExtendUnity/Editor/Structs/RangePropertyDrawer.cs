using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (RangeInt))]
[CustomPropertyDrawer (typeof (RangeF))]
public class RangePropertyDrawer : PropertyDrawer {

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight;
	}


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

		
		float LabelWidth = 28;

		SerializedProperty minProp = prop.FindPropertyRelative ("min");
		SerializedProperty maxProp = prop.FindPropertyRelative ("max");

		EditorGUI.LabelField (
			new Rect(pos.x, pos.y, EditorGUIUtility.labelWidth, pos.height), label
		);
		
		var x = pos.x + EditorGUIUtility.labelWidth;
		var totalWidth = pos.width - EditorGUIUtility.labelWidth;
		var width = totalWidth * 0.5f;

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
		
		float labelWidthTmp = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = LabelWidth;
		
		EditorGUI.PropertyField(new Rect (x + totalWidth * 0.5f - width, pos.y, width, pos.height), minProp);
		EditorGUI.PropertyField(new Rect (x + totalWidth - width, pos.y, width, pos.height), maxProp);
		
		EditorGUIUtility.labelWidth = labelWidthTmp;

        EditorGUI.indentLevel = indent;
	}
}
