using UnityEngine;
using UnityEngine.Events;
public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    public string description;

    public UnityAction<T> OnEventRaised;
    //声明了一个事件

    public string lastsender;

    public void RaiseEvent(T value,object sender)
    {//启动这个事件的函数
        OnEventRaised?.Invoke(value);
        lastsender = sender.ToString();
    }

    //关于事件系统，一般只有一个进行广播，而监听者则是多方面的

}
