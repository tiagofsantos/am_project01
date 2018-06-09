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

    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();

        staminaSlider = GameObject.FindGameObjectWithTag("StaminaSlider").GetComponent<Slider>();
        staminaFill = GameObject.FindGameObjectWithTag("StaminaFill").GetComponent<Image>();

        potionQuantity = GameObject.FindGameObjectWithTag("PotionItemQuantity").GetComponent<Text>();
        beefQuantity = GameObject.FindGameObjectWithTag("BeefItemQuantity").GetComponent<Text>();
        starQuantity = GameObject.FindGameObjectWithTag("StarItemQuantity").GetComponent<Text>();
        boltQuantity = GameObject.FindGameObjectWithTag("BoltItemQuantity").GetComponent<Text>();

        elapsedTime = GameObject.FindGameObjectWithTag("TimeElapsed").GetComponent<Text>();

        defaultStaminaColor = new Color(staminaFill.color.r, staminaFill.color.g, staminaFill.color.b, 1f); ;
        sprintStaminaColor = new Color(staminaFill.color.r, staminaFill.color.g, staminaFill.color.b, .5f); ;
    }

    void Update()
    {
        staminaSlider.value = localPlayer.vitals.stamina;
        staminaFill.color = localPlayer.movement.sprinting ? sprintStaminaColor : defaultStaminaColor;

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
