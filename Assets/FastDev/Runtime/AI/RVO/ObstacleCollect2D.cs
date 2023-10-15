using RVO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = RVO.Vector2;
namespace FastDev
{
    public class ObstacleCollect2D : MonoBehaviour
    {
        void Awake()
        {
            Collider2D[] boxColliders = GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < boxColliders.Length; i++)
            {
                IList<Vector2> obstacle = new List<Vector2>();
                obstacle.Add(new Vector2(boxColliders[i].bounds.min.x, boxColliders[i].bounds.max.y));
                obstacle.Add(new Vector2(boxColliders[i].bounds.min.x, boxColliders[i].bounds.min.y));
                obstacle.Add(new Vector2(boxColliders[i].bounds.max.x, boxColliders[i].bounds.min.y));
                obstacle.Add(new Vector2(boxColliders[i].bounds.max.x, boxColliders[i].bounds.max.y));
                Simulator.Instance.addObstacle(obstacle);
            }
        }
    }
}
