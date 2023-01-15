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
    [SerializeField] protected string axieId;

    protected int maxHp;
    protected int currentHp;
    protected int posX;
    protected int posY;
    public CharacterType Type => characterType;
    public int PosX { set => posX = value; get => posX; }
    public int PosY { set => posY = value; get => posY; }
    public bool IsDead => currentHp <= 0;
    public int HP => currentHp;

    public void SetPosition(Cell cellPos, int _posX, int _posY)
    {
        cellPos.Char = this;
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
    public int RandomNumber()
    {
        return Random.Range(0, 3);
    }
    public void TakeDmg(int _dmg)
    {
        currentHp -= _dmg;
        currentHp = Mathf.Max(0, currentHp);
        anim.SetHealthBar(currentHp);
    }
}

