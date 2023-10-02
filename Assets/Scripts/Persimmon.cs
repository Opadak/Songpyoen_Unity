using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persimmon : MonoBehaviour
{
    public bool collided;

    PathPoints pathPoints;

    void Awake()
    {
        pathPoints = FindAnyObjectByType<PathPoints>();
    }
    public void Release()
    {
        pathPoints.Clear();
        StartCoroutine(CreatePathPoints());
    }

    IEnumerator CreatePathPoints()
    {
        while (true)
        {
            if (collided) break;
            pathPoints.CreateCurrentPathPoint(transform.position);
            yield return new WaitForSeconds(pathPoints.timeInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (collision.gameObject.CompareTag("Songpyeon"))
        {
            GameManager.Instance.SetColliderEnterFieldObject(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            GameObject.Destroy(this.gameObject, 3f);
        }
    }
}