using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] assets = Resources.FindObjectsOfTypeAll<T>();
                if(assets == null || assets.Length < 1) {
                    throw new System.Exception("Could not find any singleton scriptable object instances in the resources");
                }
                if(assets.Length > 1)
                {
                    throw new System.Exception("Results length is greater than 1 of " + typeof(T).ToString());
                }

                _instance = assets[0];
                _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }
            return _instance;
        }
    }

}