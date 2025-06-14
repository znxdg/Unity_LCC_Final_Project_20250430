using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// NPC 狀態
    /// </summary>
    public class NpcState : State
    {
        protected NPC npc;

        public NpcState(NPC _npc, StateMachine _stateMachine, string _name)
        {
            npc = _npc;
            stateMachine = _stateMachine;
            name = _name;
        }
    }
}
