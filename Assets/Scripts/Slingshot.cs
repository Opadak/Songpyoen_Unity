using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 responPos;

    Vector2 firstDragPosition; //Drag First Position 
    Vector2 curDragPosision; //Drag Current Position

    [SerializeField] GameObject particlesPrefab;

    
    [SerializeField] float deadY = -16.8f;

    [SerializeField] float maxForce = 100f; // 최대 힘
    [SerializeField] float minForce = 1f;  // 최소 힘

    List<SlingshotParticles> parts; 
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        responPos = this.gameObject.transform.position;
        CreateParts();
    }

    void Update()
    {
        if(this.gameObject.transform.position.y < deadY)
        {
            this.gameObject.transform.position = responPos;
            Freeze();
            Invoke("UnFreeze", 0.1f);
        }    
    }

    void CreateParts()
    {
        float degree = 0.15f;
        parts = new List<SlingshotParticles>();
        for(int i = 0; i < 5; i++)
        {
            GameObject part = Instantiate(particlesPrefab, this.gameObject.transform);
            SlingshotParticles p = part.GetComponent<SlingshotParticles>();
            p.SetPosition(this.gameObject.transform.position);
            //p.ToggleRendererEnabled(false);
            p.SetOpacity(1 - (degree * (i + 1)));
            parts.Add(p);
        }
    }

    void OnMouseDown()
    {
        firstDragPosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        curDragPosision = Input.mousePosition;
        Debug.Log("------- Position Check -------");

    }

    void OnMouseUp()
    {
        rigid.AddForce(CalculateForce(), ForceMode2D.Impulse);


        //Release Mouse Positions
        firstDragPosition = Vector3.zero;
        curDragPosision = Vector3.zero;

    }

    Vector3 CalculateForce()
    {

        Vector3 diffVector = firstDragPosition - curDragPosision;
        float distance = diffVector.magnitude;
        float clampedForce = Mathf.Clamp(distance, minForce, maxForce);
        float force = Mathf.Log(distance + 1) * clampedForce; 
        

        diffVector.Normalize();

        return diffVector * force;
    }

    void Freeze()
    {
        rigid.isKinematic = true;
        rigid.velocity = Vector3.zero;
    }

    void UnFreeze()
    {
        rigid.isKinematic = false;
    }
}
