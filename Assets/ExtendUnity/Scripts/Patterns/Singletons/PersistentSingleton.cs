using UnityEngine;

public abstract class PersistentSingleton<T> : Singleton<T>
    where T : PersistentSingleton<T>
{	
	protected override void Awake () {
		if(!HasInstance || Instance == this) {
            base.Awake();
			GameObject.DontDestroyOnLoad(this.gameObject);
		} else {
			GameObject.DestroyObject(this.gameObject);
		}
	}
}