//using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家採收
    /// </summary>
    public class PlayerHarvest : PlayerState
    {
        private FarmTile tile;
        public PlayerHarvest(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            tile = PlayerCheckFarm.instance.IsFramToHit().GetComponent<FarmTile>();
            player.ani.SetTrigger("觸發採集");
        }

        public override void Exit()
        {
            base.Exit();
            tile.PlantHarvest();       // 移除預置物
        }

        public override void Update()
        {
            base.Update();
            if (timer >= 0.7f) stateMachine.SwitchState(player.playerIdle);
        }
    }
}
