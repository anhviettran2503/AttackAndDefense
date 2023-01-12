using AxieMixer.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Spine.Unity;
using Game;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
public class LoadCharacterSpine : MonoBehaviour
{
    [SerializeField] private string attackerID;
    [SerializeField] private string defenderID;
    private string attackerGenesID;
    private string defenderGenesID;
    
    private void Start()
    {
        Mixer.Init();
        GetBothGenes();
    }
    private void GetBothGenes()
    {
        StartCoroutine(GetAxiesGenes(attackerID, x =>
        {
            attackerGenesID = x;
        })) ;
        StartCoroutine(GetAxiesGenes(defenderID, x =>
        {
            defenderID = x;
        }));
    }
    private IEnumerator GetAxiesGenes(string axieId,Action<string> callback)
    {
        string searchString = "{ axie (axieId: \"" + axieId + "\") { id, genes, newGenes}}";
        JObject jPayload = new JObject();
        jPayload.Add(new JProperty("query", searchString));
        var wr = new UnityWebRequest("https://graphql-gateway.axieinfinity.com/graphql", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jPayload.ToString().ToCharArray());
        wr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        wr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        wr.SetRequestHeader("Content-Type", "application/json");
        wr.timeout = 10;
        yield return wr.SendWebRequest();
        if (wr.error == null)
        {
            var result = wr.downloadHandler != null ? wr.downloadHandler.text : null;
            if (!string.IsNullOrEmpty(result))
            {
                JObject jResult = JObject.Parse(result);
                string genesStr = (string)jResult["data"]["axie"]["newGenes"];
                callback(genesStr);
            }
        }
    }
}
