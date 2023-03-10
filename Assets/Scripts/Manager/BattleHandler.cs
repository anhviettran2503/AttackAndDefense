using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
public class BattleHandler : MonoBehaviour
{
    private List<LitteBattle> litteBattles;
    private Cell[,] tables;
    [SerializeField] private List<Attacker> attackers;
    [SerializeField] private List<Defender> defenders;
    [SerializeField] private float battleDuration = 1f;
    [SerializeField] private int battleTurn;
    private Vector2 timeDurationClamp = new Vector2(0.5f, 4f);
    private float tempTime;
    public List<Defender> Denfenders => defenders;
    public Cell[,] Table => tables;
    public UnityAction<int> SetTurn;
    public UnityAction<int> SetAttackerPower;
    public UnityAction<int> SetDefenderPower;
    public UnityAction<float> SetDuration;
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
            battleTurn += 1;
            SetTurn?.Invoke(battleTurn);
            Action();
        }
    }
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        tables = _table;
        attackers = new List<Attacker>();
        defenders = new List<Defender>();
        battleTurn = 0;
        _attackers.ForEach(x =>
        {
            attackers.Add(x);
        });
        _defenders.ForEach(x =>
        {
            defenders.Add(x);
        });
        UpdatePower();
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
    public void ChangeDuration(bool inc)
    {
        var value = 0.5f;
        if (!inc) value = -0.5f;
        battleDuration += value;
        battleDuration = Mathf.Clamp(battleDuration, timeDurationClamp.x, timeDurationClamp.y);
        SetDuration?.Invoke(battleDuration);
    }

    private void Action()
    {
        if (attackers.Count == 0 || defenders.Count == 0)
        {
            RemoveAllCharacter();
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
                RemoveDefender(litteBattles[i].Defender, false);
                RemoveAttacker(litteBattles[i].Attacker, false);
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
        SetAttackerPower?.Invoke(attackerPower);
        SetDefenderPower?.Invoke(defenderPower);
    }
    [Button]
    private void RemoveAllCharacter()
    {
        for (int i = attackers.Count - 1; i >= 0; i--)
        {
            RemoveAttacker(attackers[i], true);
        }
        for (int i = defenders.Count - 1; i >= 0; i--)
        {
            RemoveDefender(defenders[i], true);
        }
    }
    private void RemoveDefender(Defender _defender, bool isOver)
    {
        if (!isOver && (_defender == null || _defender.HP > 0)) return;
        defenders.Remove(_defender);
        GameManager.Instance.Pool.CharDead(_defender);
        tables[_defender.PosX, _defender.PosY].Char = null;

    }
    private void RemoveAttacker(Attacker _attacker, bool isOver)
    {
        if (!isOver && (_attacker == null || _attacker.HP > 0)) return;
        attackers.Remove(_attacker);
        GameManager.Instance.Pool.CharDead(_attacker);
        tables[_attacker.PosX, _attacker.PosY].Char = null;
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
    private IEnumerator ClearCharInCell(Vector2 _currentPos)
    {
        yield return new WaitForFixedUpdate();
        tables[(int)_currentPos.x, (int)_currentPos.y].Char = null;
    }


}
