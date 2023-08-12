using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    private GroundPiece[] allgroundPieces;

    public GameObject PlayPanel;
    public GameObject VolumePanel;

    void Start()
    {
        SetUpNewLevel();
       
        if(PlayPanel.activeInHierarchy )//prevents  game from playing if the playpanel is active
        {
            Time.timeScale = 0;
        }
        
    }

    public void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }else if(Singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
   private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelCompleted;
    }
    private void OnLevelCompleted(Scene scene , LoadSceneMode mode)
    {
        SetUpNewLevel();
    }

    public void checkCompleted()
    {
        bool IsFinished = true;

        for(int i = 1; i < allgroundPieces .Length; i++)
        {
            if(allgroundPieces [i].isColored == false )
            {
                IsFinished = false;
                break;
            }
        }
        if(IsFinished)
        {
            
            NextLevel();
           
        }
    }



    private  void NextLevel()
    {
        if(SceneManager .GetActiveScene ().buildIndex == 5)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
    public void makeVolumePanelActive()
    {
        VolumePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CancelVolumePanel()
    {
        VolumePanel.SetActive(false);
        Time.timeScale = 1;
    }


    public void PlayPanelClick()
    {
        PlayPanel.SetActive(false);
        Time.timeScale = 1;
    }
    private void SetUpNewLevel()
    {
        allgroundPieces = FindObjectsOfType<GroundPiece>();
    }
}
