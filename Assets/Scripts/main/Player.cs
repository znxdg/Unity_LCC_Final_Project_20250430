using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 玩家
    /// </summary>
    public class Player : MonoBehaviour
    {

        #region 基本參數&設置
        [Header("基本數值")]
        [SerializeField, Range(0, 10)]
        public float moveSpeed = 3.5f;
        [SerializeField, Range(0, 20)]
        public float runSpeed = 7f;

        public Animator ani { get; private set; }           // 動畫控制元件
        public Rigidbody2D rig { get; private set; }        // 2D 剛體元件
                                                            
        public float way_value;           // 控制方向參數

        public float action_value;        // 控制動作參數

        public float hor_value;

        public float ver_value;

        #endregion

        public StateMachine stateMachine { get; private set; }
        public PlayerIdle playerIdle { get; private set; }
        public PlayerWalk playerWalk { get; private set; } 
        public PlayerRun playerRun { get; private set; }

        private void Awake()
        {
            ani = GetComponent<Animator>();     // 取得動畫元件
            rig = GetComponent<Rigidbody2D>();  // 取得剛體元件

            stateMachine = new StateMachine();
            playerIdle = new PlayerIdle(this, stateMachine, "玩家待機");
            playerWalk = new PlayerWalk(this, stateMachine, "玩家走路");
            playerRun = new PlayerRun(this, stateMachine, "玩家跑步");

            stateMachine.Initialize(playerIdle);    // 指定預設為待機
        }

        private void Update()
        {
            stateMachine.Update();
            hor_value = Input.GetAxis("Horizontal");
            ver_value = Input.GetAxis("Vertical");
            if (Mathf.Abs(hor_value) > Mathf.Abs(ver_value)) ver_value = 0;
            else hor_value = 0;
            if (ver_value > 0) way_value = 2;
            if (hor_value != 0) way_value = 1;
            if (ver_value < 0) way_value = 0;
        }

        public void Flip(float h)
        {
            if (Mathf.Abs(h) < 0.1f) return;
            float angle = h > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }
}
