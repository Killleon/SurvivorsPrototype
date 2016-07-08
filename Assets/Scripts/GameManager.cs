using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public Button trade;
    public Button openDoors;
    public Button reject;

    public Text Text_homeCooking;
    public Text Text_homeCombat;
    public Text Text_homeTechnology;
    public Text Text_homeMedicine;

    public GameObject visitorObject;
    public RandomVisitor visitorScript;
    public Homebase homeBaseStats;

    private string HUD_stats;

    // Use this for initialization
    void Start () {
        visitorScript = visitorObject.GetComponent<RandomVisitor>();
        openDoors.onClick.AddListener(Accepted);
        reject.onClick.AddListener(Rejected);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Accepted(){
        homeBaseStats.homeStats.cooking += visitorScript.s.cooking;
        homeBaseStats.homeStats.combat += visitorScript.s.combat;
        homeBaseStats.homeStats.technology += visitorScript.s.technology;
        homeBaseStats.homeStats.medicine += visitorScript.s.medicine;
        visitorScript.accepted = true;
    }

    public void Rejected(){
        visitorScript.rejected = true;
    }
}
