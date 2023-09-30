using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField]
    Transform BoomPos;
    Slingshot slingshot;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;

    public bool isFollow = false;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

    }
    void FixedUpdate()
    {
        /*if(slingshot.CheckSongPyeon() != null)
        {
            BoomPos = slingshot.CheckSongPyeon().transform;
            LimitCameraArea();
        }
        else
        {
            Reset();
        }*/
    }

    void LimitCameraArea()
    {

        transform.position = Vector3.Lerp(transform.position,
                                          BoomPos.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    public void Reset()
    {
        transform.position = cameraPosition;
    }

}
