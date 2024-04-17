using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonGuide : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] List<InputAction> actions;
    [SerializeField] List<Image> buttons;

    [Header("Button Idle/Clicked")]
    [SerializeField] List<Sprite> buttonSprites = new List<Sprite>();

    private void OnEnable()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Enable();
            int index = i;
            actions[i].performed += ctx => ShowPressedButton(index);
            actions[i].canceled += ctx => ShowIdleButton(index);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].performed -= ctx => ShowPressedButton(i);
            actions[i].canceled -= ctx => ShowIdleButton(i);
            actions[i].Disable();
        }
    }

    void ShowPressedButton(int index)
    {
        buttons[index].sprite = buttonSprites[1];
    }

    void ShowIdleButton(int index)
    {
        buttons[index].sprite = buttonSprites[0];
    }
}
