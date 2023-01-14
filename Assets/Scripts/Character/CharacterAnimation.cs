using AxieMixer.Unity;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] protected SkeletonAnimation characterAnim;
    private Vector2 charSize = new Vector2(0.7f, 0.7f);
    public void LoadSpine(string _axieId,string _geneID)
    {
        Mixer.SpawnSkeletonAnimation(characterAnim, _axieId, _geneID);
        characterAnim.GetComponent<MeshRenderer>().sortingOrder = 1;
        characterAnim.skeleton.SetLocalScale(charSize);
        characterAnim.transform.position = new Vector3(0, -0.2f, 0);
        Idle();
    }
    public void Rotate()
    {
        characterAnim.skeleton.ScaleX = -charSize.x;
    }
    public void Idle()
    {
        characterAnim.state.SetAnimation(0, "action/idle/normal", true);
    }
}
