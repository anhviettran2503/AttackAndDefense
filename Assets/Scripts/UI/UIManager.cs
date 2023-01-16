using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]private List<Panel> panelList;
    
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private Button rematchButton;
    public void OnPreparing()
    {
        panelList.ForEach(x =>
        {
            if (x is WaitingUI)
            {
                x.EnableContent();
            }
            else
            {
                x.DisableContent();
            }
        });
    }
    public void OnPlayGame()
    {
        panelList.ForEach(x =>
        {
            if (x is PlayGameUI)
            {
                x.EnableContent();
            }
            else
            {
                x.DisableContent();
            }
        });
    }
    public void OnStopGame()
    {
        panelList.ForEach(x =>
        {
            if (x is ResultUI)
            {
                x.EnableContent();
            }
            else
            {
                x.DisableContent();
            }
        });
    }
    
}
