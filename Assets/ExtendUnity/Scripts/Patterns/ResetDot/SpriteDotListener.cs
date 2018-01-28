using UnityEngine;
using System.Collections;
using System;

public class SpriteDotListener : DotListener
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool hideOnRetire = false;

    public SpriteRenderer SpriteRenderer {
        get { return spriteRenderer; } 
    }


    protected override bool IsListenerActive { get { return false; } }

    public override void DotStateChanged (ResetDot.States state)
    {
        base.DotStateChanged (state);

        switch (state)
        {
            case ResetDot.States.Active:
                if (spriteRenderer != null) spriteRenderer.enabled = true;
                break;
            case ResetDot.States.Retiring:
                if (hideOnRetire && spriteRenderer != null) 
                    spriteRenderer.enabled = false;
                break;
            default:
                if (spriteRenderer != null) spriteRenderer.enabled = false;
                break;
        }
    }
}   
