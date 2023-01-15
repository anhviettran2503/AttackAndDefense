using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
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

public enum AttackerAction
{
    Idle,
    WantToAttack,
    Attacking,
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown,
}
public enum DefenderAction
{
    Idle,
    Attacking,
}
[System.Serializable]
public enum LittleBattleState
{
    Fighting,
    AttackerWin,
    DefenderWin,
    BothDead,
}
public class GameSpecs
{
    public static int AttackerAmount = 32;
    public static int DefenderAmount = 16;
    public static int SizeTableX = 22;
    public static int SizeTableY = 22;
    public static int CharTotal = AttackerAmount + DefenderAmount;
}
[System.Serializable]
public class LitteBattle
{
    public Attacker Attacker;
    public Defender Defender;
    public LittleBattleState LittleBattleState;
    public int attackerHP;
    public int defenderHP;
    public void Fighting()
    {
        if (LittleBattleState != LittleBattleState.Fighting) return;
        var attackerNum = Attacker.RandomNumber();
        var targetNum = Defender.RandomNumber();
        var dmgDeal = 0;
        var tempDmg = (3 + attackerNum - targetNum) % 3;
        if (tempDmg == 0)
        {
            dmgDeal = 4;
        }
        if (tempDmg == 1)
        {
            dmgDeal = 5;
        }
        if (tempDmg == 2)
        {
            dmgDeal = 3;
        }
        Attacker.TakeDmg(dmgDeal);
        Defender.TakeDmg(dmgDeal);
        attackerHP = Attacker.HP;
        defenderHP = Defender.HP;
        if (Attacker.IsDead || Defender.IsDead)
        {
            if (Attacker.IsDead && Defender.IsDead) LittleBattleState = LittleBattleState.BothDead;
            else
                if (Attacker.IsDead)
            {
                LittleBattleState = LittleBattleState.DefenderWin;
            }
            else
                 if (Defender.IsDead)
            {
                LittleBattleState = LittleBattleState.AttackerWin;
            }
        }
    }
}