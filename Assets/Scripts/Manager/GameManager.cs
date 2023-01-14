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
    private GameState gameState=GameState.None;

    private void Start()
    {
        StartCoroutine(Preparing());
    }
    private IEnumerator Preparing()
    {
        SetGameState(GameState.Waiting);
        createMap.Create();
        loadSpine.LoadGenes();
        yield return new WaitUntil(() => (!string.IsNullOrEmpty(loadSpine.AttackerGenes) && !string.IsNullOrEmpty(loadSpine.DefenderGenes)));
        genChar.GenAttackers(loadSpine.AttackerGenes);
        genChar.GenDefenders(loadSpine.DefenderGenes);
    }
    private void SetGameState(GameState state)
    {
        if (gameState == state) return;
        gameState=state;
    }

}
