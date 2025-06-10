//using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace YuCheng
{
    /// <summary>
    /// 玩家觸碰到農田後的狀態
    /// </summary>
    public class PlayerOnFarm : PlayerState
    {
        GameObject FarmObject;
        FarmTile farmTile;

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
            FarmObject = PlayerCheckFarm.instance.IsFramToHit();
            Print.Text(FarmObject);
            // 如果玩家面對農田
            if (FarmObject != null)
            {
                farmTile = FarmObject.GetComponent<FarmTile>();
                // 處於能澆水狀態 並按下空白鍵 就切換到 澆水狀態
                if (Input.GetKeyDown(KeyCode.Space) && player.canWater)
                    stateMachine.SwitchState(player.playerWater);
                // 處於能種植狀態 並按下E鍵 就切換到 種植狀態
                if (Input.GetKeyDown(KeyCode.E) && (!farmTile.isPlanting) && player.takeSeed)
                    stateMachine.SwitchState(player.playerPlanting);
                // 按下E鍵 就切換到 採收狀態
                if (Input.GetKeyDown(KeyCode.E) && farmTile.canHarvest)
                    stateMachine.SwitchState(player.playerHarvest);
            }
        }
    }
}
