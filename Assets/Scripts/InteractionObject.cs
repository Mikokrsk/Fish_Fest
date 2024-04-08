using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void OnMouseEnter()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 255);
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = new Color(0, 0, 0, 0);
    }
}