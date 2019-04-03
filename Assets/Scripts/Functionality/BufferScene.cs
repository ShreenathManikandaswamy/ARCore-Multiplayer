using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferScene : MonoBehaviour
{

    public GameObject BasicUI;

    private void Start()
    {
        BasicUI.SetActive(true);
    }

    public void StartScene()
    {
        BasicUI.SetActive(false);
        Application.LoadLevel(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
