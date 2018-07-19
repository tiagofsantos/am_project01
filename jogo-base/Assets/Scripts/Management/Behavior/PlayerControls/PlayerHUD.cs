using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private Color defaultStaminaColor;
    private Color sprintStaminaColor;

    /* O player ao qual o HUD está associado */
    private Player localPlayer;

    private Slider staminaSlider;
    private Image staminaFill;

    private Text potionQuantity;
    private Text beefQuantity;
    private Text starQuantity;
    private Text boltQuantity;

    private Text elapsedTime;

    private Text stunTimer;
    private GameObject stunContainer;

    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();

        staminaFill = GameObject.FindGameObjectWithTag("StaminaFill").GetComponent<Image>();
        staminaSlider = GameObject.FindGameObjectWithTag("StaminaSlider2").GetComponent<Slider>();

        potionQuantity = GameObject.FindGameObjectWithTag("PotionItemQuantity").GetComponent<Text>();
        beefQuantity = GameObject.FindGameObjectWithTag("BeefItemQuantity").GetComponent<Text>();
        starQuantity = GameObject.FindGameObjectWithTag("StarItemQuantity").GetComponent<Text>();
        boltQuantity = GameObject.FindGameObjectWithTag("BoltItemQuantity").GetComponent<Text>();

        elapsedTime = GameObject.FindGameObjectWithTag("TimeElapsed").GetComponent<Text>();

        stunContainer = GameObject.FindGameObjectWithTag("StunIndicator");
        stunTimer = GameObject.FindGameObjectWithTag("StunTimer").GetComponent<Text>();

        stunContainer.SetActive(false);

        defaultStaminaColor = new Color(staminaFill.color.r, staminaFill.color.g, staminaFill.color.b, 1f); ;
        sprintStaminaColor = new Color(staminaFill.color.r, staminaFill.color.g, staminaFill.color.b, .5f); ;
    }

    void Update()
    {
        staminaSlider.value = (float) Math.Round(localPlayer.vitals.stamina);
        staminaFill.color = localPlayer.movement.sprinting ? sprintStaminaColor : defaultStaminaColor;

        stunContainer.SetActive(localPlayer.vitals.isStunned());
        stunTimer.text = "Stunned: " + Math.Round(localPlayer.vitals.stunClock) + " s";

        Inventory inv = localPlayer.inventory;

        potionQuantity.text = inv.quantityOf("Energy Potion") + "";
        beefQuantity.text = inv.quantityOf("Beef") + "";
        starQuantity.text = inv.quantityOf("Star") + "";
        boltQuantity.text = inv.quantityOf("Lightning Bolt") + "";

        elapsedTime.text = formatTimer(GameManager.instance.getLocalSession().elapsedTime);
    }

    private string formatTimer(float timer)
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");
        return minutes + ":" + seconds;
    }
}
