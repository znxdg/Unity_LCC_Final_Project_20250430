using UnityEngine;

namespace YuCheng
{

    public class PlayerWater : PlayerState
    {
        private FarmTile tile;

        public PlayerWater(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            tile = PlayerCheckFarm.instance.IsFramToHit().GetComponent<FarmTile>();
            base.Enter();
            if (!tile.isWatered) player.ani.SetTrigger("觸發澆水");
        }

        public override void Exit()
        {
            base.Exit();
            if (tile != null && !tile.isWatered) tile.Water();
        }

        public override void Update()
        {
            base.Update();
            if (timer >= 0.7f) stateMachine.SwitchState(player.playerIdle);
        }
    }
}
