using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldObjects : MonoBehaviour
{
    [SerializeField] GameObject objPrefab;
    [SerializeField] List<GameObject> objects;
    [SerializeField] List<Vector3> objsPos;

    [SerializeField] float destroyCouny = 2.5f;

    private void Awake()
    {
        foreach(var arr in objects)
        {
            objsPos.Add(arr.transform.position);
        }
    }
    void OnDestroy()
    {
        Release();
    }

    public void DistroyFieldObj(GameObject obj, Vector2 dir)
    {
        GameObject foundObj = objects.Find(item => item == obj);
        foundObj.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
        foundObj.GetComponent<Animator>().SetBool("FadeOut",true);
        if (foundObj != null)
        {
            objects.Remove(foundObj);
            Destroy(foundObj, destroyCouny); // 게임 오브젝트 파괴
        }
    }

    public void SetFieldObjActive()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            GameObject.Destroy(objects[i]);
        }
        objects.Clear();

        objects = new List<GameObject>();
        for (int i = 0 ; i < objsPos.Count; i++)
        {
            GameObject obj = Instantiate(objPrefab,this.gameObject.transform) as GameObject;
            obj.transform.position = objsPos[i];
            objects.Add(obj);
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
