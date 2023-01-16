using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropCamera : MonoBehaviour
{
    private Camera camera;
    private Vector3 defaultPos;
    private Vector3 origin;
    private Vector3 difference;
    private bool drag;
    [SerializeField] private Vector2 clampPos;
    private void Start()
    {
        camera = Camera.main;
        drag = false;
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            difference = camera.ScreenToWorldPoint(Input.mousePosition) - camera.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }
        if (drag)
        {
            camera.transform.position = origin - difference;
        }
        var pos = camera.transform.position;
        pos.x = Mathf.Clamp(pos.x, clampPos.x, clampPos.y);
        pos.y = Mathf.Clamp(pos.y, clampPos.x, clampPos.y);
        camera.transform.position = pos;
    }
}
