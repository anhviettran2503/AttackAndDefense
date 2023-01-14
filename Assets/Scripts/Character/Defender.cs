using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Character
{
    private void Start()
    {
        maxHp = 32;
        currentHp = maxHp;
    }
}
