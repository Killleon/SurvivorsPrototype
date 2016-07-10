using UnityEngine;
using System.Collections;

[System.Serializable]
public class DialogOptions
{
    public string m_Option;
    public Dialog m_Dialog;

    public DialogOptions(string options, Dialog dialog)
    {
        m_Option = options;
        m_Dialog = dialog;
    }
}
