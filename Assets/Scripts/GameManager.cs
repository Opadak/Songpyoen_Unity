using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    [SerializeField] FieldObjects fieldObjects;
    [SerializeField] UIManager uiManager;

    void Start()
    {
        if (fieldObjects == null)
            fieldObjects = FindObjectOfType<FieldObjects>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();


    }

    void Update()
    {
        
    }

    public void SetColliderEnterFieldObject(GameObject obj)
    {


        fieldObjects.DistroyFieldObj(obj);
    }

    public void SetStart()
    {
        uiManager.SetStart();
        fieldObjects.SetFieldObjActive();

    }

}
