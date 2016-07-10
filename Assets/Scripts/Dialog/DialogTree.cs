using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DialogTree : ScriptableObject {

    public Dialog root;

    public List<NodeWindow> nodes = new List<NodeWindow>();
}
public class NodeWindow : ScriptableObject
{
    public Rect m_rWindow;

    public Dialog m_Dialog;

    public List<NodeWindow> m_Connect = new List<NodeWindow>();
}
