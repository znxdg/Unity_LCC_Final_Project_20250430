using UnityEditor;
using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家觸碰到農田後的狀態
    /// </summary>
    public class PlayerOnFarm : PlayerState
    {
        public PlayerOnFarm(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            // 如果面對農田並按下E鍵 就切換到 澆水狀態
            if (player.IsFram() && Input.GetKeyDown(KeyCode.E)) 
                stateMachine.SwitchState(player.playerWater);
        }
    }
}
