using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject waitingPanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject resultPanel;

    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI attackPowerText;
    [SerializeField] private TextMeshProUGUI defenderPowerText;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private Button rematchButton;
    public void OnPreparing()
    {
        waitingPanel.SetActive(true);
        resultPanel.SetActive(false);
    }
    public void OnPlayGame()
    {
        waitingPanel.SetActive(false);
        playPanel.SetActive(true);
    }
    public void OnStopGame()
    {
        resultPanel.SetActive(true);
    }
    public void OnPauseGame()
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
    

    public void SetTimeText(int _turn)
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
}
