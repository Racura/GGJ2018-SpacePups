
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    protected virtual void OnEnable () {
        var parent = GetComponentInParent<ITintObject>();


        int tint = parent != null ? parent.Tint : 0;

        if (spriteRenderer != null && tint >= 0 && tint < sprites.Length) {
            spriteRenderer.sprite = sprites [tint];
        }


    }
}
