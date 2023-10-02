using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] FieldObjects fieldObjects;
    [SerializeField] UIManager uiManager;

    [SerializeField] GameObject GameOverPopup;
    [SerializeField] GameObject GamePausePopup;
    [SerializeField] GameObject GameVictoryPopup;

    bool isVictory;
    protected override void Awake()
    {
        base.Awake();

        if (fieldObjects == null)
            fieldObjects = FindObjectOfType<FieldObjects>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

        GameOverPopup.SetActive(false);
        GamePausePopup.SetActive(false);
        GameVictoryPopup.SetActive(false);
    }

    public void SetColliderEnterFieldObject(GameObject obj, Vector3 dir)
    {
        
        isVictory = uiManager.ReduceSongPyeon();
        if (isVictory)
        {
            Invoke("GameVictory", 3f);
        }
        
        Vector2 direction = new Vector2(dir.x * 5f, dir.y * 5f);  
        fieldObjects.DistroyFieldObj(obj, direction);
    }

    public void DeleteIcon()
    {
        if(uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
        bool isZero = uiManager.ReducePersimmonIcons();
        if(isZero)
        {
            //게임 종료
            Invoke("GameEnd", 5f);
        }
    }

    public void SetStart()
    {
        uiManager.SetStart();
        fieldObjects.SetFieldObjActive();
        Slingshot slingshot = FindObjectOfType<Slingshot>();
        slingshot.CanCreate = true;
        slingshot.CreatePersimmon();
    }


    public void Pause()
    {
        if (!GamePausePopup.activeSelf)
        {
            Time.timeScale = 0f;
            GamePausePopup.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            GamePausePopup.SetActive(false);
        }
    }


    public void ReStart()
    {
        GamePausePopup.SetActive(false);
        GameOverPopup.SetActive(false);
        GameVictoryPopup.SetActive(false);
        Time.timeScale = 1f;
        SetStart();
    }

    public void Continue()
    {
        GamePausePopup.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameVictory()
    {
        Time.timeScale = 0f;
        Relese();
        GameVictoryPopup.SetActive(true);

    }

    public void GameEnd()
    {
        if(isVictory)
            return;
        Relese();
        Time.timeScale = 0f;
        GameOverPopup.SetActive(true);
    }


    void Relese()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Persimmon");
        if(gameObjects.Length > 0)
        {
            foreach(var arr in  gameObjects)
            {
                GameObject.Destroy(arr);
            }
        }

        FindObjectOfType<PathPoints>().Clear();

    }

}
