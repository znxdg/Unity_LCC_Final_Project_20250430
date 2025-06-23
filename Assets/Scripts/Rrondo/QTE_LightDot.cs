using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rrondo
{
    public class QTE_LightDot : MonoBehaviour
    {
        public List<Transform> pathPoints;
        public float moveSpeed = 3f;

        public IEnumerator MoveAlongPath(Func<int, bool> shouldTriggerQTE, Func<int, IEnumerator> qteAction)
        {
            if (pathPoints == null || pathPoints.Count < 2)
                yield break;

            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                Vector3 start = pathPoints[i].position;
                Vector3 end = pathPoints[i + 1].position;
                float distance = Vector3.Distance(start, end);
                float t = 0f;

                while (t < 1f)
                {
                    t += Time.deltaTime * moveSpeed / distance;
                    transform.position = Vector3.Lerp(start, end, t);
                    yield return null;
                }

                if (shouldTriggerQTE(i))
                {
                    yield return qteAction(i);
                }
            }
        }
    }
}
