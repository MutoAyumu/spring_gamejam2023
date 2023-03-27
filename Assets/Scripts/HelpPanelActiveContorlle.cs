using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelActiveContorlle : MonoBehaviour
{
    [SerializeField] GameObject _helpPanel;
    private void Start()
    {
        _helpPanel.SetActive(false);
    }
    public void Active()
    {
        _helpPanel.SetActive(true);
    }

    public void UnActive()
    {
        _helpPanel.SetActive(false);
    }
}
