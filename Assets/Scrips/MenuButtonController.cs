using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public void StartButton()
    {
        Cursor.visible = false;
        Application.LoadLevel("Story");
    }

    public void OptionsButton(GameObject options)
    {
        options.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
