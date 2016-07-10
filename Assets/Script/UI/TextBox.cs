using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

//Message that store the name and the message
[System.Serializable]
public class Message
{
    public string name;
    [Multiline]
    public string message;
}

public class TextBox : MonoBehaviour
{

    public float duration = 2f;
    public Text textBox;
    public Text nameBox;
    public GameObject optionList;
    public Button optionPrefab;

    DialogTree allMessage; // messages to be show
    Dialog currentMessage;

    Canvas cvs;//canvas with the text box UI

    Tweener messegeTween; //Text tweener

    

    int indexOptions = 0;

    List<Button> options = new List<Button>();
    
    // Use this for initialization
    void Start()
    {

        cvs = GetComponent<Canvas>();//Get the canvas
        cvs.enabled = false;
        
    }

    // I make the tween of the first message of the all messages list. When the message is finish, I clear the ui text and show the next.
    //To advence through the text the player have to press z. If the text is being add to the ui text, and the player press z, the tween will stop and the
    //full text will be add to the ui.
    void Update()
    {

        if (messegeTween != null)//if there is no tween, don't continue
        {
            if (messegeTween.IsComplete() || !messegeTween.IsPlaying()) // check if the tween was complete
            {
                if (TextInput())
                {
                   
                    ClearText();
                    if(currentMessage != null)
                    {
                        StartTween();
                    }
                    else
                    {
                        if (cvs.enabled)
                        {
                            Debug.LogWarning("Hey");
                           
                            
                            cvs.enabled = false;
                        }
                    }
                   
                }

            }else
            {
                if (TextInput())
                {
                    InteruptTween();
                }
            }
        }

    }
    /// <summary>
    /// Clear all the texts and remove the last index of the list
    /// </summary>
    void ClearText(bool remove = true)
    {
       
        if(remove == true && currentMessage!=null && currentMessage.Options.Count ==0)
        {
            currentMessage = null;
        }
       
        textBox.text = string.Empty;
        nameBox.text = string.Empty;
    }
    /// <summary>
    /// Interupt the tween and show the full message.
    /// </summary>
    void InteruptTween()
    {
        messegeTween.Kill();
        textBox.text = currentMessage.Message;
        nameBox.text = currentMessage.Name; 
    }
    /// <summary>
    /// Starts the tween and write the name
    /// </summary>
    void StartTween()
    {
        if (currentMessage == null) return;
        nameBox.text = currentMessage.Name;
        messegeTween = textBox.DOText(currentMessage.Message, duration);
        CreateOptions();
    }
    /// <summary>
    /// If the key that i assign for the text, is press
    /// </summary>
    /// <returns>If the key was press or not</returns>
    bool TextInput()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }
    /// <summary>
    /// This is the method that will be call by the interact event
    /// </summary>
    /// <param name="messages">dialog</param>
    /// <param name="method">game event</param>
    /// <param name="index">when the game event will be called during the dialog</param>
    public void AddMessages(DialogTree messages)
    {



        allMessage = messages;
        currentMessage = messages.root;
        

        if (!cvs.enabled)
        {
            cvs.enabled = true;
        }
        if(messegeTween == null)
        {
            ClearText(false);
            StartTween();
        }
        else
        {
            if(!messegeTween.IsPlaying())
            {
                ClearText(false);
                StartTween();
            }
        }
    }
    //Get the number of messages.
    public int GetMessageCount()
    {
        return allMessage.nodes.Count;
    }

    void CreateOptions()
    {
        for(int i = 0; i < currentMessage.Options.Count;i++)
        {
            Button b = Instantiate(optionPrefab);
            b.transform.SetParent(optionList.transform, false);
            b.GetComponentInChildren<Text>().text = currentMessage.Options[i].m_Option;
            b.onClick.AddListener(delegate { SelectOption(i); });
            options.Add(b);
        }
    }
    
    void SelectOption(int index)
    {
        ClearText(false);
        currentMessage = currentMessage.Options[index].m_Dialog;
        StartTween();
        while(options.Count >0)
        {
            int count = options.Count;
            Destroy(options[0]);
            if(count == options.Count)
            {
                options.RemoveAt(0);
            }

        }
    }
}
