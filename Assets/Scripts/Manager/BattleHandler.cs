using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    private List<LitteBattle> litteBattles;
    private Cell[,] tables;
    private List<Attacker> attackers;
    private List<Defender> defenders;
    [SerializeField] private float battleDuration = 1f;
    [SerializeField] private int battleTime;
    private float tempTime;
    public List<Defender> Denfenders => defenders;
    public Cell[,] Table => tables;
    private void Start()
    {
        Load();
    }
    private void Load()
    {
        litteBattles = new List<LitteBattle>();
        tempTime = 0;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.Pause) tempTime = 0;
        if (GameManager.Instance.State != GameState.Play) return;
        tempTime += Time.fixedDeltaTime;
        if (tempTime >= battleDuration)
        {
            tempTime = 0;
            battleTime += 1;
            GameManager.Instance.UIManager.SetTimeText(battleTime);
            Action();
        }
    }
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        tables = _table;
        attackers = _attackers;
        defenders = _defenders;
        UpdatePower();
    }
    private void Action()
    {
        if (attackers.Count == 0 || defenders.Count == 0)
        {
            GameManager.Instance.EndGame();
            return;
        };
        AttackMove();
        Fighting();
    }
    private void Fighting()
    {
        SetCharacterReadyForFighting();
        LittleBattleFighting();
        DropBattleEnd();
        UpdatePower();
    }
    private void AttackMove()
    {
        attackers.ForEach(x =>
        {
            x.Action();
        });
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
    private void UpdatePower()
    {
        var attackerPower = 0;
        var defenderPower = 0;
        attackers.ForEach(x =>
        {
            attackerPower += x.HP;
        });
        defenders.ForEach(x =>
        {
            defenderPower += x.HP;
        });
        GameManager.Instance.UIManager.SetAttackerPower(attackerPower);
        GameManager.Instance.UIManager.SetDefenderPower(defenderPower);
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
    public IEnumerator ClearCharInCell(Vector2 _currentPos)
    {
        yield return new WaitForFixedUpdate();
        tables[(int)_currentPos.x, (int)_currentPos.y].Char = null;
    }
}
