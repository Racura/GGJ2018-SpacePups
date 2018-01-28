using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour
	where T : Singleton<T>
{
    public static bool HasInstance { get { return instance != null; } }

    public bool IsInstance { get { return this == instance; } }

    static T instance;
	protected static T Instance {
		get {
			if(instance == null){
				instance = FindObjectOfType<T>();
			}
			return instance;
		}
	}
    
    public static void Create()
    {
        var name = typeof(T).Name;

        if (instance != null) {
            Debug.LogWarning(name + " is not null, creating new instance");
        }

        var gameObject = new GameObject(name, typeof(T)) {
			hideFlags = HideFlags.DontSaveInBuild,
        };

        instance = gameObject.GetComponent<T>();
    }

	protected virtual void Awake () {
		instance = this as T;
	}
	
	protected virtual void OnDestroy () {
		if(instance == this) {
			instance = null;
		}
	}
}