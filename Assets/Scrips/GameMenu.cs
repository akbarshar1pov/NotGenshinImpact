using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject PanelMenu;
    public GameObject HealthSlider;
    bool isActive = true;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            PanelMenu.SetActive(isActive);
            HealthSlider.SetActive(!isActive);
            Cursor.visible = isActive;
            isActive = !isActive;
        }
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void Restart(string map)
    {
        Application.LoadLevel(map);
    }

    public void Close()
    {
        PanelMenu.SetActive(false);
        isActive = !isActive;
    }
}
