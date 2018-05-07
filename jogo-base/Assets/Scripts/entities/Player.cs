using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Character character;
    private Vitals vitals;

    private bool sprinting;

    public Player() {
        this.vitals = new Vitals();
        this.sprinting = false;
    }
    
	void Start () {
        this.character = new Scout();
    }

    void Update () {

        if (sprinting)
            Debug.Log("Alterar velocidade aqui");
        else
            vitals.restoreStamina(character.getLevel(Skill.Resistance));
	}

}
