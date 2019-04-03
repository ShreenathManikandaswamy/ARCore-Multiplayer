using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLoggedIn : MonoBehaviour
{

    public GameObject uncheckedbutton;
    public GameObject checkedbutton;
    // Start is called before the first frame update
    void Start()
    {
        uncheckedbutton.SetActive(true);
        checkedbutton.SetActive(false);
    }

    public void OnClickUnChecked()
    {
        uncheckedbutton.SetActive(false);
        checkedbutton.SetActive(true);
    }

    public void OnClickChecked()
    {
        uncheckedbutton.SetActive(true);
        checkedbutton.SetActive(false);
    }
}
