using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjects : MonoBehaviour
{
    private List<GameObject> objects;

    [SerializeField] float destroyCouny = 2.5f;

    void Start()
    {
        objects = new List<GameObject>();
        foreach (var arr in GetComponentsInChildren<GameObject>())
        {

            objects.Add(arr);

        }

    }

    void OnDestroy()
    {
        Release();
    }

    public void DistroyFieldObj(GameObject obj)
    {
        GameObject foundObj = objects.Find(item => item == obj);

        if (foundObj != null)
        {
            objects.Remove(foundObj);
            Destroy(foundObj, destroyCouny); // 게임 오브젝트 파괴
        }
    }

    public void SetFieldObjActive()
    {
        foreach(var arr in objects)
        {
            if(!arr.activeSelf)
                arr.SetActive(true);
        }
    }

    void Release()
    {
        foreach(var arr in objects)
        {
            GameObject.DestroyImmediate(arr);

        }
        objects.Clear();

    }

}
