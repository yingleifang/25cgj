using UnityEngine;
using System;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;//��ʾ��������¼�

    public UnityEvent<T> response;//һ�ֺ�UnityAction���ƵĶ���������inspector���油��ܶࡰ���ġ��¼��ĺ���

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
