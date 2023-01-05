using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{    

    public void StartTheGame()
    {
        StartCoroutine(StartGameCo());
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    private IEnumerator StartGameCo()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);

    }

}
