using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (VectorAngle))]
public class VectorAnglePropertyDrawer : PropertyDrawer {

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		return EditorGUIUtility.singleLineHeight;
	}


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
		
		SerializedProperty angleProp = prop.FindPropertyRelative ("angle");


		EditorGUI.BeginProperty(pos, label, angleProp);
			
		EditorGUI.PropertyField(
			pos,
			angleProp,
			label
		);

		EditorGUI.EndProperty();
	}
}
