using UnityEngine;
using System.Collections;

// =================================== This script is for the prefab of the exclamation helpers =================================== //

public class ExclamationMark : MonoBehaviour
{
    private static Animator anim;
    private SpriteRenderer spriteRender;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShowExclamationMark();
    }

    // The function that shows the graphic
    void ShowExclamationMark()
    {
        spriteRender.enabled = true;
        anim.SetBool("ExclamationMarkOn", true);
    }

    // I added this event in the Animation tab in Unity. This function destroys the instantiated prefab (itself)!
    void DestroyItself()
    {
        Destroy(gameObject);
    }
}
