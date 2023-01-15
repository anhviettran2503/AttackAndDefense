using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class GamePlayHandler : Singleton<GamePlayHandler>
{
    [SerializeField] private BattleHandler battleHandler;
    public BattleHandler Battle => battleHandler;
    [SerializeField] private float actionDuration = 1f;
    
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        battleHandler.StartGame(_table, _attackers, _defenders);
    }
}
