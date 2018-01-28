using System;
using UnityEngine;
using Zenject;

public class DependencyResolver : MonoBehaviour {

    Zenject.DiContainer container;


    public Zenject.DiContainer GetContainer ()
    {
        if (container == null)
        {
            Zenject.Context context = GameObject.FindObjectOfType <Zenject.SceneContext>();
            if (context == null) context = GameObject.FindObjectOfType <Zenject.ProjectContext>();
            if (context == null) context = GameObject.FindObjectOfType <Zenject.Context>();

            if (context != null) container = context.Container;
        }

        return container;
    }

    public T GetObject<T>() where T : class
    {
        var container = GetContainer ();

        if (container != null) return container.TryResolve<T>();

        return default (T);
    }

    public T GetObject<T>(object identifer) where T : class
    {
        var container = GetContainer ();

        T ouptut = null;

        if (ouptut == null && container != null) ouptut = container.TryResolveId<T>(identifer);
        if (ouptut == null && container != null) ouptut = container.TryResolve<T>();

        return ouptut;
    }




    static DependencyResolver resolver;
    public static DependencyResolver EnsureResolver () 
    {
        if (resolver == null) 
        {
            resolver = GameObject.FindObjectOfType<DependencyResolver>();
        }

        if (resolver == null)
        {
            var go = new GameObject ("Dependency Resolver", typeof (DependencyResolver));
            go.hideFlags = HideFlags.DontSave;

            resolver = go.GetComponent<DependencyResolver>();
        }

        return resolver;
    }

    public static T Get <T> () where T : class
    {
        var helper = EnsureResolver ();
        return helper.GetObject<T>();
    }

    public static T Get <T> (object identifer) where T : class
    {
        var helper = EnsureResolver ();
        return helper.GetObject<T>(identifer);
    }
}