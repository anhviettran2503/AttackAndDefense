using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    [SerializeField] private GameObject content;

    public void EnableContent()
    {
        content.SetActive(true);
    }
    public void DisableContent()
    {
        content.SetActive(false);
    }
}
