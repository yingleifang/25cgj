using UnityEngine;
using System;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;//表示其监听的事件

    public UnityEvent<T> response;//一种和UnityAction类似的东西，能在inspector下面补充很多“订阅”事件的函数

    private void OnEnable()
    {
        if (eventSO != null)
        {
            eventSO.OnEventRaised += OnEventRaised;
        }
    }

    private void OnDisable()
    {
        if (eventSO != null)
        {
            eventSO.OnEventRaised -= OnEventRaised;
        }
    }

    private void OnEventRaised(T value)
    {
        
        response.Invoke(value);
        
    }
}
