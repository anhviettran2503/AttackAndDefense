using AxieMixer.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;

public class LoadSpine : MonoBehaviour
{
    [SerializeField] private string attackerID;
    [SerializeField] private string defenderID;
    [SerializeField] private List<string> animations;
    [SerializeField] private List<string> runAnims;
    [SerializeField] private List<string> idleAnims;
    [SerializeField] private List<string> attackAnims;
    private string attackerGenesID;
    private string defenderGenesID;

    public string AttackerGenes => attackerGenesID;
    public string DefenderGenes => defenderGenesID;
    
    private void Start()
    {
        Load();
    }
    private void Load()
    {
        Mixer.Init();
        LoadAnimations();
        attackerGenesID = "";
        defenderGenesID = "";
    }
    private void LoadAnimations()
    {
        animations = Mixer.Builder.axieMixerMaterials.GetMixerStuff(AxieFormType.Normal).GetAnimatioNames();
        runAnims = new List<string>();
        idleAnims = new List<string>();
        attackAnims=new List<string>();
        animations.ForEach(x =>
        {
            if (x.Contains("attack")) attackAnims.Add(x);
            if (x.Contains("run")) runAnims.Add(x);
            if (x.Contains("idle")) idleAnims.Add(x);
        });
    }
    public void LoadGenes()
    {
        StartCoroutine(GetAxiesGenes(attackerID, x =>
        {
            attackerGenesID = x;
        }));
        StartCoroutine(GetAxiesGenes(defenderID, x =>
        {
            defenderGenesID = x;
        }));
    }
    public string RandomRun()
    {
        var index= UnityEngine.Random.Range(0, runAnims.Count);
        return runAnims[index];
    }
    public string RandomIdle()
    {
        int index= UnityEngine.Random.Range(0, idleAnims.Count);
        return idleAnims[index];
    }
    public string RandomAttack()
    {
        int index = UnityEngine.Random.Range(0, attackAnims.Count);
        return attackAnims[index];
    }
    private IEnumerator GetAxiesGenes(string axieId, Action<string> callback)
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
