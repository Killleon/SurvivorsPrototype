using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class DialogTreeWindow : EditorWindow
{

    static DialogTreeWindow dialogWindow;
    static DialogTree currentDialogTree;
    Rect mouseDown;
    Rect mouseUp;
    bool drawMode = false;
    bool buttonDown = false;

    List<NodeWindow> nodes = new List<NodeWindow>();
    NodeWindow nodeClick;
    Vector2 scroll = new Vector2(0, 0);
    Vector2 nodeScroll = new Vector2(0, 0);
    Rect infoWindow;

    //Informations
    string charName = "";
    string message = "";
    public DialogEvent methods;

    bool fade = false;
    float fadeAnim = 0;

    string[] options;

    string[] controls = { "NameField", "MessageField", "MethodField" };
    Event e;
    int controlIndex = 0;

    public static void OpenWindow(DialogTree dialogTree)
    {

        dialogWindow = (DialogTreeWindow)EditorWindow.GetWindow<DialogTreeWindow>();
        dialogWindow.Focus();
        Debug.Log(dialogWindow.position);
        dialogWindow.position = new Rect(100, 100, 1000, 500);
        dialogWindow.maxSize = new Vector2(1000, 1000);
        dialogWindow.Show();
        if (dialogTree.root == null)
        {
            dialogTree.root = new Dialog();
        }
        currentDialogTree = dialogTree;
        dialogWindow.Init(dialogWindow.position.size);
    }
    
    public void Init(Vector2 size)
    {
        e = Event.current;
        nodes = currentDialogTree.nodes;
        if (nodes.Count == 0)
        {
            AddTreeToNodeWindow(currentDialogTree.root, new Vector2(0,200));

        }
        infoWindow = new Rect(size.x - 300, 100, 300, size.y);
        nodeClick = nodes[0];
    }
    NodeWindow AddTreeToNodeWindow(Dialog dialog, Vector2 pos)
    {
        if (dialog == null)
            return null;
        NodeWindow node = CreateInstance<NodeWindow>();
        node.m_Dialog = dialog;
        node.m_rWindow = new Rect(pos.x, pos.y, 100, 100);

        int index = 0;
        foreach (DialogOptions entry in dialog.Options)
        {

            node.m_Connect.Add(AddTreeToNodeWindow(entry.m_Dialog, new Vector2(pos.x + 200, pos.y + 200 * (index))));
            index++;
        }


        nodes.Add(node);
        return node;

    }

    void CreateNodeWindow(NodeWindow nodeWindow, Rect pos)
    {

        NodeWindow node = CreateInstance<NodeWindow>();
        node.m_Dialog = nodeWindow.m_Dialog.CreateOption("");
        node.m_Dialog.PreviousDialog = nodeWindow.m_Dialog;
        node.m_rWindow = new Rect(pos.position.x, pos.position.y, 100, 100);
        node.m_Connect.Add(nodeWindow);


        nodes.Add(node);


    }


    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, dialogWindow.position.size.x, 150));
        scroll = GUILayout.BeginScrollView(scroll, false, true);
        GUILayout.BeginHorizontal();
        if (!drawMode && GUILayout.Button("Draw Node: Off"))
        {
            drawMode = true;
        }
        else if (drawMode && GUILayout.Button("Draw Node: On"))
        {
            drawMode = false;
        }
        GUILayout.BeginVertical();
        DrawInfoWindow(0);
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        if (e.type == EventType.mouseDown)
        {
            mouseDown = new Rect(e.mousePosition, new Vector2(0, 0));
            if (drawMode)
            {
                OnWindow(mouseDown);
                if (nodeClick != null)
                {
                    buttonDown = true;
                }
            }
            else
            {
                OnWindow(mouseDown);
                Debug.Log(nodeClick == null);
            }



        }
        if (e.type == EventType.mouseUp && buttonDown)
        {
            mouseUp = new Rect(e.mousePosition, new Vector2(0, 0));
            buttonDown = false;
            CreateNodeWindow(nodeClick, mouseUp);

            nodeClick = null;
        }
        if (buttonDown)
        {
            mouseUp = new Rect(e.mousePosition, Vector2.zero);

            DrawNodeCurve(mouseDown, mouseUp);

        }




        try
        {
            BeginWindows();
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].m_rWindow = GUI.Window(i + 1, nodes[i].m_rWindow, DrawNodeWindow, "Dialog" + i);
                for (int j = 0; j < nodes[i].m_Connect.Count; j++)
                {
                    DrawNodeCurve(nodes[i].m_Connect[j].m_rWindow, nodes[i].m_rWindow);
                }
            }

            EndWindows();
        }
        catch
        {
            nodes = new List<NodeWindow>();
            AddTreeToNodeWindow(currentDialogTree.root, new Vector2(0, 200));
        }

        







    }

    void Update()
    {
        Repaint();
    }


    void ResetInformations()
    {
        charName = nodeClick.m_Dialog.Name;
        message = nodeClick.m_Dialog.Message;
        methods = nodeClick.m_Dialog.DialogMethod;
        options = GetOptionsName();
    }
    void ApplyInformations()
    {
        nodeClick.m_Dialog.Name = charName;
        nodeClick.m_Dialog.Message = message;
        nodeClick.m_Dialog.DialogMethod = methods;
        SetOptionsName();
    }
    string[] GetOptionsName()
    {
        List<string> names = new List<string>();
        foreach(DialogOptions op in nodeClick.m_Dialog.Options)
        {
            names.Add(op.m_Option);
        }
        return names.ToArray();
    }
    void SetOptionsName()
    {
        for(int i=0;i< nodeClick.m_Dialog.Options.Count;i++)
        {
            nodeClick.m_Dialog.Options[i].m_Option = options[i];
        }
    }

    void DrawNodeWindow(int id)
    {
        if (!drawMode)
        {
            GUI.DragWindow();
            
        }


        GUILayout.Label(nodes[id - 1].m_Dialog.Name);


    }
    void DrawInfoWindow(int id)
    {

        if (nodeClick != null)
        {

            try
            {
                
                GUILayout.Label("Name: ");
               
                charName = GUILayout.TextField(charName);

                GUILayout.Label("Messege:");
               
                message = GUILayout.TextArea(message);

                for (int i = 0; i < options.Length; i++)
                {
                    GUILayout.Label(string.Format("Option {0}: ",i));
                    options[i] = GUILayout.TextField(options[i]);
                }

                GUILayout.Label("On Talk Event:");
                SerializedObject so = new SerializedObject(this);
                SerializedProperty sp = so.FindProperty("methods");
               

                EditorGUILayout.PropertyField(sp);
                so.ApplyModifiedProperties();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Reset"))
                {
                    ResetInformations();
                }
                if (GUILayout.Button("Apply"))
                {
                    ApplyInformations();
                }
                GUILayout.EndHorizontal();

                

            }
            catch
            {

            }


        }


    }
    //ref https://community.unity.com/t5/Extensions-OnGUI/Simple-node-editor/td-p/1448640
    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    void OnWindow(Rect pos)
    {
        

        for (int i = 0; i < nodes.Count; i++)
        {
            bool checkHorizontal = (pos.position.x >= nodes[i].m_rWindow.position.x && pos.x <= nodes[i].m_rWindow.x + nodes[i].m_rWindow.width);
            //Debug.Log(checkHorizontal);
            bool checkVertical = (pos.position.y >= nodes[i].m_rWindow.position.y && pos.y <= nodes[i].m_rWindow.y + nodes[i].m_rWindow.height);
            //Debug.Log(checkVertical);
            if (checkHorizontal && checkVertical)
            {

                nodeClick = nodes[i];
                ResetInformations();
            }
        }


    }


}


