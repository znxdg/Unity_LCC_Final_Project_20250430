using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 狀態
    /// </summary>
    public class State
    {
        protected string name;
        protected StateMachine stateMachine;
        protected float timer;

        public virtual void Enter()
        {
            Print.Text($"進入「{name}」狀態", "#6f6");
            timer = 0;
        }
        
        public virtual void Update()
        {
            Print.Text($"更新「{name}」狀態", "#ff3");
            timer += Time.deltaTime;    // 累加一幀的時間
        }

        public virtual void Exit()
        {
            Print.Text($"離開「{name}」狀態", "#f66");
        }
    }
}
