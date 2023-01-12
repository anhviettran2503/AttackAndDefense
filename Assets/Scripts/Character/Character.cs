using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int axieId;
    [SerializeField] protected int currentHp;
    [SerializeField] protected bool isCanMove;
    [SerializeField] protected SkeletonAnimation characterAnim;
    protected float attackDmg;
    protected float randomNum;
    protected virtual void LoadCharacter()
    {

    }
}
