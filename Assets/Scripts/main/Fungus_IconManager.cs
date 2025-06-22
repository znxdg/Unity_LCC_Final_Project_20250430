using Fungus;
using UnityEngine;
using System.Reflection;

namespace YuCheng
{
    [CommandInfo("Custom", "Set Bool From Script", "更改某個物件上的參數")]
    public class SetBoolOnScript : Command
    {
        public GameObject targetObject;
        public string scriptName;
        public string boolFieldName;
        public bool newValue;

        public override void OnEnter()
        {
            if (targetObject == null || string.IsNullOrEmpty(scriptName) || string.IsNullOrEmpty(boolFieldName))
            {
                Debug.LogWarning("參數未設置正確！");
                Continue();
                return;
            }

            var script = targetObject.GetComponent(scriptName);
            if (script == null)
            {
                Debug.LogWarning($"找不到腳本 {scriptName}");
                Continue();
                return;
            }

            var field = script.GetType().GetField(boolFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null && field.FieldType == typeof(bool))
            {
                field.SetValue(script, newValue);
            }
            else
            {
                Debug.LogWarning($"找不到欄位或不是 bool 類型：{boolFieldName}");
            }

            Continue();
        }
    }

}
