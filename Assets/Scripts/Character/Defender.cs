using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Character
{
    public DefenderAction DefenderAction { get; set; }  
    private void Start()
    {
        maxHp = 32;
        currentHp = maxHp;
    }

}
