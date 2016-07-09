using UnityEngine;
using UnityEditor;
using System.Collections;

public class WindowScriptTest : EditorWindow {

    [MenuItem("Window/Test")]
	static void Open()
    {
        WindowScriptTest test = (WindowScriptTest)GetWindow<WindowScriptTest>();
        test.Show();
        Debug.Log(test.position);
    }
}
