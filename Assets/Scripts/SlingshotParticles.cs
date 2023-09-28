using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotParticles : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetOpacity(float opacity)
    {
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp01(opacity); 
        spriteRenderer.color = color;
    }

    public void SetPosition(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }

    public void ToggleRendererEnabled(bool isEnabled)
    {
        if(spriteRenderer.enabled != isEnabled)
        {
            spriteRenderer.enabled = isEnabled;
        }
    }

}
