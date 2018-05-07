using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Character character;
    
	void Start () {
        this.character = new Scout();
    }

    void Update () {
	}
}
