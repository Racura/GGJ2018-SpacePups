using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (Coord2d))]
public class Coord2dPropertyDrawer  : PropertyDrawer {

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight * 1;
	}


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {


		//x + (x + z) * 2 = y
		//y/2 - x/2 = x + z
		//y/2 - z = 1.5x



		
		float labelWidthTmp = EditorGUIUtility.labelWidth;
		float propWidth = (pos.width - labelWidthTmp) * 0.5f;


		SerializedProperty xProp = prop.FindPropertyRelative ("x");
		SerializedProperty yProp = prop.FindPropertyRelative ("y");

		EditorGUI.LabelField (
			new Rect(pos.x, pos.y, labelWidthTmp, EditorGUIUtility.singleLineHeight),
      		label
		);
		
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.labelWidth = (int)Mathf.Max(4, propWidth * 0.2f);
		
		EditorGUI.PropertyField(new Rect (pos.x + labelWidthTmp + propWidth * 0, pos.y, propWidth, pos.height), xProp);
		EditorGUI.PropertyField(new Rect (pos.x + labelWidthTmp + propWidth * 1, pos.y, propWidth, pos.height), yProp);
		
		EditorGUI.indentLevel = indent;
		EditorGUIUtility.labelWidth = labelWidthTmp;
	}
}
