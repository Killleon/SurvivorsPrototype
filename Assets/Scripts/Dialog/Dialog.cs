using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class Dialog {
    //Name of the person
    
    private string m_sName;
    public string Name
    {
        get
        {
             if(m_sName==null)
            {
                m_sName = "";
            }
            return m_sName;
        }
        set
        {
            m_sName = value;
        }
    }
    //Message
    
    private string m_sMessage;
    public string Message
    {
        get
        {
            if(m_sMessage == null)
            {
                m_sMessage = "";
            }
            return m_sMessage;
        }
        set
        {
            m_sMessage = value;
        }
    }
    //Action - Method to run while show the text
   
    public DialogEvent m_Method;
    public DialogEvent DialogMethod
    {
        get
        {
            if(m_Method == null)
            {
                m_Method = new DialogEvent();
            }
            return m_Method;
        }
        set
        {
            m_Method = value;
        }
    }
    //Dialogs
    
    Dialog m_PreviousDialog;
    public Dialog PreviousDialog
    {
        get
        {
            return m_PreviousDialog;
        }
        set
        {
            m_PreviousDialog = value;
        }
    }
    
    //Options
    [SerializeField]
    private List<DialogOptions> m_Options = new List<DialogOptions>();
    public List<DialogOptions> Options
    {
        get
        {
            return m_Options;
        }
        set
        {
            m_Options = value;
        }
    }

    /// <summary>
    /// Say if the dialog has options or not
    /// </summary>
    public bool HasOptions()
    {
        
        return m_Options.Count > 1;
    }

    
    /// <summary>
    /// Create an option
    /// </summary>
    /// <param name="option">Question or Option</param>
    /// <returns>What comes next</returns>
    public Dialog CreateOption(string option)
    {
        Dialog dialog = new Dialog() ;
        m_Options.Add(new DialogOptions(option, dialog));
        return dialog;
    }
    /// <summary>
    /// Create an option
    /// </summary>
    /// <param name="option">Question or Option</param>
    /// <param name="dialog">What comes next/param>
    /// <returns>What comes next</returns>
    public Dialog CreateOption(string option, Dialog dialog)
    {
        m_Options.Add(new DialogOptions(option, dialog));
        return dialog;
    }
    /// <summary>
    /// Disconect the option from the dialog.
    /// </summary>
    public Dialog DeleteOption(int index)
    {
        Dialog dialog = m_Options[index].m_Dialog;
        m_Options.RemoveAt(index);
        return dialog;
    }
}

[System.Serializable]
public class DialogEvent:UnityEvent
{

}


