using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject ControlsGuide;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Level1()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Level3()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Level4()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Level5()
    {
        SceneManager.LoadSceneAsync(5);
    }

    public void Level6()
    {
        SceneManager.LoadSceneAsync(6);
    }

    public void Level7()
    {
        SceneManager.LoadSceneAsync(7);
    }
    public void Level8()
    {
        SceneManager.LoadSceneAsync(8);
    }
    public void Level9()
    {
        SceneManager.LoadSceneAsync(9);
    }
    public void Level10()
    {
        SceneManager.LoadSceneAsync(10);
    }


    //public void Awake()
    //{
    //    // Need to add the "if button pressed than set to active thing. Plus a cross it top right to go back. 
    //    ControlsGuide.SetActive(true);
    //}


}
