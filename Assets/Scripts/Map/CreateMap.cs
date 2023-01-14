using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class CreateMap : MonoBehaviour
{
    [SerializeField]
    private Cell cellPrefab;

    [SerializeField] private Vector2 mapSize;
    [SerializeField] private float cellSize;

    private List<Cell> cells;
    public void Create()
    {
        Clear();
        cells = new List<Cell>();
        var halfWidth = mapSize.x / 2;
        var halfHeight = mapSize.y / 2;
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {

                var cell = Instantiate(cellPrefab, this.transform);
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                cell.transform.localPosition = new Vector3((i - halfWidth)*cellSize, (j - halfHeight) * cellSize, 0);
                cell.SetColorForBg(i, j);
                cells.Add(cell);

            }
        }
    }
    private void Clear()
    {
        if (cells == null || cells.Count == 0) return;
        cells.ForEach(x =>
        {
            Destroy(x.gameObject);
        });
        cells = null;
    }
    private void OnDestroy()
    {
        Clear();
    }
}
