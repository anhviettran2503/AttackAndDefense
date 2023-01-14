using AxieMixer.Unity;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterType characterType;
    [SerializeField] protected int maxHp;
    [SerializeField] protected string axieId;
    [SerializeField] protected int currentHp;
    [SerializeField] protected bool isCanMove;
    [SerializeField] protected SkeletonAnimation characterAnim;
    protected float attackDmg;
    protected float randomNum;
    
    public void LoadSpine(string geneID)
    {
        Mixer.SpawnSkeletonAnimation(characterAnim, axieId, geneID);
        characterAnim.GetComponent<MeshRenderer>().sortingOrder = 1;
    }
}
