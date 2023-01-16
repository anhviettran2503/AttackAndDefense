using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOutCamera : MonoBehaviour
{
    public Vector2 clamp;
    public float smooth;
    public float zoomChange;
    private Camera camera;
    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            camera.orthographicSize -= zoomChange * smooth * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            camera.orthographicSize += zoomChange * smooth * Time.deltaTime;
        }
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, clamp.x, clamp.y);
    }
}
