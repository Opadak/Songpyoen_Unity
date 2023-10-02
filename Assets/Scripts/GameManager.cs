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

    int perInstance;
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

    void Update()
    {
        
    }

    public void SetColliderEnterFieldObject(GameObject obj)
    {
        if(obj.GetInstanceID() != perInstance)
        {
           bool isVictory = uiManager.ReduceSongPyeon();
            if (isVictory)
            {
                Invoke("GameVictory", 3f);
            }
        }
            
        fieldObjects.DistroyFieldObj(obj);
        perInstance = obj.GetInstanceID();
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
        FindObjectOfType<Slingshot>().CreatePersimmon();
    }


    public void Pause()
    {
        Time.timeScale = 0f;
        GamePausePopup.SetActive(true);
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
        if(GameVictoryPopup.activeSelf)
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
