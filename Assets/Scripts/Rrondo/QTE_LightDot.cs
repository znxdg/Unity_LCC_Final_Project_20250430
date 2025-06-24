using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rrondo
{
    public class QTE_LightDot : MonoBehaviour
    {
        public List<Transform> pathPoints;
        public float speed = 300f;  // UI單位建議用大一點
        public RectTransform rectTransform;

        private void Awake()
        {
            if (rectTransform == null) 
                rectTransform = GetComponent<RectTransform>();
        }
        public IEnumerator MoveAlongPath(Func<int, bool> shouldTriggerQTE, Func<int, IEnumerator> onQTETrigger)
        {
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                Vector3 start = pathPoints[i].position;
                Vector3 end = pathPoints[i + 1].position;
                float t = 0f;

                while (t < 1f)
                {
                    t += Time.deltaTime * speed / Vector3.Distance(start, end);
                    rectTransform.position = Vector3.Lerp(start, end, t);
                    yield return null;
                }

                if (shouldTriggerQTE?.Invoke(i) == true)
                    yield return onQTETrigger?.Invoke(i);
            }
        }
    }
}
