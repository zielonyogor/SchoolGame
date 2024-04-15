using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCountdown : MonoBehaviour
{
    public TextMeshProUGUI textHolder;
    public void ChangeText(string newText)
    {
        transform.GetComponent<TextMeshProUGUI>().text = newText;
    }

    public void Exit()
    {
        Destroy(gameObject);
    }
}
