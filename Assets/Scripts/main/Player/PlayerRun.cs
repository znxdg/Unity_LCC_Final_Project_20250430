using UnityEngine;
using YuCheng;

public class PlayerRun : PlayerState
{
    public PlayerRun(Player _player, StateMachine _stateMachine, string _name) : base(_player, _stateMachine, _name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ani.SetFloat("動作模式", 1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!player.canMove)
        {
            stateMachine.SwitchState(player.playerIdle);
        }
        player.rig.linearVelocity =
                new Vector2(player.hor_value * player.runSpeed,
                player.ver_value * player.runSpeed);
        player.ani.SetFloat("方向", player.way_value);
        // 玩家翻面
        player.Flip(player.hor_value);

        if (Mathf.Abs(player.hor_value) == 0 && Mathf.Abs(player.ver_value) == 0)
            stateMachine.SwitchState(player.playerIdle);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            stateMachine.SwitchState(player.playerWalk);
    }
}
