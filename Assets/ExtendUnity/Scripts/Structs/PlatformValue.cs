using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlatformValue<T> {

	public T apple;
	public T android;

    public T GetValue ()
    {
        #if UNITY_IOS
        return apple;
        #elif UNITY_ANDROID
        return android;
        #else
        return default (T);
        #endif
    }
}

[Serializable] public class PlatformString : PlatformValue<string> { }
[Serializable] public class PlatformInt : PlatformValue<int> { }
[Serializable] public class PlatformFloat : PlatformValue<float> { }
[Serializable] public class PlatformObject : PlatformValue<UnityEngine.Object> { }