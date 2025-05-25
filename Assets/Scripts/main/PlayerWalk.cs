using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家走路
    /// </summary>
    public class PlayerWalk : PlayerState
    {
        public PlayerWalk(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
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
            player.rig.linearVelocity =
                new Vector2(player.hor_value * player.moveSpeed,
                player.ver_value * player.moveSpeed);
            player.ani.SetFloat("方向", player.way_value);
            player.ani.SetFloat("動作模式", 0.5f);
            // 玩家翻面
            player.Flip(player.hor_value);

            if (Mathf.Abs(player.hor_value) == 0 && Mathf.Abs(player.ver_value) == 0) 
                stateMachine.SwitchState(player.playerIdle);
        }
    }
}
