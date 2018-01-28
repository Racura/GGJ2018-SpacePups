using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (InsetF))]
public class InsetPropertyDrawer  : PropertyDrawer {

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		return EditorGUIUtility.singleLineHeight * 3
			+ EditorGUIUtility.standardVerticalSpacing * 2;
	}


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

		SerializedProperty topProp		= prop.FindPropertyRelative ("top");
		SerializedProperty bottomProp	= prop.FindPropertyRelative ("bottom");
		SerializedProperty leftProp		= prop.FindPropertyRelative ("left");
		SerializedProperty rightProp	= prop.FindPropertyRelative ("right");

		EditorGUI.LabelField (
			new Rect(pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight), label
		);
		
		float labelWidthTmp = EditorGUIUtility.labelWidth;
		float inset = 44;
        EditorGUIUtility.labelWidth = 40;
        
		
		EditorGUI.PropertyField(GetRect(pos, 0.5f, 0, inset), topProp);
		EditorGUI.PropertyField(GetRect(pos, 0.0f, 1, inset), leftProp);
		EditorGUI.PropertyField(GetRect(pos, 1.0f, 1, inset), rightProp);
		EditorGUI.PropertyField(GetRect(pos, 0.5f, 2, inset), bottomProp);
		
		EditorGUIUtility.labelWidth = labelWidthTmp;
	}

	public Rect GetRect(Rect rect, float column, int row, float inset) {
		var w = (rect.width - inset) * 0.5f;

		return new Rect(
			rect.x + inset + w * column, 
			rect.y + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * row,
			w, 
			EditorGUIUtility.singleLineHeight
		);
	}
}
