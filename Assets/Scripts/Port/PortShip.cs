using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortShip : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2d;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void OnMouseEnter()
    {
        _spriteRenderer.color = Color.gray;
    }
    private void OnMouseDown()
    {
        if (NPCDialogueManager.Instance.currentDialogueTree == null)
        {
            _spriteRenderer.color = Color.white;
            LoadLevelManager.Instance.LoadLevel("Ocean");
        }
    }
    private void OnMouseExit()
    {
        _spriteRenderer.color = Color.white;
    }
}
