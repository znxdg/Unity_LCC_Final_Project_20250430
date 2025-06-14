using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// NPC 接任務前
    /// </summary>
    public class NpcMissionBefore : NpcState
    {
        public NpcMissionBefore(NPC _npc, StateMachine _stateMachine, string _name) : base(_npc, _stateMachine, _name)
        {
        }
    }
}
