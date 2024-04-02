using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<Sprite> endings = new List<Sprite>();
    [SerializeField] SpriteRenderer background;

    private string[] dialogueParts;
    float letterDelay = 0.05f;

    void Start()
    {
        if (MiniGameManager.instance.gameData.consecutiveErrors == 2)
        {
            Debug.Log("przegrana");
        }
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

        //if (MiniGameManager.instance.gameData.day == 7)
        //{
        //    yield return new WaitForSeconds(.5f);
        //    text.enabled = false;
        //    if (MiniGameManager.instance.gameData.errors == 0)
        //    {
        //        background.sprite = endings[1];
        //    }
        //    else
        //    {
        //        background.sprite = endings[0];
        //    }
        //    yield return new WaitForSeconds(4.0f);
        //}
        MiniGameManager.instance.ExitCutscene();
    } 
}
