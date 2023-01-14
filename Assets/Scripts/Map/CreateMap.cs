using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class CreateMap : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private int MapSizeWidth;
    [SerializeField] private int MapSizeHeight;
    [SerializeField] private float cellSize;
    
    private Camera mainCamera;
    private Cell[,] tables;
    private Vector2 centerMap;
    public Cell[,] Tables => tables;
    public Vector2 Central => centerMap;
    public void Create()
    {
        Clear();
        tables = new Cell[MapSizeWidth, MapSizeHeight];
        for (int i = 0; i < MapSizeWidth; i++)
        {
            for (int j = 0; j < MapSizeHeight; j++)
            {
                var cell = Instantiate(cellPrefab, this.transform);
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                cell.transform.localPosition = new Vector3(i * cellSize, j * cellSize, 0);
                cell.SetColorForBg(i, j);
                tables[i, j] = cell;
            }
        }
        SetMainCameraPosition();
    }
    private void Clear()
    {
        if (tables == null || tables.Length == 0) return;
        for (int i = 0; i < MapSizeWidth; i++)
        {
            for (int j = 0; j < MapSizeHeight; j++)
            {
                Destroy(tables[i, j].gameObject);
            }
        }
        tables = null;
    }
    private void SetMainCameraPosition()
    {
        mainCamera = Camera.main;
        Vector3 cameraPos = new Vector3((MapSizeWidth / 2 - 0.5f), (MapSizeHeight / 2-0.5f), -1);
        cameraPos *= cellSize;
        centerMap = cameraPos;
        mainCamera.transform.position = cameraPos;
    }
    private void OnDestroy()
    {
        Clear();
    }
}
