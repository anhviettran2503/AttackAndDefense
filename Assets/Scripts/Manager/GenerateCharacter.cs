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


    private void Start()
    {
        Mixer.Init();
       
    }
    public void GenAttackers(string geneID)
    {
        var character= GetChar(CharacterType.Attacker, geneID);
    }
    public void GenDefenders(string geneID)
    {
        var character = GetChar(CharacterType.Defender, geneID);
    }
    private Character GetChar(CharacterType type, string geneID)
    {
        Character resultObject;
        switch (type)
        {
            case CharacterType.Attacker:
                resultObject = Instantiate(attackerPrefab, this.transform.position, this.transform.rotation);
                resultObject.LoadSpine(geneID);
                break;
            case CharacterType.Defender:
                resultObject = Instantiate(defenderPrefab, this.transform.position, this.transform.rotation);
                resultObject.LoadSpine(geneID);
                break;
            default:
                resultObject = null;
                break;
        }
        return resultObject;
    }
}
