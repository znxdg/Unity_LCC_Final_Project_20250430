using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家種植
    /// </summary>
    public class PlayerPlanting : PlayerState
    {
        private FarmTile tile;
        public PlayerPlanting(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            tile = PlayerCheckFarm.instance.IsFramToHit().GetComponent<FarmTile>();
            if (!tile.isPlanting) player.ani.SetTrigger("觸發種植");
        }

        public override void Exit()
        {
            base.Exit();
            tile.PlantCrop();
        }

        public override void Update()
        {
            base.Update();
            if (timer >= 0.7f) stateMachine.SwitchState(player.playerIdle);
        }
    }
}
