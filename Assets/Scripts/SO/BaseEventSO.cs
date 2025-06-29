using UnityEngine;
using UnityEngine.Events;
public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    public string description;

    public UnityAction<T> OnEventRaised;
    //������һ���¼�

    public string lastsender;

    public void RaiseEvent(T value,object sender)
    {//��������¼��ĺ���
        OnEventRaised?.Invoke(value);
        lastsender = sender.ToString();
    }

    //�����¼�ϵͳ��һ��ֻ��һ�����й㲥�������������Ƕ෽���

}
