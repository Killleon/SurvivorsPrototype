using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Homebase : MonoBehaviour {

    public Stats homeStats = new Stats();
    public Text text_Cooking;
    public Text text_Combat;
    public Text text_Technology;
    public Text text_Medicine;

    // Use this for initialization
    void Start () {
        
        homeStats.cooking = homeStats.combat = homeStats.medicine = homeStats.technology = 2;
    }
	
	// Update is called once per frame
	void Update () {

        text_Cooking.text = "Cooking: "+ homeStats.cooking;
        text_Combat.text = " Combat: " + homeStats.combat;
        text_Technology.text = " Tech: " + homeStats.technology;
        text_Medicine.text = " Meds: " + homeStats.medicine;
    }
}
