using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("kolizja");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("yas queen");
            SceneManager.LoadScene("LevelMenu");
        }
    }
}
