using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class GamePlayHandler : Singleton<GamePlayHandler>
{
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] private Cell[,] tables;
    [SerializeField] private List<Attacker> attackers;
    [SerializeField] private List<Defender> defenders;


    [SerializeField] private float actionDuration = 1f;
    private float currentTime = 0;
    private Vector2 center;
    public Cell[,] Table => tables;
    public Vector2 Center => center;
    public List<Defender> Denfenders => defenders;
    public List<Attacker> Attackers => attackers;
    private void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.Play) return;
        currentTime += Time.fixedDeltaTime;
        if (currentTime >= actionDuration)
        {
            //Action();
        }
    }
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        tables = _table;
        attackers = _attackers;
        defenders = _defenders;
        center = new Vector2(tables.GetLongLength(0) / 2, tables.GetLongLength(1) / 2);
    }
    public void EndGame()
    {

    }
    [Button]
    public void Action()
    {
        if (attackers.Count == 0) return;
        attackers.ForEach(x =>
        {
            x.Action();
        });
        battleHandler.Fighting();
    }
    public void UpdateAttackerPosition(Attacker _attacker, Vector2 _currentPos, Vector2 _nextPos)
    {
        if (_attacker == null) return;
        var character = tables[(int)_currentPos.x, (int)_currentPos.y].Char;
        if (_attacker == character)
        {
            tables[(int)_nextPos.x, (int)_nextPos.y].Char = _attacker;
            _attacker.transform.SetParent(tables[(int)_nextPos.x, (int)_nextPos.y].transform);
            _attacker.transform.localPosition = Vector2.zero;
            _attacker.PosX = (int)_nextPos.x;
            _attacker.PosY = (int)_nextPos.y;

            StartCoroutine(ClearCharInCell(_currentPos));
        }
    }
    public IEnumerator ClearCharInCell(Vector2 _currentPos)
    {
        yield return new WaitForFixedUpdate();
        tables[(int)_currentPos.x, (int)_currentPos.y].Char = null;
    }
    [Button]
    public void ClearAttackerList()
    {
        attackers= attackers.Where(item => item != null).ToList();
    }
    [Button]
    public void ClearDefenderList()
    {
        defenders= defenders.Where(item => item != null).ToList();
    }
}
