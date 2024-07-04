using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class,new()
{
    private T _instance;

    public T instance
    {
        get { 
            
            if(_instance == null)
            {
                _instance = new T();
            }
                return _instance; 
        }
    }
}

