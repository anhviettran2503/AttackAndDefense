using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CharacterPool characterPool;
    [SerializeField] private LoadSpine loadSpine;
    [SerializeField] private CreateMap createMap;
    [SerializeField] private BattleSort battleSort;
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] private UIManager uiManager;
    private GameState gameState;

    public BattleHandler Battle => battleHandler;
    public LoadSpine Spine => loadSpine;
    public GameState State => gameState;
    public UIManager UIManager => uiManager;
    public CharacterPool Pool => characterPool;
    private void Start()
    {
        StartCoroutine(Preparing());
    }
    private IEnumerator Preparing()
    {
        gameState = GameState.Preparing;
        uiManager.OnPreparing();
        createMap.Create();
        loadSpine.LoadGenes();
        yield return new WaitUntil(() => (!string.IsNullOrEmpty(loadSpine.AttackerGenes)
        && !string.IsNullOrEmpty(loadSpine.DefenderGenes)));
        StartCoroutine(NewGame());
    }
    private IEnumerator NewGame()
    {
        var attackers = characterPool.GenAttackers(loadSpine.AttackerGenes, GameSpecs.AttackerAmount);
        var defenders = characterPool.GenDefenders(loadSpine.DefenderGenes, GameSpecs.DefenderAmount);
        yield return new WaitUntil(() => ((attackers.Count + defenders.Count) == GameSpecs.CharTotal));
        battleSort.PreparingBattle(createMap.Tables, attackers, defenders);
        yield return new WaitForSecondsRealtime(1f);
        StartGame(createMap.Tables, attackers, defenders);
    }
    public void StartGame(Cell[,] _table, List<Attacker> _attackers, List<Defender> _defenders)
    {
        uiManager.OnPlayGame();
        gameState = GameState.Play;
        battleHandler.StartGame(_table, _attackers, _defenders);
    }
    public void PauseGame()
    {
        if (gameState == GameState.Play) gameState = GameState.Pause;
        else if (gameState == GameState.Pause) gameState = GameState.Play;
    }
    public void EndGame()
    {
        uiManager.OnStopGame();
        gameState = GameState.Stop;
    }
    public void ReMatch()
    {
        uiManager.OnPreparing();
        StartCoroutine(NewGame());
    }
}
