using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject level1;
    [SerializeField] PolygonCollider2D level1Collider;
    [SerializeField] GameObject airwall1;

    [SerializeField] GameObject level2;
    [SerializeField] GameObject airwall2;
    [SerializeField] PolygonCollider2D level2Collider;

    [SerializeField] GameObject level3;
    [SerializeField] PolygonCollider2D level3Collider;
    [SerializeField] GameObject airwall3;

    [SerializeField] GameObject level4;
    [SerializeField] PolygonCollider2D level4Collider;

    [SerializeField] CinemachineConfiner2D _confiner;

    public Transform[] cutscenePoints;      // ¡û drag any empty GameObjects here
    public CinemachineVirtualCamera cutsceneCam;   // CM-Cutscene

    public void LoadLevel1()
    {
        level1.SetActive(true);
        _confiner.m_BoundingShape2D = level1Collider;
        _confiner.InvalidateCache();
    }

public void LoadLevel2()
    {
        level2.SetActive(true);
        _confiner.m_BoundingShape2D = level2Collider;
        _confiner.InvalidateCache();
        Destroy(airwall1);
        cutsceneCam.transform.position = cutscenePoints[0].position;
        GetComponent<SimpleCutsceneTrigger>().PlayCutscene();
    }

    public void LoadLevel3()
    {
        level3.SetActive(true);
        _confiner.m_BoundingShape2D = level3Collider;
        _confiner.InvalidateCache();
        Destroy(airwall2);
        cutsceneCam.transform.position = cutscenePoints[1].position;
        GetComponent<SimpleCutsceneTrigger>().PlayCutscene();
    }

    public void LoadLevel4()
    {
        level4.SetActive(true);
        _confiner.m_BoundingShape2D = level4Collider;
        _confiner.InvalidateCache();
        Destroy(airwall3);
        cutsceneCam.transform.position = cutscenePoints[2].position;
        GetComponent<SimpleCutsceneTrigger>().PlayCutscene2();
    }
}
