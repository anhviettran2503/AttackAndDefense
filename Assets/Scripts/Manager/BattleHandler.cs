using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private List<LitteBattle> litteBattles;
    [SerializeField] private Cell[,] tables;
    [SerializeField] private List<Attacker> attackers;
    [SerializeField] private List<Defender> defenders;
    public List<Defender> Denfenders => defenders;
    public Cell[,] Table => tables;
    private void Start()
    {
        litteBattles = new List<LitteBattle>();

    }
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        tables = _table;
        attackers = _attackers;
        defenders = _defenders;
    }
    [Button]
    public void Action()
    {
        if (attackers.Count == 0 || defenders.Count==0) return;
        attackers.ForEach(x =>
        {
            x.Action();
        });
        Fighting();

    }
    public void Fighting()
    {
        SetCharacterReadyForFighting();
        LittleBattleFighting();
        DropBattleEnd();
    }
    private void SetCharacterReadyForFighting()
    {
        attackers.ForEach(x =>
        {
            if (x.AttackerAction == AttackerAction.WantToAttack)
            {
                if (x.Target.DefenderAction == DefenderAction.Idle)
                {
                    CreateNewLittleBattle(x, x.Target);
                }
            }
        });

    }
    private void LittleBattleFighting()
    {
        litteBattles.ForEach(x =>
        {
            x.Fighting();
        });
    }
    private void DropBattleEnd()
    {
        if (litteBattles.Count == 0) return;
        for (int i = litteBattles.Count - 1; i >= 0; i--)
        {
            if (litteBattles[i].LittleBattleState == LittleBattleState.End)
            {
                RemoveDefender(litteBattles[i].Defender);
                RemoveAttacker(litteBattles[i].Attacker);
                litteBattles.Remove(litteBattles[i]);
            }
        };
    }
    private void RemoveDefender(Defender _defender)
    {
        if (_defender == null || _defender.HP > 0) return;
        defenders.Remove(_defender);
        Destroy(_defender.gameObject);
        tables[_defender.PosX, _defender.PosY].Char = null;

    }
    private void RemoveAttacker(Attacker _attacker)
    {
        if (_attacker == null || _attacker.HP > 0) return;
        attackers.Remove(_attacker);
        Destroy(_attacker.gameObject);
        tables[_attacker.PosX, _attacker.PosY].Char = null;
    }

    public void UpdateAttackerPosition(Attacker _attacker, Vector2 _currentPos, Vector2 _nextPos)
    {
        if (_attacker == null) return;
        var character = tables[(int)_currentPos.x, (int)_currentPos.y].Char;
        if (_attacker == character)
        {
            _attacker.SetPosition(tables[(int)_nextPos.x, (int)_nextPos.y], (int)_nextPos.x, (int)_nextPos.y);
            StartCoroutine(ClearCharInCell(_currentPos));
        }
    }
    private void CreateNewLittleBattle(Attacker _attacker, Defender _defender)
    {
        var battle = new LitteBattle();
        battle.Attacker = _attacker;
        battle.Defender = _defender;
        _defender.DefenderAction = DefenderAction.Attacking;
        _attacker.AttackerAction = AttackerAction.Attacking;
        litteBattles.Add(battle);
    }
    [Button]
    private void ClearAttackerList()
    {
        attackers = attackers.Where(item => item != null).ToList();
    }
    [Button]
    private void ClearDefenderList()
    {
        defenders = defenders.Where(item => item != null).ToList();
    }
    public IEnumerator ClearCharInCell(Vector2 _currentPos)
    {
        yield return new WaitForFixedUpdate();
        tables[(int)_currentPos.x, (int)_currentPos.y].Char = null;
    }
}
