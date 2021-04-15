using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSaveMenu : MonoBehaviour
{

    [SerializeField] GameObject singleplayerMenu;

    public void StartButton()
    {
        //future functionality for loading saved game
        SceneManager.LoadScene("Nathan'sTestScene", LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Nathan'sTestScene"));
    }

    public void BackButton()
    {
        this.gameObject.SetActive(false);
        singleplayerMenu.SetActive(true);
    }

}
