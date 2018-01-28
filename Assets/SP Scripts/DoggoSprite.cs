using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoSprite : MonoBehaviour {

    [Zenject.Inject] IGridManager m_grid;


    [SerializeField] SpriteRenderer m_spriteRenderer;

    [SerializeField] Sprite m_default;
    [SerializeField] Sprite m_fromUp;
    [SerializeField] Sprite m_fromDown;


    int m_index;
    bool m_flipped;
    Doggo m_doggo;
    public int Index { get { return m_index; } }
    public bool Flipped { get { return m_flipped; } }


    public void SetDoggo (Doggo doggo) {
        m_doggo = doggo;
    }

    public void SetIndex (int index) {
        m_index = index;
    }

    public void SetFlipped (bool flipped) {
        m_flipped = flipped;
        transform.localScale = new Vector3 (flipped ? -1 : 1, 1, 1);
    }

    public void UpdateSprite () {

        if (m_doggo == null || !m_doggo.IsActive) return;
        if (m_doggo.Coords == null) return;

        var dir     = m_doggo.Direction;
        var path    = m_doggo.Coords;

        var coord   = path[Index];

        var prevDir = (Index <= 0) 
            ? dir 
            : path[Index - 1] - coord;
        var nextDir = (Index >= path.Length - 1) 
            ? prevDir * -1
            : coord - path[Index + 1];
            

        float angle = Flipped ? 180 : 0;

        if (prevDir.x != 0) angle += (prevDir.x > 0) ? 0 : 180;
        if (prevDir.y != 0) angle += (prevDir.y > 0) ? 90 : 270;

        Sprite sprite = null;

        if (prevDir.x == -nextDir.y && prevDir.y == nextDir.x) 
            sprite = Flipped ? m_fromDown : m_fromUp;

        if (prevDir.x == nextDir.y && prevDir.y == -nextDir.x) 
            sprite = Flipped ? m_fromUp : m_fromDown;

        if (sprite == null) sprite = m_default;

        if (m_spriteRenderer != null) {
            m_spriteRenderer.sprite = sprite;
        }

        transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
    }
}
