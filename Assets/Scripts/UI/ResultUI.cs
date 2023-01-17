using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResultUI : Panel
{
    [SerializeField] private TextMeshProUGUI resultText;
    public void SetResultText(string _result)
    {
        resultText.text = _result;
    }
    public void ReMatch()
    {
        GameManager.Instance.ReMatch();
    }
}
