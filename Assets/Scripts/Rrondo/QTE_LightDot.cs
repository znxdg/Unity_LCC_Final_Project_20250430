using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rrondo
{
    public class QTE_LightDot : MonoBehaviour
    {
        [Header("光點路徑")]
        public List<Transform> pathPoints;

        [Header("光點移動時間（秒）")]
        public float totalMoveDuration = 6f;

        /// <summary>
        /// 控制光點沿路徑移動，並在符合條件的節點觸發事件
        /// </summary>
        /// <param name="shouldTriggerQTE">給定節點 index 判斷是否觸發 QTE</param>
        /// <param name="onTriggerQTE">實際觸發 QTE 的協程</param>
        public IEnumerator MoveAlongPath(Func<int, bool> shouldTriggerQTE, Func<int, IEnumerator> onTriggerQTE)
        {
            if (pathPoints == null || pathPoints.Count < 2)
            {
                Debug.LogError("QTE_LightDot：pathPoints 資料不足");
                yield break;
            }

            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                Vector3 start = pathPoints[i].position;
                Vector3 end = pathPoints[i + 1].position;
                float t = 0f;
                float durationPerSegment = totalMoveDuration / (pathPoints.Count - 1);

                while (t < 1f)
                {
                    t += Time.deltaTime / durationPerSegment;
                    transform.position = Vector3.Lerp(start, end, t);
                    yield return null;
                }

                // 到達節點後判斷是否觸發 QTE
                if (shouldTriggerQTE != null && shouldTriggerQTE(i))
                {
                    if (onTriggerQTE != null)
                    {
                        yield return onTriggerQTE(i);
                    }
                }
            }
        }
    }
}

