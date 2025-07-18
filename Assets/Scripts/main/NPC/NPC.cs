﻿using UnityEngine;
using System.Collections;
using Fungus;
using Rrondo;

namespace YuCheng
{

    public class NPC : MonoBehaviour
    {
        [SerializeField]
        private WorldToUI worldToUIInteraction;
        [SerializeField]
        private float uiOffset_X;
        [SerializeField]
        private float uiOffset_Y;
        [field: SerializeField, Header("Fungus 物件")]
        public Flowchart fungus {  get; private set; }
        [SerializeField]
        public IngredientConfig item;

        [SerializeField]
        Fungus_Item triggerMedicine;

        public int decoctionCount {  get; private set; }
        public int needDecoctionCount { get; private set; } = 1;

        private SpriteRenderer sr;                  // NPC本身
        public Animator ani {  get; private set; }

        private StateMachine stateMachine;

        public bool isEnter { get; set; }           // 判斷角色是否進入互動範圍
        public NpcMissionBefore before { get; private set; }
        public NpcMissioning missioning { get; private set; }
        public NpcMissionComplete complete { get; private set; }

        private void Awake()
        {
            ani = GetComponent<Animator>();
            stateMachine = new StateMachine();
            sr = GetComponent<SpriteRenderer>();

            before = new NpcMissionBefore(this, stateMachine, $"{name} 接任務前");
            missioning = new NpcMissioning(this, stateMachine, $"{name} 任務進行中");
            complete = new NpcMissionComplete(this, stateMachine, $"{name} 任務完成");
            if (!triggerMedicine.fungusShowState)
            {
                stateMachine.Initialize(before);
            }
            else
            {
                stateMachine.Initialize(missioning);
            }
        }

        private void Start()
        {
            StartCoroutine(NPC_FadeSystem(sr, 1f, 0, 0.9f));    // NPC淡入場景
        }

        private void Update()
        {
            stateMachine.Update();
            worldToUIInteraction.UpdateUIPoint(transform, uiOffset_X, uiOffset_Y);
#if UNITY_EDITOR
            TestMissionComplete();
#endif
        }

        /// <summary>
        /// 控制物件淡入淡出
        /// </summary>
        /// <param name="sr">物件的SpriteRenderer</param>
        /// <param name="duration">淡入淡出花費時間</param>
        /// <param name="startAlpha">起始的透明度，範圍0 ~ 1</param>
        /// <param name="endAlpha">結束的透明度，範圍0 ~ 1</param>
        /// <returns></returns>
        IEnumerator NPC_FadeSystem(SpriteRenderer sr, float duration, float startAlpha, float endAlpha)
        {
            float time = 0f;
            while (time < duration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
                time += Time.deltaTime;
                yield return null;
            }
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, endAlpha);
        }

        public void InteractionButton(CanvasGroup groupInteraction, bool isEnter)
        {
            if (isEnter)
            {
                StartCoroutine(FadeSystem.Fade(groupInteraction, true));
            }
            else
            {
                StartCoroutine(FadeSystem.Fade(groupInteraction, false));
            }
        }

#if UNITY_EDITOR
        private void TestMissionComplete()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0)) decoctionCount = needDecoctionCount;
        }
#endif
    }
}
