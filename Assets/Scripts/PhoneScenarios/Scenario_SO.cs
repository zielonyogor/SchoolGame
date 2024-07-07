using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "PhoneScenario")]
public class Scenario_SO : ScriptableObject
{
    [Header("Phone Message")]
    [TextArea(4, 4)]
    public string scenarioText;
    public string personName;

    [Header("Optional 2nd Scenario")]
    public Scenario_SO secondScenario;
    //Convention in writing Scenario with another Scenario (for now):
    //  - There is one button - "Continue" Button (text stored in optionCorrect)
    //  - Rest should be null

    [Header("Options")]
    [TextArea(2,2)]
    public string optionCorrect;
    [TextArea(2, 2)]
    public string optionIncorrect;

    [Header("Results")]
    [Tooltip("Default result after choosing correct option")]
    [TextArea(4, 4)]
    public string resultCorrect;
    [Tooltip("Rare result after choosing correct option")]
    [TextArea(4, 4)]
    public string resultCorrectRare;

    [Tooltip("Default result after choosing incorrect option")]
    [TextArea(4, 4)]
    public string resultIncorrect;
    [Tooltip("Rare result after choosing incorrect option")]
    [TextArea(4, 4)]
    public string resultIncorrectRare;

}
