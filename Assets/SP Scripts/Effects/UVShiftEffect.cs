using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class UVShiftEffect : MonoBehaviour {

    [SerializeField] Material mat;
    void OnRenderImage(RenderTexture src, RenderTexture dest) {

        if (mat != null) Graphics.Blit(src, dest, mat);
        else Graphics.Blit(src, dest);
    }
}
