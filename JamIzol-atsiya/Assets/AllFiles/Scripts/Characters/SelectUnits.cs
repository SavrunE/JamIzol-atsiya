using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnits : MonoBehaviour
{
    private Vector3 point1, point2;
    private int layerMaskDefault = 1 << 0;
    private float height, wight;

    public Rect SelectRect;
    public Texture2D selectTexture;
    public bool DragMouse;

    private ControllerUnits controllerUnits;
    void Start()
    {
        controllerUnits = GetComponent<ControllerUnits>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            point1 = Input.mousePosition;
            Clear();
        }
        if (Input.GetMouseButton(0))
        {
            point2 = Input.mousePosition;
            DragMouse = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            DragMouse = false;
            Select();
        }
    }
    private void OnGUI()
    {
        if (DragMouse)
        {
            wight = point2.x - point1.x;
            height = (Screen.height - point2.y) - (Screen.height - point1.y);
            SelectRect = new Rect(point1.x, Screen.height - point1.y, wight, height);
            GUI.DrawTexture(SelectRect, selectTexture, ScaleMode.StretchToFill, true);
        }
    }
    private void Clear()
    {

    }
    private void Select()
    {

    }
}
