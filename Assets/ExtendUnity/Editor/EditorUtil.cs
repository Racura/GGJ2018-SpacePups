using UnityEngine;
using UnityEditor;
using System.Collections;


public static class EditorUtil {

	public static void DrawRectangle(Transform transform,  Rect rect)
	{
		DrawRectangle (transform, (Vector3)rect.position, rect.size);
	}

	public static void DrawRectangle(Transform transform, Vector3 location, Vector3 size)
	{
		Handles.DrawPolyLine (
			transform.TransformPoint(location + new Vector3(0, 0)),
			transform.TransformPoint(location + new Vector3(size.x, 0)),
			transform.TransformPoint(location + size),
			transform.TransformPoint(location + new Vector3(0, size.y)),
			transform.TransformPoint(location + new Vector3(0, 0))
		);
	}
}
