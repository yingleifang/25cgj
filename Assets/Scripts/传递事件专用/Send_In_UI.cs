using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Send_In_UI : MonoBehaviour
{
    public ObjectEventSO In_UI;
    void Start()
    {
        In_UI.RaiseEvent(null, this);
        Debug.Log("������ҳ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
