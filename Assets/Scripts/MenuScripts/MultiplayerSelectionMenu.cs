using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSelectionMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject hostMenu;
    [SerializeField] GameObject clientMenu;
    public void BackButton()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void HostButton()
    {
        this.gameObject.SetActive(false);
        hostMenu.SetActive(true);
    }

    public void ClientButton()
    {
        this.gameObject.SetActive(false);
        clientMenu.SetActive(true);
    }
}
