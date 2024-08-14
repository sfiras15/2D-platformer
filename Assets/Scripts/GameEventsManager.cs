using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;

    public event Action<int> onGoldGained;

    public QuestEvents questEvents;

    private int goldAmount;

    public event Action onPortalOpened;
    public event Action onPortalClosed;


    private void Awake()
    {
        if (instance == null) instance = this;  

        questEvents = new QuestEvents();
    }

    public void GoldGained(int value)
    {
        if (onGoldGained != null)
        {
            goldAmount += value;
            onGoldGained(value);
        }
    }
    public void OpenPortal()
    {
        if (onPortalOpened != null)
        {
            onPortalOpened();
        }
    }
    public void ClosePortal()
    {
        if (onPortalClosed != null)
        {
            onPortalClosed();
        }
    }
    public void GoToNextLevel()
    {
        Debug.Log("SceneCount:" + SceneManager.sceneCountInBuildSettings);

        if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("SceneIndex:" + SceneManager.GetSceneByBuildIndex(0).buildIndex);
            SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).buildIndex + 1);
        }
        
        Debug.Log("Next Level");
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Next Level");
    }
}
