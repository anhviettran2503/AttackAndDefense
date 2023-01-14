using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayHandler : Singleton<GamePlayHandler>
{

    [SerializeField] private Cell[,] tables;
    [SerializeField] private List<Character> attackers;
    [SerializeField] private List<Character> defenders;
    public Cell[,] Table => tables;
    private void FixedUpdate()
    {
        
    }
    public void StartGame()
    {

    }
    public void EndGame()
    {

    }

    private void Action()
    {

    }
    
}
