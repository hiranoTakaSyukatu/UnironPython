using Microsoft.Scripting.Hosting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnironPython
{
    public class UnironScript : MonoBehaviour
    {
        [SerializeField]
        TextAsset textAsset;

        ScriptEngine engine = null;
        ScriptScope scope = null;
        ScriptSource source = null;
        CompiledCode compiledCode = null;

        void Start()
        {
            if (UnironEnv.IsVaild)
            {
                engine = UnironEnv.GetScriptEngine();
                engine.Runtime.LoadAssembly(typeof(GameObject).Assembly);
                scope = engine.CreateScope();
                source = engine.CreateScriptSourceFromString(textAsset.text);
                compiledCode = source.Compile();
            }
            else
            {
                UnironEnv.ScriptEngineCreatedEventHandler += engine =>
                {
                    this.engine = engine;
                    engine.Runtime.LoadAssembly(typeof(GameObject).Assembly);
                    scope = engine.CreateScope();
                    source = engine.CreateScriptSourceFromString(textAsset.text);
                    compiledCode = source.Compile();
                };
            }
        }

        void Update()
        {
            if (engine != null && Input.GetKeyDown(KeyCode.Return))
            {
                source.Execute(scope);
            }
        }
    }
}
