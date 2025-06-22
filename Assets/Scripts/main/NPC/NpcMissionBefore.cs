using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// NPC 接任務前
    /// </summary>
    public class NpcMissionBefore : NpcState
    {
        private bool isInteraction;

        public NpcMissionBefore(NPC _npc, StateMachine _stateMachine, string _name) : base(_npc, _stateMachine, _name)
        {
        }

        public override void Update()
        {
            base.Update();
            if (isInteraction && timer > 1) stateMachine.SwitchState(npc.missioning); 
        }

        protected override void Interaction()
        {
            base.Interaction();
            isInteraction = true;
            npc.fungus.SendFungusMessage("任務前");
        }
    }
}
