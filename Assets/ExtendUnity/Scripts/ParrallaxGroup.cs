using System;
using UnityEngine;

public class ParrallaxGroup : MonoBehaviour {

	[SerializeField] new Camera camera;
	[SerializeField] float scrollAmount = 1f;


	[SerializeField] bool scrollPosition = true;
	[SerializeField] bool scrollScale = false;

	Vector2 lastPosition;
	float lastSize;


	protected virtual void OnEnable ()
	{
		if(camera == null) SetCamera(null);
			
		lastPosition	= camera.transform.position;
		lastSize		= camera.orthographicSize;
	}

	public void SetCamera(Camera camera) {

		if(camera == null) camera = Camera.main;
		if(camera == this.camera) return;

		this.camera		= camera;

		lastPosition	= camera.transform.position;
		lastSize		= camera.orthographicSize;	
	}

	protected virtual void LateUpdate ()
	{
		if(camera != null) {
			if(scrollPosition) {
				var dif = (Vector2)camera.transform.position - lastPosition;
				transform.Translate( dif * (1 - scrollAmount) );
			}

			if(scrollScale && camera.orthographic) {
				var dif = camera.orthographicSize / lastSize;
				transform.localScale = Vector3.Scale(
					transform.localScale, Vector2.one * (dif * (1 - scrollAmount) + scrollAmount)
				);
			}

			lastPosition	= camera.transform.position;
			lastSize		= camera.orthographicSize;
		}
	}
}

