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

            // 如果面對農田 處於能澆水狀態 並按下E鍵 就切換到 澆水狀態
            if ((PlayerCheckFarm.instance.IsFramToHit() != null) && Input.GetKeyDown(KeyCode.E) && player.canWater) 
                stateMachine.SwitchState(player.playerWater);
            // 如果面對農田 處於能種植狀態 並按下E鍵 就切換到 種植狀態
            if ((PlayerCheckFarm.instance.IsFramToHit() != null) && Input.GetKeyDown(KeyCode.E) && player.takeSeed)
                stateMachine.SwitchState(player.playerPlanting);
            // 如果面對農田 處於能採收狀態 並按下E鍵 就切換到 採收狀態
            if ((PlayerCheckFarm.instance.IsFramToHit() != null) && Input.GetKeyDown(KeyCode.E) 
                && player.canHarvest && plantGrowSystem.instance != null)
                if (plantGrowSystem.instance.currentStage >= (plantGrowSystem.instance.growthSprites.Length - 2))
                    stateMachine.SwitchState(player.playerHarvest);
        }
    }
}
