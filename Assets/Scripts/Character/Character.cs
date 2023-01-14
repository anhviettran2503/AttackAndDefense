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
    [SerializeField] protected int posX;
    [SerializeField] protected int posY;
    protected float attackDmg;
    protected float randomNum;
   
    public bool CanMove => isCanMove;
    public CharacterType Type => characterType;
    public int PosX => posX;
    public int PosY => posY;
    public void SetPosition(Cell cellPos, int _posX, int _posY)
    {
        transform.SetParent(cellPos.transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        posX = _posX;
        posY = _posY;
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
        action.DoAction(isCanMove, posX, posY);
    }

}
