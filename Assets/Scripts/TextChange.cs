using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private string[] dialogueParts;
    float letterDelay = 0.05f;

    void Start()
    {
        dialogueParts = MiniGameManager.instance.cutsceneText.Split('_');
        StartCoroutine(playDialogue());
    }

    private IEnumerator playDialogue(){
        foreach(string part in dialogueParts){
            for (int i = 0; i <= part.Length; i++)
            {
                text.text = part.Substring(0, i);
                yield return new WaitForSeconds(letterDelay);
            }
            yield return new WaitForSeconds(1.5f);
        }
        SceneManager.LoadScene("LevelMenu"); 
        //should add method for MiniGameManager to Load LevelSelector
    } 
}
