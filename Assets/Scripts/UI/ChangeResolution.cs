using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ScreenResolution
{
    SmallResolution,
    MediumResolution,
    BigResolution
}
public class ChangeResolution : MonoBehaviour
{
    private ScreenResolution currentResolution = ScreenResolution.MediumResolution;
    public void ToggleResolution()
    {
        switch (currentResolution)
        {
            case ScreenResolution.SmallResolution:
                Screen.SetResolution(1200, 720, false);
                currentResolution = ScreenResolution.MediumResolution;
                break;
            case ScreenResolution.MediumResolution:
                Screen.SetResolution(1800, 1080, true);
                currentResolution = ScreenResolution.BigResolution;
                break;
            default:
                Screen.SetResolution(600, 360, false);
                currentResolution = ScreenResolution.SmallResolution;
                break;
        }
    }
}
