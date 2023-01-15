using AxieMixer.Unity;
using NaughtyAttributes;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attacker : Character
{
    protected Queue<Character> enemies;
    [SerializeField] private Defender target;
    [SerializeField] private AttackerAction attackerAction;
    [SerializeField] private Vector2 predictPos;
    [SerializeField] private int minDistance;
    public AttackerAction AttackerAction { set => attackerAction = value; get => attackerAction; }
    public Defender Target => target;
    private void Start()
    {
        maxHp = 16;
        currentHp = maxHp;
    }
    [Button]
    public void Action()
    {
        if (target == null) SetTarget();
        UpdateDistance();
        attackerAction=CheckDirection();
        if (attackerAction == AttackerAction.Idle || attackerAction == AttackerAction.WantToAttack) return;
        GamePlayHandler.Instance.UpdateAttackerPosition(this, new Vector2(posX, posY), predictPos);
    }
    [Button]
    private void SetTarget()
    {
        var defenders = GamePlayHandler.Instance.Denfenders;
        minDistance = GameSpecs.SizeTableX * GameSpecs.SizeTableY;
        defenders.ForEach(x =>
        {
            if ((Mathf.Abs(x.PosX - posX) + Mathf.Abs(x.PosY - posY)) < minDistance)
            {
                target = x;
                UpdateDistance();
            }
        });
    }
    private void UpdateDistance()
    {
        if (target == null) return;
        minDistance = Distance(target.PosX, target.PosY, posX, PosY);
    }

    private AttackerAction CheckDirection()
    {
        Dictionary<AttackerAction, int> direction = new Dictionary<AttackerAction, int>();
        AttackerAction bestDirection;
        direction[AttackerAction.MoveLeft] = Distance(posX - 1, posY, target.PosX, target.PosY);
        direction[AttackerAction.MoveRight] = Distance(posX + 1, posY, target.PosX, target.PosY);
        direction[AttackerAction.MoveUp] = Distance(posX, posY + 1, target.PosX, target.PosY);
        direction[AttackerAction.MoveDown] = Distance(posX, posY - 1, target.PosX, target.PosY);
        var best = direction.OrderBy(r => r.Value).Take(1);

        Debug.Log("Best choice=" + best.ElementAt(0).Key + " " + best.ElementAt(0).Value);
        var predictDirection = new Vector2(posX, PosY);
        bestDirection = best.ElementAt(0).Key;
        switch (best.ElementAt(0).Key)
        {
            case AttackerAction.MoveLeft:
                predictDirection.x -= 1;
                break;
            case AttackerAction.MoveRight:
                predictDirection.x += 1;
                break;
            case AttackerAction.MoveUp:
                predictDirection.y += 1;
                break;
            case AttackerAction.MoveDown:
                predictDirection.y -= 1;
                break;
            default:
                break;
        }
        var character = GamePlayHandler.Instance.Table[(int)predictDirection.x, (int)predictDirection.y].Char;
        if (character != null)
        {
            switch (character.Type)
            {
                case CharacterType.Attacker:
                    return AttackerAction.Idle;
                case CharacterType.Defender:
                    return AttackerAction.WantToAttack;
                default:
                    break;
            }
        }
        predictPos = predictDirection;
        return bestDirection;
    }
    private int Distance(int beginX, int beginY, int endX, int endY)
    {
        return (int)(Mathf.Abs(beginX - endX) + Mathf.Abs(beginY - endY));
    }
    private void OnDestroy()
    {

    }
}
