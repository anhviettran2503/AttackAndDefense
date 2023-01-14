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
public enum CharacterState
{
    Idle,
    Move,
    Attack,
    Die
}
public class GameSpecs
{
    public static int AttackerAmount = 32;
    public static int DefenderAmount = 16;
    public static int CharTotal = AttackerAmount + DefenderAmount;
}