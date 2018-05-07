using UnityEngine;

public class Player : MonoBehaviour {

    private Character character;
    private Vitals vitals;
    private Inventory inventory;

    private bool sprinting;

    public Player() {
        this.vitals = new Vitals();
        this.sprinting = false;
        this.inventory = new Inventory();
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
