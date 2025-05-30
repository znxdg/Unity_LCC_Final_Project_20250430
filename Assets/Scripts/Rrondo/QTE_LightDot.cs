using UnityEngine;
using System.Collections.Generic;

namespace Rrondo
{
    public class QTE_LightDot : MonoBehaviour
    {
        [Header("設定光點行走的路徑點")]
        public List<Transform> pathPoints;

        [Header("移動速度")]
        public float speed = 3f;

        private int currentIndex = 0;

        void Update()
        {
            if (pathPoints == null || pathPoints.Count == 0) return;

            Transform targetPoint = pathPoints[currentIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
            {
                currentIndex = (currentIndex + 1) % pathPoints.Count; // 繞圈
            }
        }
    }

}

