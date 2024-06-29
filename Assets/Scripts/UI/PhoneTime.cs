using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneTime : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = System.DateTime.Now.Hour.ToString("D2") + ":" 
            + System.DateTime.Now.Minute.ToString("D2");
    }
}
