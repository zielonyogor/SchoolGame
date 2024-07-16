using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_1, text_2;
    [SerializeField] List<Sprite> endings = new List<Sprite>();
    [SerializeField] Image background;

    private string cutsceneText;

    void Start()
    {
        cutsceneText = MiniGameManager.instance.cutsceneText;
        cutsceneText = cutsceneText.Replace('_', '\n');
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue(){
        for (int i = 0; i < cutsceneText.Length; i++)
        {
            text_1.text += cutsceneText[i];
            text_2.text += cutsceneText[i];
            if (cutsceneText[i] == '\n')
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(Constants.letterDelay);
        }
        yield return new WaitForSeconds(3f);
        //for when I'm done with ending sprites (maybe later game version)
        /*if (MiniGameManager.instance.gameData.day == Constants.lastDay)
        {
            yield return new WaitForSeconds(.5f);
            text.enabled = false;
            if (MiniGameManager.instance.gameData.errors == 0)
            {
                background.sprite = endings[1];
            }
            else
            {
                background.sprite = endings[0];
            }
            yield return new WaitForSeconds(4.0f);
        }*/
        MiniGameManager.instance.ExitCutscene();
    } 
}
