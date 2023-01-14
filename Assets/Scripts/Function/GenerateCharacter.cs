using AxieMixer.Unity;
using NaughtyAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GenerateCharacter : MonoBehaviour
{
    [SerializeField] Attacker attackerPrefab;
    [SerializeField] Defender defenderPrefab;
    private List<Character> characters = new List<Character>();
    public List<Character> Characters => characters;
    private void Start()
    {
        Mixer.Init();
       
    }
    public void GenAttackers(string geneID,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var character = GetChar(CharacterType.Attacker, geneID);
            characters.Add(character);
        }
    }
    public void GenDefenders(string geneID, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var character = GetChar(CharacterType.Defender, geneID);
            characters.Add(character);
        }
    }
    private Character GetChar(CharacterType type, string geneID)
    {
        Character resultObject;
        switch (type)
        {
            case CharacterType.Attacker:
                resultObject = Instantiate(attackerPrefab, transform);
                resultObject.LoadSpine(geneID);
                break;
            case CharacterType.Defender:
                resultObject = Instantiate(defenderPrefab, transform);
                resultObject.LoadSpine(geneID);
                break;
            default:
                resultObject = null;
                break;
        }
        return resultObject;
    }
}
