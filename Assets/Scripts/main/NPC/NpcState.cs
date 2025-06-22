using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// NPC 狀態
    /// </summary>
    public class NpcState : State
    {
        protected NPC npc;
        public bool playerInArea;

        public NpcState(NPC _npc, StateMachine _stateMachine, string _name)
        {
            npc = _npc;
            stateMachine = _stateMachine;
            name = _name;
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

            playerInArea = npc.isEnter;

            if (playerInArea && Input.GetKeyDown(KeyCode.E)) Interaction();
        }

        protected virtual void Interaction()
        {
            Print.Text("開始互動", "#f66");
        }
    }
}
