using Fungus;
using UnityEngine;


namespace YuCheng
{
    [CommandInfo("Custom", "Check Flag Then Continue", "判斷一個腳本物件的 bool 值，為 true 則繼續執行，否則跳過 Block")]
    public class CheckFlagThenContinue : Command
    {
        [Tooltip("要判斷的腳本物件")]
        public Fungus_Item dialogueFlag;

        public Player player;

        //[Tooltip("如果是 false，是否結束整個 block？")]
        //public bool stopBlockIfFalse = true;

        public override void OnEnter()
        {
            if (dialogueFlag == null)
            {
                Debug.LogWarning("未指定 DialogueFlagSO");
                Continue();
                return;
            }

            if (!dialogueFlag.fungusShowState)
            {
                // 繼續執行 Block
                Continue();
                dialogueFlag.fungusShowState = true;
            }
            else
            {
                StopParentBlock();
                if (player != null) player.canMove = true;
            }
        }
    }
}
