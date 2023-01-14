using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Character characterOnCell;
    public Character Character { get { return characterOnCell; } }
    [SerializeField] private SpriteRenderer bg;
    
    public void SetColorForBg(int x, int y)
    {
        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 == 1 && y % 2 == 1)){
            bg.color = Color.white;
        }
        else
        {
            bg.color = Color.grey;
        }
    }
}
