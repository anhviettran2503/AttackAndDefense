using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GenerateCharacter genChar;
    [SerializeField] private LoadSpine loadSpine;
    [SerializeField] private CreateMap createMap;
    [SerializeField] private BattleSort battleSort;
    [SerializeField] private BattleHandler battleHandler;
    public BattleHandler Battle => battleHandler;
    private GameState gameState = GameState.None;
    public GameState State => gameState;
    [SerializeField] private float battleDuration = 1f;
    private void Start()
    {
        StartCoroutine(Preparing());
    }
    private IEnumerator Preparing()
    {
        createMap.Create();
        loadSpine.LoadGenes();
        yield return new WaitUntil(() => (!string.IsNullOrEmpty(loadSpine.AttackerGenes)
        && !string.IsNullOrEmpty(loadSpine.DefenderGenes)));
        genChar.GenAttackers(loadSpine.AttackerGenes, GameSpecs.AttackerAmount);
        genChar.GenDefenders(loadSpine.DefenderGenes, GameSpecs.DefenderAmount);
        yield return new WaitUntil(() => ((genChar.Attackers.Count+ genChar.Defenders.Count) >= GameSpecs.CharTotal));
        battleSort.PreparingBattle(createMap.Tables, genChar.Attackers, genChar.Defenders);
        yield return new WaitForSecondsRealtime(1f);
        StartGame(createMap.Tables, genChar.Attackers, genChar.Defenders);
    }
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        battleHandler.StartGame(_table, _attackers, _defenders);
    }
}
