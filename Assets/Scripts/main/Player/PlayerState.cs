//using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家狀態
    /// </summary>
    public class PlayerState : State
    {
        protected Player player;
        public PlayerState(Player _player, StateMachine _stateMachine, string _name)
        {
            player = _player;
            stateMachine = _stateMachine;
            name = _name;
        }

        public override void Update()
        {
            base.Update();
            if (!player.canMove) Print.Text(player.canMove);
        }
    }
}
