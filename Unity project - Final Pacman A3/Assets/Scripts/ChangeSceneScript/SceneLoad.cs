using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    public void LoadExit()
    {
        SceneManager.LoadScene("StartScene");
    }


}
