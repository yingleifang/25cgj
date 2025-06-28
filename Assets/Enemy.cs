//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Pathfinding;

//public class Enemy : MonoBehaviour
//{
//    [SerializeField]
//    private Transform player;

//    private Seeker seeker;
//    private List<Vector3> pathPointList;
//    private int currentIndex = 0;
//    private float pathGenerationInterval = 0.5f;//0.5秒生成一次路径
//    private float pathGenerateTimer = 0f;

//    private void Awake()
//    {
//        seeker = GetComponent<Seeker>();
//    }

//    private void Update()
//    {
        
//    }

//    private void AutoPath()
//    {
//        if (pathPointList == null || pathPointList.Count <= 0)
//        {
//            GeneratePath(player.position);
//        }
//    }
//    //获取路径点
//    private void GeneratePath(Vector3 target)
//    {
//        currentIndex = 0;
//        seeker.StartPath(transform.position, target, Path =>
//        {
//            pathPointList = Path.vectorPath;
//        });
//    }
//}
