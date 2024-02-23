using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private string[] dialogueParts;
    float letterDelay = 0.05f;

    void Start()
    {
        //dialogueParts = MiniGameManager.instance.dayInfo.dialogue.Split('_');
        dialogueParts = "everything is simply a shape, a form, an identifier to let other recognize me as me.. but then what am i...? is this me? my true self, my fake self? what is it that i am?! NOBODY UNDERSTANDS ME. !!_MEOW MEOW MEOW".Split('_');
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
    } 
}
