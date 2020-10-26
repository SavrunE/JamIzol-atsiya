using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

[EditorTool("Castom snap move", typeof(CustomSnap))]
public class CastomSnappingTool : EditorTool
{
    public Texture2D ToolIcon;

    private Transform oldTarget;

    CustomSnapPoint[] allPoint;
    CustomSnapPoint[] targetPoints;

    private void OnEnable()
    {
        Debug.Log("CastomSnappingTool activated");

    }

    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = ToolIcon,
                text = "Custom Snap Move Tool",
                tooltip = "Custom Snap Move Tool - my first tool will be good"
            };
        }
    }
    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((CustomSnap)target).transform;

        if (targetTransform != oldTarget)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(targetTransform.gameObject);

            if (prefabStage != null)
            {
                allPoint = prefabStage.prefabContentsRoot.GetComponentsInChildren<CustomSnapPoint>();
            }
            else
            {
                allPoint = FindObjectsOfType<CustomSnapPoint>();
            }

            targetPoints = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

            oldTarget = targetTransform;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetTransform, "Move with snap tool");
            MoveWithSnapping(targetTransform, newPosition);
        }
    }
    private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
    {


        Vector3 bestPosition = newPosition;
        float closestDistance = float.PositiveInfinity;

        foreach (CustomSnapPoint point in allPoint)
        {
            if (point.transform.parent == targetTransform) continue;
            foreach (CustomSnapPoint ownPoint in targetPoints)
            {
                Vector3 targetPosition = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                float distance = Vector3.Distance(targetPosition, newPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPosition = targetPosition;
                }
            }
        }
        if (closestDistance < 1f)
        {
            targetTransform.position = bestPosition;
        }
        else
        {
            targetTransform.position = newPosition;
        }
    }
}
