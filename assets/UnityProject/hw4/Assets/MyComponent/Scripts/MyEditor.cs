using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Disk))]
public class MyEditor : Editor
{ 
	public override void OnInspectorGUI()
	{
		var target = (Disk)(serializedObject.targetObject);
		target.speed = EditorGUILayout.FloatField("SPEED",target.speed);
		target.size = EditorGUILayout.Vector3Field ("SIZE",target.size);
		target.position = EditorGUILayout.Vector3Field ("POSITION",target.position);
		target.direction = EditorGUILayout.Vector3Field ("DIRECTION",target.direction);
		target.color = EditorGUILayout.ColorField("COLOR",target.color);

	}

}