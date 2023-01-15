using AxieMixer.Unity;
using Spine.Unity;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class CharacterAnimation : MonoBehaviour
{
    private Character character;
    [SerializeField] protected SkeletonAnimation characterAnim;
    [SerializeField] protected SpriteRenderer healthBar;

    private Vector2 charSize = new Vector2(0.7f, 0.7f);
    private int currentHP, maxHP;
    private float healthBarFactor;
    private void Start()
    {
        Load();
    }
    private void Load()
    {
        character = GetComponent<Character>();
        maxHP = character.HP;
        currentHP = maxHP;
        healthBarFactor = healthBar.transform.localScale.x;
    }
    public void LoadSpine(string _axieId, string _geneID)
    {
        Mixer.SpawnSkeletonAnimation(characterAnim, _axieId, _geneID);
        characterAnim.GetComponent<MeshRenderer>().sortingOrder = 1;
        characterAnim.skeleton.SetLocalScale(charSize);
        characterAnim.transform.position = new Vector3(0, -0.4f, 0);
        Idle();
    }
    public void Rotate()
    {
        characterAnim.skeleton.ScaleX = -charSize.x;
    }
    
    public void SetHealthBar(int _updateHP)
    {
        if (healthBar == null) return;
        if (_updateHP == currentHP) return;
        var beginValue = (currentHP * healthBarFactor) / maxHP;
        var endValue = (_updateHP * healthBarFactor) / maxHP;
        healthBar.transform.DOScaleX(endValue, 0.5f);
        currentHP = _updateHP;
    }
    public void Idle()
    {
        characterAnim.state.SetAnimation(0, GameManager.Instance.Spine.RandomIdle(), true);
    }
    public void Run()
    {
        characterAnim.state.SetAnimation(0, GameManager.Instance.Spine.RandomRun(), false);
    }
    public void Attack()
    {
        characterAnim.state.SetAnimation(0, GameManager.Instance.Spine.RandomAttack(), false);
    }
}
