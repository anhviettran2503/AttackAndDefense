using AxieMixer.Unity;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Character
{
    private void Start()
    {
        maxHp = 16;
        currentHp = maxHp;
    }
    private void OnDestroy()
    {
        
    }
}
