using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour {

    public float playerHealth;
    public float playerMaxHealth = 100;
    public float playerMoney;
    public Image playerHealthImage;
    public Text playerHealthText;
    public Text playerMoneyText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Fill the health bar depending on the current hull integrity
        playerHealthImage.fillAmount = (playerHealth / 100);

        // Set the health text to display the current hull integrity
        playerHealthText.text = "Hull Integrity: " + playerHealth;

        // Set the money text to display how much money the player currently has
        playerMoneyText.text = "" + playerMoney;

	}
}
