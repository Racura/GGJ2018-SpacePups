using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoSpriteController : MonoBehaviour {

    [Zenject.Inject] Zenject.DiContainer m_container;

    [Zenject.Inject] IGridManager m_grid;
    [SerializeField] Doggo m_doggo;

    [SerializeField] DoggoSprite head;
    [SerializeField] DoggoSprite[] body;
    [SerializeField] DoggoSprite tail;
    
    
    List<DoggoSprite> m_sprites;

	// Use this for initialization
	protected virtual void OnEnable () {
		RebuildSprites ();
	}
	protected virtual void LateUpdate () {
		RebuildSprites ();
	}
	protected virtual void OnDisable () {
		
	}

    public void RebuildSprites () {

        var path    = m_doggo.Coords;
        var dir     = m_doggo.Direction;

        if (path == null) return;


        var grid    = m_grid.UnityGrid;
        var prime   = m_doggo.PrimaryCoord;

        var pos     = grid.GetCellCenterWorld (new Vector3Int(prime.x, prime.y, 0));

        bool flipped = false;

        GenBodySprites (path.Length - 2);
    
        for (int i = 0; i < path.Length; ++i) {
            
            if (i == 0) {
                flipped = dir.x == 0 ? head.Flipped :  dir.x < 0;
            }

            DoggoSprite spr = null;

            if (i == 0) {
                spr = head;
            } else if (i == path.Length - 1) {
                 spr = tail;
            } else {
                if (m_sprites != null && i - 1 < m_sprites.Count)
                    spr = m_sprites[i - 1];
            }

            if (spr == null) continue;

            spr.SetFlipped (flipped);
            spr.SetIndex (i);
            spr.SetDoggo (m_doggo);

            spr.UpdateSprite ();

            var thisPos = grid.GetCellCenterWorld (new Vector3Int(path[i].x, path[i].y, 0));
            spr.transform.localPosition = thisPos - pos;

            spr.gameObject.SetActive (true);
        }

        for (int i = path.Length - 2; i < m_sprites.Count; ++i) {
            if (i < 0) continue;
            m_sprites[i].gameObject.SetActive (false);
        }
    }

    private void GenBodySprites(int count)
    {
        if (m_sprites == null) {
            m_sprites = new List<DoggoSprite> (body);
        }

        if (body.Length <= 0) return;

        while (m_sprites.Count < count) {

            var spr = body[m_sprites.Count % body.Length];
            if (spr == null) break;

            var inst = GameObject.Instantiate (spr, spr.transform.parent, false);
            m_container.InjectGameObject (inst.gameObject);

            m_sprites.Add (inst);

            
        }
    }
}
