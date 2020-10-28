#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UnitSelect))]

public class UnitSelectEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		UnitSelect e = (UnitSelect)target;
		GUILayout.Label("Build Icons:", EditorStyles.boldLabel);
		if (GUILayout.Button("Create / Update"))
		{
			e.BuildGrid();
		}
	}
}
#endif