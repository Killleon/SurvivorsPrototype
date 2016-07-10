using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateDialogTree  {

    [MenuItem("Assets/Create/Dialog/DialogTree")]
    public static DialogTree Create()
    {
        DialogTree asset = ScriptableObject.CreateInstance<DialogTree>();

        try
        {
            AssetDatabase.CreateAsset(asset, "Assets/Dialogs/DialogTree.asset");
        }
        catch
        {
            AssetDatabase.CreateFolder("Assets", "Dialogs");
            AssetDatabase.CreateAsset(asset, "Assets/Dialogs/DialogTree.asset");
        }
        
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;

        return asset;
    }
}
