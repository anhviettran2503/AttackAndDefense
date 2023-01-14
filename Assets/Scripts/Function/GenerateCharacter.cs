using AxieMixer.Unity;
using NaughtyAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;

public class GenerateCharacter : MonoBehaviour
{
    [SerializeField] Attacker attackerPrefab;
    [SerializeField] Defender defenderPrefab;
    private List<Attacker> attackers = new List<Attacker>();
    private List<Defender> defenders = new List<Defender>();
    public List<Attacker> Attackers => attackers;
    public List<Defender> Defenders => defenders;
    private void Start()
    {
        Mixer.Init();
       
    }
    public void GenAttackers(string geneID,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GenCharacter(CharacterType.Attacker, geneID);
        }
    }
    public void GenDefenders(string geneID, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GenCharacter(CharacterType.Defender, geneID);
            
        }
    }
    private void GenCharacter(CharacterType type, string geneID)
    {
        switch (type)
        {
            case CharacterType.Attacker:
                var attacker = Instantiate(attackerPrefab, transform);
                attacker.LoadSpine(geneID);
                attackers.Add(attacker);
                break;
            case CharacterType.Defender:
                var defender = Instantiate(defenderPrefab, transform);
                defender.LoadSpine(geneID);
                defenders.Add(defender);
                break;
            default:
                break;
        }
    }
}
