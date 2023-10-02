using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;

    bool isMouseDown;

    public GameObject persimmonPrefab;

    public float persimmonPositionOffset;

    Rigidbody2D persimmon;
    Collider2D persimmonCollider;

    public float force;

    public bool CanCreate { get; set; }
    void Awake()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CanCreate = true;
        CreatePersimmon();
    }

    public void CreatePersimmon()
    {
        if (persimmon != null || !CanCreate)
            return;

        persimmon = Instantiate(persimmonPrefab).GetComponent<Rigidbody2D>();
        persimmonCollider = persimmon.GetComponent<Collider2D>();
        persimmonCollider.enabled = false;

        persimmon.isKinematic = true;

        ResetStrips();
    }

    public GameObject CheckSongPyeon()
    {
        if (persimmon != null)
            return persimmon.gameObject;
        return null;
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition
                - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (persimmonCollider)
            {
                persimmonCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
        currentPosition = idlePosition.position;
    }

    void Shoot()
    {
        if (persimmon == null)
            return;
        persimmon.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        persimmon.velocity = birdForce;

        persimmon.GetComponent<Persimmon>().Release();

        persimmon = null;
        persimmonCollider = null;
        GameManager.Instance.DeleteIcon();
        Invoke("CreatePersimmon", 2);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (persimmon)
        {
            Vector3 dir = position - center.position;
            persimmon.transform.position = position + dir.normalized * persimmonPositionOffset;
            persimmon.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}
