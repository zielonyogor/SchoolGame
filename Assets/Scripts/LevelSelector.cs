using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public void LoadSelector()
    {
        
    }
    public void LoadScene(int day)
    {
        //GlobalVariables.Set("currentDay", day); - nie ma tego juz
        SceneManager.LoadScene("DialogueGame");
    }
}
