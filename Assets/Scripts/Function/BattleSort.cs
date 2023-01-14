using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using NaughtyAttributes;
using static Unity.Collections.AllocatorManager;

public class BattleSort : MonoBehaviour
{

    private Cell[,] cellMap;

    private int centerX, centerY;
    public void PreparingBattle(Cell[,] _cellMap, List<Character> _characters)
    {
        cellMap = _cellMap;
        centerX = (int)(cellMap.GetLongLength(0) / 2);
        centerY = (int)(cellMap.GetLongLength(1) / 2);
        List<Character> attackers = new List<Character>();
        List<Character> defenders = new List<Character>();
        _characters.ForEach(x =>
        {
            if (x.CanMove)
            {
                attackers.Add(x);
            }
            else
            {
                defenders.Add(x);
            }
        });
        AttackerSort(attackers);
        DefenderSort(defenders);
    }
    private void DefenderSort(List<Character> defenders)
    {
        Vector4 block = new Vector4(centerX - 2, centerX + 2, centerY - 2, centerY + 2);
        int charIndex = 0;
        for (int i = (int)block.x; i < block.y; i++)
        {
            for (int j = (int)block.z; j < block.w; j++)
            {
                defenders[charIndex].SetPosition(cellMap[i, j], i, j);
                charIndex += 1;
            }
        }
    }
    private void AttackerSort(List<Character> attackers)
    {

        List<Vector4> blocks = new List<Vector4>();
        blocks.Add(new Vector4(centerX - 5, centerX - 3, centerY - 2, centerY + 2));
        blocks.Add(new Vector4(centerX + 3, centerX + 5, centerY - 2, centerY + 2));
        blocks.Add(new Vector4(centerX - 2, centerX + 2, centerY + 3, centerY + 5));
        blocks.Add(new Vector4(centerX - 2, centerX + 2, centerY - 5, centerY - 3));
        int charIndex = 0;
        blocks.ForEach(block =>
        {
            bool rotate = false;
            if (block.x < centerX && block.z < centerY)
                rotate = true;
            for (int i = (int)block.x; i < block.y; i++)
            {
                for (int j = (int)block.z; j < block.w; j++)
                {
                    attackers[charIndex].SetPosition(cellMap[i, j], i, j);
                    if (rotate) attackers[charIndex].Rotate();
                    charIndex += 1;
                }
            }
        });
    }
}
