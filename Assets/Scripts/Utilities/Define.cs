using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { 
    None,
    Waiting,
    Start,
    Play,
    AttackWin,
    DefenderWin,
    Stop,
    ReMatch,
}
public enum CharacterType
{
    Attacker,
    Defender,
}