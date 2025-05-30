using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家待機
    /// </summary>
    public class PlayerIdle : PlayerOnFarm
    {
        public PlayerIdle(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetFloat("動作模式", 0);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if(!player.canMove) return;
            // Print.Text($"玩家=> 水平軸:{hor_value}；垂直軸:{ver_value}", "#f93");
            if (Mathf.Abs(player.hor_value) > 0.1f || Mathf.Abs(player.ver_value) > 0.1f)
                stateMachine.SwitchState(player.playerWalk);
        }
    }
}
