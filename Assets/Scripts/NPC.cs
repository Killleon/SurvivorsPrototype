using UnityEngine;
using System.Collections;
using DG.Tweening;

public class NPC : MonoBehaviour {

    [SerializeField]
    DialogTree m_Dialog;

    CameraController camController;

    Tweener camFocus;

    bool m_bDisplayDialog = false;

    TextBox textBox;
	// Use this for initialization
	void Start () {
        camController = FindObjectOfType<CameraController>();
        textBox = FindObjectOfType<TextBox>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(camFocus != null && camFocus.IsComplete() && !m_bDisplayDialog) 
        {
            camFocus = null;
            
            m_bDisplayDialog = true;
        }
	}

    void OnMouseDown()
    {
        camFocus = camController.FocusCam(this.gameObject);
        textBox.AddMessages(m_Dialog);
    }
}
