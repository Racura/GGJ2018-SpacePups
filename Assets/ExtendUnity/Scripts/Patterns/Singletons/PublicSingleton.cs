using UnityEngine;
using System.Collections;

public abstract class PublicSingleton<T> : Singleton<T>
	where T : PublicSingleton<T>
{
    public new static T Instance {
		get { return Singleton<T>.Instance; }
	}
}