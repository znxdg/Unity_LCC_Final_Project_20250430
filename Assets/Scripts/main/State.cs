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
        public virtual void Enter()
        {
            Print.Text($"進入「{name}」狀態", "#6f6");
        }
        
        public virtual void Update()
        {
            Print.Text($"更新「{name}」狀態", "#ff3");
        }

        public virtual void Exit()
        {
            Print.Text($"離開「{name}」狀態", "#f66");
        }
    }
}
