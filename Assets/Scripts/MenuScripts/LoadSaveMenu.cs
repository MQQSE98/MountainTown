using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveMenu : MonoBehaviour
{
    [SerializeField] GameObject singleplayerMenu;

    public void LaunchButton()
    {
        //future functionality for loading saved game
    }

    public void BackButton()
    {
        this.gameObject.SetActive(false);
        singleplayerMenu.SetActive(true);
    }
}
