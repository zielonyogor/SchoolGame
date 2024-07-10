using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator notebookAnimator;

    [SerializeField] private TMPro.TextMeshProUGUI startText;

    private void Start()
    {
        if (MiniGameManager.instance.isPlaying)
        {
            notebookAnimator.SetTrigger("isPlaying");
        }
        if(MiniGameManager.instance.gameData.day > 1)
        {
            startText.text = "Continue";
        }
    }

    public void ExitGame()
    {
        SaveSystem.SaveGame();
        Application.Quit();
    }
}
