using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家種植
    /// </summary>
    public class PlayerPlanting : PlayerState
    {
        public PlayerPlanting(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("觸發種植");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (timer >= 0.7f) stateMachine.SwitchState(player.playerIdle);
        }
    }
}
