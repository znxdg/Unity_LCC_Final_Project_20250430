using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// NPC 完成任務
    /// </summary>
    public class NpcMissionComplete : NpcState
    {
        public NpcMissionComplete(NPC _npc, StateMachine _stateMachine, string _name) : base(_npc, _stateMachine, _name)
        {
        }

        protected override void Interaction()
        {
            base.Interaction();
            npc.fungus.SendFungusMessage("任務完成");
        }
    }
}
