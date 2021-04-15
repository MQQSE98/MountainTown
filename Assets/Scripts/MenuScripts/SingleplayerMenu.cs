using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleplayerMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject loadSaveMenu;
    [SerializeField] private GameObject newSaveMenu;

    public void BackButton()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void CreateNewButton()
    {
        this.gameObject.SetActive(false);
        newSaveMenu.SetActive(true);

        //later add real code to start a new game
    }

    public void LoadButton()
    {
        this.gameObject.SetActive(false);
        loadSaveMenu.SetActive(true);
    }

}
