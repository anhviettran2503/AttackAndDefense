using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayGameUI : Panel
{
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI attackPowerText;
    [SerializeField] private TextMeshProUGUI defenderPowerText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private TextMeshProUGUI durationText;
    private void Start()
    {
        GameManager.Instance.Battle.SetTurn += SetTurnText;
        GameManager.Instance.Battle.SetAttackerPower += SetAttackerPower;
        GameManager.Instance.Battle.SetDefenderPower += SetDefenderPower;
        GameManager.Instance.Battle.SetDuration += SetDuration;
    }
    public void SetTurnText(int _turn)
    {
        turnText.text = _turn.ToString();
    }
    public void SetAttackerPower(int _power)
    {
        attackPowerText.text = "Attacker:" + _power.ToString();
    }
    public void SetDefenderPower(int _power)
    {
        defenderPowerText.text = "Defender:" + _power.ToString();
    }
    public void SetPauseGame()
    {
        GameManager.Instance.PauseGame();
        if (GameManager.Instance.State == GameState.Pause)
        {
            pauseButtonText.text = "Play";
        }
        else
            if (GameManager.Instance.State == GameState.Play)
        {
            pauseButtonText.text = "Pause";
        }
    }
    public void IncDuration()
    {
        GameManager.Instance.Battle.ChangeDuration(true);
    }
    public void DecDuration()
    {
        GameManager.Instance.Battle.ChangeDuration(false);
    }
    public void SetDuration(float duration)
    {
        durationText.text = duration.ToString();
    }
    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.Battle.SetTurn -= SetTurnText;
        GameManager.Instance.Battle.SetAttackerPower -= SetAttackerPower;
        GameManager.Instance.Battle.SetDefenderPower -= SetDefenderPower;
    }
}
