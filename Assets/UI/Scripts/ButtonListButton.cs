using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    private Text MyText;

    [SerializeField]
    private ButtonListControl buttonControl;

    public void SetText(string TextString)
    {
        MyText.text = TextString;
    }

    public void OnClick()
    {
        buttonControl.ButtonClicked(MyText.text);
    }
}
