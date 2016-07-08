using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class RandomVisitor : MonoBehaviour
{
    private static string[] wordList = new string[] {
"Leon Wong","Lucas Goes","Tomer Tomer","Daniel Bastardo","Gordon Niemann","Chante Blaze","Jarrod Hottodornot"
    };

    public bool rejected = false;
    public bool accepted = false;
    private bool visitorStopped;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Stats s = new Stats();
    //private static Animator anim;

    public float speed;
    public int highestPossibleStat = 7;
    public int lowestPossibleStat = 0;
    public CanvasGroup canvas;
    public CanvasGroup radialMenu;
    public CanvasScaler scaler;
    public Text UIName;
    public Text UIStats;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        s.cooking = UnityEngine.Random.Range(lowestPossibleStat, highestPossibleStat);
        s.combat = UnityEngine.Random.Range(lowestPossibleStat, highestPossibleStat);
        s.medicine = UnityEngine.Random.Range(lowestPossibleStat, highestPossibleStat);
        s.technology = UnityEngine.Random.Range(lowestPossibleStat, highestPossibleStat);
        int rng = UnityEngine.Random.Range(0, wordList.Length);
        s.randomName = wordList[rng];

        print(s.randomName);
        print("cooking is " + s.cooking);
        print("combat is " + s.combat);
        print("medicine is " + s.medicine);
        print("technology is " + s.technology);

        UIName.text = s.randomName;
        UIStats.text = "Cooking: " + s.cooking + "\n" + "Combat: " + s.combat + "\n" + "Technology: " + s.technology + "\n" + "Medicine: " + s.medicine;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = new Vector2(horizontalInput, 0.0f);
        if(transform.position.x <= 0.5f)
            Move(2);
        else
        {
            print("test");
            Move(0);
            visitorStopped = true;
        }

        if (visitorStopped)
        {
            sr.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        }

        if (rejected)
        {
            Move(-2);
            if(transform.position.y <= -5)
            {
                Destroy(gameObject);
            }
        }

        if (accepted)
        {
            Move(2);
            if (transform.position.y <= -5)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Move(float input)
    {
        //Instead of adding a force (which is the same as velocity +=...) we directly set the velocity
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }

        //We send the absolute value of input, which will convert -1 to 1, but keep 1 as it is. (It converts a number to be always positive)
        //anim.SetFloat("Speed", Mathf.Abs(input));
    }

    void OnMouseDown()
    {
        if (visitorStopped)
        {
            radialMenu.alpha = 1;
            radialMenu.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z+9));
        }
    }
}
