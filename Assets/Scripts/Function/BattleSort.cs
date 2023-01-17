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
    public void PreparingBattle(Cell[,] _cellMap, List<Attacker> attackers, List<Defender> defenders)
    {
        cellMap = _cellMap;
        centerX = (int)(cellMap.GetLongLength(0) / 2);
        centerY = (int)(cellMap.GetLongLength(1) / 2);
        AttackerSort(attackers);
        DefenderSort(defenders);
    }
    private void AttackerSort(List<Attacker> attackers)
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
                    attackers[charIndex].CharacterAlive();
                    cellMap[i, j].Char = attackers[charIndex];
                    if (rotate) attackers[charIndex].Rotate();
                    charIndex += 1;
                }
            }
        });
    }
    private void DefenderSort(List<Defender> defenders)
    {
        Vector4 block = new Vector4(centerX - 2, centerX + 2, centerY - 2, centerY + 2);
        int charIndex = 0;
        for (int i = (int)block.x; i < block.y; i++)
        {
            for (int j = (int)block.z; j < block.w; j++)
            {
                defenders[charIndex].SetPosition(cellMap[i, j], i, j);
                defenders[charIndex].CharacterAlive();
                cellMap[i, j].Char = defenders[charIndex];
                charIndex += 1;
            }
        }
    }

}
