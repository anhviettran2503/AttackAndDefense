using AxieMixer.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    [SerializeField] Attacker attackerPrefab;
    [SerializeField] Defender defenderPrefab;
    [SerializeField] private List<Attacker> attackers = new List<Attacker>();
    [SerializeField] private List<Defender> defenders = new List<Defender>();
    public List<Attacker> Attackers => attackers;
    public List<Defender> Defenders => defenders;
    private void Start()
    {
        Mixer.Init();
        attackers = new List<Attacker>();
        defenders = new List<Defender>();
    }
    public List<Attacker> GenAttackers(string geneID, int amount)
    {
        attackers = new List<Attacker>();
        attackers = GetComponentsInChildren<Attacker>().ToList();
        if (attackers.Count <= 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GenCharacter(CharacterType.Attacker, geneID);
            }
        }
        return attackers;
    }
    public List<Defender> GenDefenders(string geneID, int amount)
    {
        defenders = new List<Defender>();
        defenders = GetComponentsInChildren<Defender>().ToList();
        if (defenders.Count <= 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GenCharacter(CharacterType.Defender, geneID);
            }
        }
        return defenders;
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
    public void CharDead(Character _char)
    {
        _char.transform.SetParent(transform);
        _char.transform.localPosition = Vector3.zero;
        _char.CharacterDead();
    }
}
