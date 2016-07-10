using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    Camera Focus;
    const int ortosize = 5;
    public float Zoom = 1.2f;
    public float ZoomVel = 5f;
    Tweener camMove;
	// Use this for initialization
	void Start () {
        Focus = GameObject.Find("FocusCamera").GetComponent<Camera>();
        Focus.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {
            CenterCam();
        }
        if(camMove!= null && camMove.IsComplete())
        {
            Focus.enabled = false;
        }
	}

    public Tweener FocusCam(GameObject obj)
    {
        Focus.enabled = true;
        Focus.DOOrthoSize(Zoom, ZoomVel);
        return Focus.transform.DOMove(new Vector3(obj.transform.position.x, obj.transform.position.y, Camera.main.transform.position.z),ZoomVel);
    }
    public void CenterCam()
    {
        Focus.DOOrthoSize(ortosize, ZoomVel);
        camMove = Focus.transform.DOMove(Camera.main.transform.position, ZoomVel);
       
    }

}
