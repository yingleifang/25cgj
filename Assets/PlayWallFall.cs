using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWallFall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var anim = GetComponent<Animator>();

        // ���´�ͷ���� clipName��layer=0, normalizedTime=0��
        anim.Play("WallFallAnim", 0, 0f);

    }

}
