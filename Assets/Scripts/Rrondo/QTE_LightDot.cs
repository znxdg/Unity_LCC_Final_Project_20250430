using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Rrondo
{
    public class QTE_LightDot : MonoBehaviour
    {

        public List<Transform> pathPoints;    // 移動路徑點
        public float segmentMoveTime = 1f;    // 每段花多少時間移動


        /// <summary>
        /// 沿著路徑移動，並在每段結束時觸發回頭呼叫
        /// </summary>
        /// <param name="shouldTriggerQTE"></param>
        /// <param name="onQTE"></param>
        /// <returns></returns>
        public IEnumerator MoveAlongPath(System.Func<int, bool> shouldTriggerQTE, System.Func<int, IEnumerator> onQTE)
        {
            if (pathPoints == null || pathPoints.Count < 2)
                yield break;

            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                Vector3 start = pathPoints[i].position;
                Vector3 end = pathPoints[i + 1].position;
                float timer = 0f;

                while (timer < segmentMoveTime)
                {
                    timer += Time.deltaTime;
                    float t = Mathf.Clamp01(timer / segmentMoveTime);
                    transform.position = Vector3.Lerp(start, end, t);
                    yield return null;
                }

                if (shouldTriggerQTE != null && shouldTriggerQTE(i))
                {
                    yield return onQTE(i);
                }
            }
        }

        public void ResetPosition()
        {
            if (pathPoints != null && pathPoints.Count > 0)
            {
                transform.position = pathPoints[0].position;
            }
        }
    }

}

