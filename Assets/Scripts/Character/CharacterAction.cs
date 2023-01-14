using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterAction : MonoBehaviour
{
    protected Queue<Character> enemies;
    private CharacterState state;
    private void Start()
    {
        state = CharacterState.Idle;
    }
    public void DoAction(bool _isCanMove,int _posX, int _posY)
    {
        if (_isCanMove)
        {

        };
    }
    public void Move()
    {

    }
    public void Idle()
    {

    }
    public void Attack()
    {

    }
    public void Die()
    {

    }
    private void OnDestroy()
    {

    }
}
