using UnityEngine;
using System.Collections;

public abstract class MeshBuilder : MonoBehaviour {

	[SerializeField] MeshRendererType rendererType = MeshRendererType.MeshFilter;
	[SerializeField] Material material;


	public Material SharedMaterial { 
		get { return material; } 
		set { 
			if(material != value) {
				material = value; 
				OnMaterialChanged();
			}
		} 
	}

	private MaterialPropertyBlock materialPropertyBlock;
	public MaterialPropertyBlock MaterialPropertyBlock { 
		get { 
			if(materialPropertyBlock == null) {
				materialPropertyBlock = new MaterialPropertyBlock ();

				if (meshRenderer != null) {
					meshRenderer.SetPropertyBlock (MaterialPropertyBlock);
				}
			}

			return materialPropertyBlock; 
		} 
	}

	public string SortingLayerName {
		get { return meshRenderer != null ? meshRenderer.sortingLayerName : ""; }
		set { if (meshRenderer != null) meshRenderer.sortingLayerName = value; }
	}
	public int SortingLayerID {
		get { return meshRenderer != null ? meshRenderer.sortingLayerID : 0; }
		set { if (meshRenderer != null) meshRenderer.sortingLayerID = value; }
	}
	public int SortingOrder {
		get { return meshRenderer != null ? meshRenderer.sortingOrder : 0; }
		set { if (meshRenderer != null) meshRenderer.sortingOrder = value; }
	}

	bool rebuild;
	Coroutine rebuildCoroutine;

	[System.NonSerialized] Mesh mesh1, mesh2;
	[System.NonSerialized] bool meshFlip;

	[System.NonSerialized] Mesh activeMesh;

	[System.NonSerialized] MeshFilter meshFilter;
	[System.NonSerialized] MeshRenderer meshRenderer;


	protected virtual void Awake () {
		if(material != null)
			OnMaterialChanged ();
	}
	
	
	
	protected virtual void OnMaterialChanged ()
	{
		if (meshRenderer != null) {
			meshRenderer.material = SharedMaterial;
			meshRenderer.SetPropertyBlock (MaterialPropertyBlock);
		}
	}

	protected virtual void OnEnable () {
		MarkForRebuild();

		if (rendererType == MeshRendererType.MeshFilter) {
			if (Application.isPlaying) {
				meshFilter = this.GetOrAddComponent<MeshFilter>(HideFlags.DontSave);
				meshRenderer = this.GetOrAddComponent<MeshRenderer>(HideFlags.DontSave);
			} else {
				meshFilter = this.GetComponent<MeshFilter>();
				meshRenderer = this.GetComponent<MeshRenderer>();
			}

			if (meshRenderer != null) {
				meshRenderer.enabled = true;
				meshRenderer.material = SharedMaterial;
				meshRenderer.SetPropertyBlock (MaterialPropertyBlock);
			}
		}
	}

	protected virtual void OnDisable () {
		if (meshRenderer != null) meshRenderer.enabled = false;
	}

	public virtual Mesh GetMesh (bool unique) {
	
		if(unique) {
			meshFlip = !meshFlip;
		}
		
		if(!Application.isPlaying) {

			if(mesh1 == null) {
				mesh1 = new Mesh() {
					hideFlags = HideFlags.DontSave,
					name		= string.Format("Editor mesh '{0}'", gameObject.name)
				};
			}

			return mesh1;
		} else {

			if(meshFlip) {

				if(mesh1 == null) {
					mesh1 = new Mesh() {
						hideFlags = HideFlags.DontSave,
						name		= string.Format("Mesh 1 '{0}'", gameObject.name)
					};
				}

				return mesh1;
			} else {

				if(mesh2 == null) {
					mesh2 = new Mesh() {
						hideFlags = HideFlags.DontSave,
						name		= string.Format("Mesh 2 '{0}'", gameObject.name)
					};
				}

				return mesh2;
			}
		}
	}

	protected virtual void LateUpdate () {
		if (!rebuild) CheckIfDirty ();
		RebuildIfDirty ();

		if (meshFilter == null || rendererType == MeshRendererType.GraphicsDraw) {
			Graphics.DrawMesh (
				activeMesh,
				transform.localToWorldMatrix,
				material,
				gameObject.layer, null, 0,
				MaterialPropertyBlock
			);
		}
	}

	public void ApplyMaterialPropertyBlock () {

		if (meshFilter != null) {
			meshRenderer.SetPropertyBlock (MaterialPropertyBlock);
		}
	}

	public void MarkForRebuild () {
		rebuild = true;
	}


	void RebuildIfDirty () {
	
		if(rebuild) {
			activeMesh = BuildMesh();
			rebuild = false;

			if (meshFilter != null) {
				meshFilter.mesh			= activeMesh;
			}
		}
	}

	protected virtual void CheckIfDirty () { }
	protected abstract Mesh BuildMesh ();

	public enum MeshRendererType {
		MeshFilter, 
		GraphicsDraw
	}
}


public abstract class MeshColorTextureBuilder : MeshBuilder {

	[SerializeField] Texture texture;
	[SerializeField] Color color = Color.white;

	public Texture Texture { 
		get { return texture; }
		set { 
			if (texture != value) {
				l_texture = texture = value;

				if (texture == null) {
					MaterialPropertyBlock.Clear ();
					MaterialPropertyBlock.SetColor("_Color", color);
				} else {
					MaterialPropertyBlock.SetTexture("_MainTex", texture);
				}
				
				ApplyMaterialPropertyBlock ();
			}
		} 
	}
	public Color Color  {
		get { return color; }
		set { 
			if(color != value) {
				l_color = color = value;
				MaterialPropertyBlock.SetColor("_Color", color);
				ApplyMaterialPropertyBlock ();
			}
		}
	}

	Texture l_texture;
	Color l_color;

	protected override void LateUpdate () {
		CheckPropertyChanges ();
		base.LateUpdate ();
	}

	protected override void OnMaterialChanged () {
		base.OnMaterialChanged ();
		CheckPropertyChanges (true);
	}

	protected virtual void CheckPropertyChanges (bool force = false) {
	
		if (force || l_texture != texture || l_color != color) {

			if (texture != null) {
				MaterialPropertyBlock.SetTexture ("_MainTex", texture);
			} else {
				MaterialPropertyBlock.Clear ();
			}
			
			MaterialPropertyBlock.SetColor("_Color", color);

			l_texture = texture;
			l_color = color;

			ApplyMaterialPropertyBlock ();
		}
	}
}