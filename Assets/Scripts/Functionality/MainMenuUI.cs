using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject PrimaryPanel;
    public GameObject SecondaryPanel;
    public GameObject FirstArrow;
    public GameObject SecondArrow;
    public GameObject ThirdArrow;


    private void Start()
    {
        FirstArrow.SetActive(true);
        PrimaryPanel.SetActive(false);
        SecondaryPanel.SetActive(false);
        SecondArrow.SetActive(false);
        ThirdArrow.SetActive(false);
    }

    public void Primary()
    {
        FirstArrow.SetActive(false);
        PrimaryPanel.SetActive(true);
        SecondaryPanel.SetActive(false);
        SecondArrow.SetActive(true);
        ThirdArrow.SetActive(false);
    }

    public void Secondary()
    {
        FirstArrow.SetActive(true);
        PrimaryPanel.SetActive(false);
        SecondaryPanel.SetActive(false);
        SecondArrow.SetActive(false);
        ThirdArrow.SetActive(false);
    }

    public void Thrid()
    {
        FirstArrow.SetActive(false);
        PrimaryPanel.SetActive(true);
        SecondaryPanel.SetActive(false);
        SecondArrow.SetActive(true);
        ThirdArrow.SetActive(false);
    }

    public void SecondayPanelTrigger()
    {
        FirstArrow.SetActive(false);
        PrimaryPanel.SetActive(true);
        SecondaryPanel.SetActive(true);
        SecondArrow.SetActive(false);
        ThirdArrow.SetActive(true);
    }
}
