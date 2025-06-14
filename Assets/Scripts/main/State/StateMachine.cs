//using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 狀態機
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// 目前的狀態
        /// </summary>
        private State curentState;
        /// <summary>
        /// 初始狀態：指定預設的狀態
        /// </summary>
        /// <param name="state">預設值狀態</param>
        public void Initialize(State state)
        {
            curentState = state;
            curentState.Enter();
        }

        public void Update()
        {
            curentState.Update();
        }

        public void SwitchState(State newState)
        {
            curentState.Exit();
            curentState = newState;
            curentState.Enter();
        }
    }
}
