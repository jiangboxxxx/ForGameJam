using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EventType
{
    BattleStart,PlayerTurn,EnemyTurn,PlayerActive,EnemyActive,PlayerEffectUpdate,EnemyEffectUpdate,BattleEnd
}

    public delegate void CallBack();
    public delegate void CallBack<T>(T arg);
    public delegate void CallBack<T, X>(T arg1,X arg2);
    public delegate void CallBack<T, X, Y>(T arg1, X arg2,Y arg3);

public class EventCenter : Singleton<EventCenter>
{
    private static Dictionary<EventType,Delegate> eventTable = new Dictionary<EventType,Delegate>();

    #region 添加和删除事件的判断
    private static void onListenerAdding(EventType eventType,Delegate callBack) {
        if (eventTable.ContainsKey(eventType) == false)
        {
            eventTable.Add(eventType, null);
        }
        Delegate d = eventTable[eventType];

        if (d != null && d.GetType()!=callBack.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
        }
    }

    private static void BeforeRemoving(EventType eventType, Delegate callBack)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
        }
    }
    private static void OnListenerRemoved(EventType eventType)
    {
        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }
    }
    
    #endregion

    #region 添加监听
    public static void AddListener(EventType eventType,CallBack callBack)
    {
        
        onListenerAdding(eventType, callBack);
        eventTable[eventType] = (CallBack)eventTable[eventType] + callBack;
    }
    public static void AddListener<T>(EventType eventType, CallBack<T> callBack)
    {

        onListenerAdding(eventType, callBack);
        eventTable[eventType] = (CallBack<T>)eventTable[eventType] + callBack;
    }
    public static void AddListener<T,X>(EventType eventType, CallBack<T,X> callBack)
    {

        onListenerAdding(eventType, callBack);
        eventTable[eventType] = (CallBack<T,X>)eventTable[eventType] + callBack;
    }
    public static void AddListener<T,X,Y>(EventType eventType, CallBack<T,X,Y> callBack)
    {

        onListenerAdding(eventType, callBack);
        eventTable[eventType] = (CallBack<T,X,Y>)eventTable[eventType] + callBack;
    }
    #endregion

    #region 删除监听
    public static void RemoveListener(EventType eventType,CallBack callBack)
    {
        BeforeRemoving(eventType, callBack);
        eventTable[eventType] = (CallBack)eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack)
    {
        BeforeRemoving(eventType, callBack);
        eventTable[eventType] = (CallBack<T>)eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T,X>(EventType eventType, CallBack<T, X> callBack)
    {
        BeforeRemoving(eventType, callBack);
        eventTable[eventType] = (CallBack<T, X>)eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T, X, Y>(EventType eventType, CallBack<T, X, Y> callBack)
    {
        BeforeRemoving(eventType, callBack);
        eventTable[eventType] = (CallBack<T, X, Y>)eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    #endregion

    #region 事件广播
    public static void BroadCast(EventType eventType)
    {
        Delegate d;
            if(eventTable.TryGetValue(eventType,out d))
        {
            CallBack callBack = d as CallBack;//获得对应的委托事件
            if (callBack != null)
            {
                callBack();//实现委托方法
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void BroadCast<T>(EventType eventType,T arg)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;//获得对应的委托事件
            if (callBack != null)
            {
                callBack(arg);//实现委托方法
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void BroadCast<T,X>(EventType eventType,T arg1,X arg2)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T,X> callBack = d as CallBack<T, X>;//获得对应的委托事件
            if (callBack != null)
            {
                callBack(arg1,arg2);//实现委托方法
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void BroadCast<T,X,Y>(EventType eventType, T arg1, X arg2,Y arg3)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T,X,Y> callBack = d as CallBack<T,X,Y>;//获得对应的委托事件
            if (callBack != null)
            {
                callBack(arg1,arg2,arg3);//实现委托方法
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    #endregion
}
