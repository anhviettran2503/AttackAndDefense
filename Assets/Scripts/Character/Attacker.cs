using AxieMixer.Unity;
using NaughtyAttributes;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Character
{
    private Vector2 centerPoint;
    [SerializeField] private Defender target;
    [SerializeField] private AttackerAction direction;
    [SerializeField] private Vector2 predictPos;
    private void Start()
    {
        maxHp = 16;
        currentHp = maxHp;
        centerPoint = Vector2.zero;
    }
    [Button]
    public void Action()
    {
        if (target == null) SetTarget();
        ChoiceDirection();
        if (direction != AttackerAction.Idle)
        {
            GamePlayHandler.Instance.UpdateAttackerPosition(this, new Vector2(posX, posY), predictPos);
        }
    }
    [Button]
    private void SetTarget()
    {
        var defenders = GamePlayHandler.Instance.Denfenders;
        int minDistance = GameSpecs.SizeTableX * GameSpecs.SizeTableY;
        defenders.ForEach(x =>
        {
            if ((Mathf.Abs(x.PosX - posX) + Mathf.Abs(x.PosY - posY)) < minDistance)
            {
                minDistance = (Mathf.Abs(x.PosX - posX) + Mathf.Abs(x.PosY - posY));
                target = x;
            }
        });
    }
    private void ChoiceDirection()
    {
        var targetX = target.PosX;
        var targetY = target.PosY;
        predictPos.x = posX;
        predictPos.y = posY;
        if (posX==targetX)
        {
            if(posY<targetY)
            {
                predictPos.y = posY + 1;
                direction = AttackerAction.Up;
            }
            else
            {
                predictPos.y = posY - 1;
                direction = AttackerAction.Down;
            }
            if(GamePlayHandler.Instance.Table[(int)predictPos.x, (int)predictPos.y].Char!=null)
            {
                direction = AttackerAction.Idle;
            }
            return;
        }
        if(posY==targetY)
        {
            if (posX < targetX)
            {
                predictPos.x = posX + 1;
                direction = AttackerAction.Right;
            }
            else
            {
                predictPos.x = posX - 1;
                direction = AttackerAction.Left;
            }
            if (GamePlayHandler.Instance.Table[(int)predictPos.x, (int)predictPos.y].Char != null)
            {
                direction = AttackerAction.Idle;
            }
            return;
        }
    }
    private void OnDestroy()
    {

    }
}
