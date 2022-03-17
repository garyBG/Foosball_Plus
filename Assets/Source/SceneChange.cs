using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   public void LoadSinglePlayer ()
    {
        SceneManager.LoadScene("485_FinalProject");
    }
    public void LoadMultiplayer()
    {
        SceneManager.LoadScene("MPScene");
    }
}
