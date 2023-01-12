using AxieMixer.Unity;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Character
{
    protected override void LoadCharacter()
    {
        base.LoadCharacter();
        //Mixer.SpawnSkeletonAnimation(characterAnim, axieId, genesStrting);
    }
}
