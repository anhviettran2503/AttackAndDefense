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
    [SerializeField] private BattleSort battleHandler;
    private GameState gameState = GameState.None;
    public GameState State => gameState;
    private void Start()
    {
        StartCoroutine(Preparing());
    }
    private IEnumerator Preparing()
    {
        SetGameState(GameState.Waiting);
        createMap.Create();
        loadSpine.LoadGenes();
        yield return new WaitUntil(() => (!string.IsNullOrEmpty(loadSpine.AttackerGenes)
        && !string.IsNullOrEmpty(loadSpine.DefenderGenes)));
        genChar.GenAttackers(loadSpine.AttackerGenes, GameSpecs.AttackerAmount);
        genChar.GenDefenders(loadSpine.DefenderGenes, GameSpecs.DefenderAmount);
        yield return new WaitUntil(() => ((genChar.Attackers.Count+ genChar.Defenders.Count) >= GameSpecs.CharTotal));
        battleHandler.PreparingBattle(createMap.Tables, genChar.Attackers, genChar.Defenders);
        yield return new WaitForSecondsRealtime(2f);
        StartGame();
    }
    private void StartGame()
    {
        GamePlayHandler.Instance.StartGame(createMap.Tables, genChar.Attackers, genChar.Defenders);
    }
    private void SetGameState(GameState state)
    {
        if (gameState == state) return;
        gameState = state;
    }

}
