using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject multiplayerSelectionMenu;
    [SerializeField] GameObject singleplayerMenu;


    public void MultiplayerButton()
    {
        this.gameObject.SetActive(false);
        multiplayerSelectionMenu.SetActive(true);
    }

    public void SinglePlayerButton()
    {
        this.gameObject.SetActive(false);
        singleplayerMenu.SetActive(true);
    } 

    public void QuitButton()
    {
        Application.Quit();
    }
}
