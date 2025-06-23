using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// NPC 任務進行中
    /// </summary>
    public class NpcMissioning : NpcState
    {
        public NpcMissioning(NPC _npc, StateMachine _stateMachine, string _name) : base(_npc, _stateMachine, _name)
        {
        }

        public override void Update()
        {
            base.Update();

            if (npc.decoctionCount >= npc.needDecoctionCount) stateMachine.SwitchState(npc.complete);
        }

        protected override void Interaction()
        {
            base.Interaction();
            npc.fungus.SendFungusMessage("任務進行中(未完成)");
        }
    }
}
