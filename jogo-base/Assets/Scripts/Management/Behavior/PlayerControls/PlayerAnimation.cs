using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    /* O player ao qual este script está associado */
    private Player localPlayer;

    /* Animator */
    private Animator anim;

    private void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(localPlayer.movement.body.velocity.x));
        anim.SetBool("Grounded", localPlayer.movement.grounded);
    }
}
