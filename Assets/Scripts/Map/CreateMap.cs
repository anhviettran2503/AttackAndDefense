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
    private Cell[,] cells;
    private Vector2 centerMap;
    public Cell[,] Cells => cells;
    public Vector2 Central => centerMap;
    public void Create()
    {
        Clear();
        cells = new Cell[MapSizeWidth, MapSizeHeight];
        for (int i = 0; i < MapSizeWidth; i++)
        {
            for (int j = 0; j < MapSizeHeight; j++)
            {
                var cell = Instantiate(cellPrefab, this.transform);
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                cell.transform.localPosition = new Vector3(i * cellSize, j * cellSize, 0);
                cell.SetColorForBg(i, j);
                cells[i, j] = cell;
            }
        }
        SetMainCameraPosition();
    }
    private void Clear()
    {
        if (cells == null || cells.Length == 0) return;
        for (int i = 0; i < MapSizeWidth; i++)
        {
            for (int j = 0; j < MapSizeHeight; j++)
            {
                Destroy(cells[i, j].gameObject);
            }
        }
        cells = null;
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
