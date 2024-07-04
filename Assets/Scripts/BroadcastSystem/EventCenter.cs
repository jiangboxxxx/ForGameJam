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

    #region ��Ӻ�ɾ���¼����ж�
    private static void onListenerAdding(EventType eventType,Delegate callBack) {
        if (eventTable.ContainsKey(eventType) == false)
        {
            eventTable.Add(eventType, null);
        }
        Delegate d = eventTable[eventType];

        if (d != null && d.GetType()!=callBack.GetType())
        {
            throw new Exception(string.Format("����Ϊ�¼�{0}��Ӳ�ͬ���͵�ί�У���ǰ�¼�����Ӧ��ί����{1}��Ҫ��ӵ�ί������Ϊ{2}", eventType, d.GetType(), callBack.GetType()));
        }
    }

    private static void BeforeRemoving(EventType eventType, Delegate callBack)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("�Ƴ����������¼�{0}û�ж�Ӧ��ί��", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("�Ƴ��������󣺳���Ϊ�¼�{0}�Ƴ���ͬ���͵�ί�У���ǰί������Ϊ{1}��Ҫ�Ƴ���ί������Ϊ{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("�Ƴ���������û���¼���{0}", eventType));
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

    #region ��Ӽ���
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

    #region ɾ������
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

    #region �¼��㲥
    public static void BroadCast(EventType eventType)
    {
        Delegate d;
            if(eventTable.TryGetValue(eventType,out d))
        {
            CallBack callBack = d as CallBack;//��ö�Ӧ��ί���¼�
            if (callBack != null)
            {
                callBack();//ʵ��ί�з���
            }
            else
            {
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
            }
        }
    }
    public static void BroadCast<T>(EventType eventType,T arg)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;//��ö�Ӧ��ί���¼�
            if (callBack != null)
            {
                callBack(arg);//ʵ��ί�з���
            }
            else
            {
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
            }
        }
    }
    public static void BroadCast<T,X>(EventType eventType,T arg1,X arg2)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T,X> callBack = d as CallBack<T, X>;//��ö�Ӧ��ί���¼�
            if (callBack != null)
            {
                callBack(arg1,arg2);//ʵ��ί�з���
            }
            else
            {
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
            }
        }
    }
    public static void BroadCast<T,X,Y>(EventType eventType, T arg1, X arg2,Y arg3)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T,X,Y> callBack = d as CallBack<T,X,Y>;//��ö�Ӧ��ί���¼�
            if (callBack != null)
            {
                callBack(arg1,arg2,arg3);//ʵ��ί�з���
            }
            else
            {
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
            }
        }
    }
    #endregion
}
