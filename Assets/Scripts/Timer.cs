using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance {get; private set; }
    public bool isDecreasing = true;
    private Image time_bar;
    private Image bar;

    private bool m_TimeUp= false;

    [SerializeField] private GameObject timingObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool TimeUp
    {
        get
        {return m_TimeUp; } 
        set
        {
            if (m_TimeUp != value)
            {
                m_TimeUp = value;
                if (true)
                {
                    if(OnTimeUp != null)
                        OnTimeUp();
                }
            }
        }
    }
    public delegate void OnTimeUpDelegate();
    public event OnTimeUpDelegate OnTimeUp;

    void Start()
    {
        bar = GetComponent<Image>();
        time_bar = transform.GetChild(0).GetComponent<Image>(); 
        time_bar.enabled = false;
        bar.enabled = false;
    }

    public IEnumerator DecreaseTimer(float time)
    {
        yield return new WaitForFixedUpdate();
        float spawnPrefabTime = Random.Range(1f, time - 4f);
        StartCoroutine(SpawnTimingGame(spawnPrefabTime));
        time_bar.enabled = true;
        bar.enabled = true;
        float time_left = time;
        if(isDecreasing)
        {
            while (time_left > 0)
            {
                time_left -= Time.deltaTime;
                time_bar.fillAmount = time_left / time;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (time_left > 0)
            {
                time_left -= Time.deltaTime;
                time_bar.fillAmount = (time - time_left) / time;
                yield return new WaitForFixedUpdate();
            }
        }
        TimeUp = true;
        DisableTimer();
    }

    //probably move to minigameManager
    private IEnumerator SpawnTimingGame(float time)
    {
        Debug.Log("meow");
        yield return new WaitForSeconds(time);
        Debug.Log("meowmeow");
        Instantiate(timingObject, new Vector3(0,0,0), Quaternion.identity);
        yield return null;
    }

    public void DisableTimer()
    {
        StopAllCoroutines();
        bar.enabled = false;
        time_bar.enabled = false;
        time_bar.fillAmount = 1f;
    }
}
