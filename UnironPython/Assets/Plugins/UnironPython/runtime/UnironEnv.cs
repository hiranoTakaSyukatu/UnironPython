using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Threading.Tasks;

namespace UnironPython
{
    public class UnironEnv
    {
        static bool isVaild = false;
        static ScriptEngine engine = null;

        internal static event Action<ScriptEngine> ScriptEngineCreatedEventHandler;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateInstance()
        {
            Task.Run(() =>
            {
                engine = Python.CreateEngine();
                IsVaild = true;
            });
        }

        internal static bool IsVaild
        {
            get => isVaild;
            set
            {
                engine = engine ?? throw new NullReferenceException();
                ScriptEngineCreatedEventHandler?.Invoke(engine);
                ScriptEngineCreatedEventHandler = null;
            }
        }

        internal static ScriptEngine GetScriptEngine()
        {
            return isVaild ? engine : null;
        }
    }
}