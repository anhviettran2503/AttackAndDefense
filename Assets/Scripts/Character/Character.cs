using AxieMixer.Unity;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField] protected CharacterAnimation anim;
    [SerializeField] protected CharacterAction action;
    [SerializeField] protected CharacterType characterType;
    [SerializeField] protected int maxHp;
    [SerializeField] protected string axieId;
    [SerializeField] protected int currentHp;
    [SerializeField] protected bool isCanMove;
    [SerializeField] protected int positionX;
    [SerializeField] protected int positionY;
    protected float attackDmg;
    protected float randomNum;
   
    public bool CanMove => isCanMove;
    
    public void SetPosition(Cell cellPos, int _posX, int _posY)
    {
        transform.SetParent(cellPos.transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        positionX = _posX;
        positionY = _posY;
    }
    public void LoadSpine(string _geneID)
    {
        anim.LoadSpine(axieId, _geneID);
    }
    public void Rotate()
    {
        anim.Rotate();
    }
    public void DoAction()
    {
        action.DoAction(isCanMove, positionX, positionY);
    }

}
