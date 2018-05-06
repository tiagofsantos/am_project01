using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed=20; //velocidade do jogador
    public float maxSpeed = 50f;//velocidade maxima do jogador
    public float jumpPower = 250f;//potencia do salto
    public bool grounded;//esta no chão
    private Rigidbody2D player; //boneco/jogador
    private Vector3 respawnPoint;


    // Use this for initialization
    void Start () {
        player = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        movementUpdate();
        changeDirection();

        
    }

    //movimento do jogador 
    private void movementUpdate()
    {
        //mover na horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");
        player.AddForce((Vector2.right * moveSpeed) * horizontal);

        //limitar a velocidade do jogador
        if(player.velocity.x > maxSpeed)
        {
            player.velocity = new Vector2(maxSpeed,player.velocity.y);
        }

        if (player.velocity.x <- maxSpeed)
        {
            player.velocity = new Vector2(-maxSpeed, player.velocity.y);
        }

        //saltar
        if (Input.GetButtonDown("Jump") && grounded)
        {
            player.AddForce(Vector2.up * jumpPower);
        }

    }

    //muda a direção do boneco
    private void changeDirection()
    {
        if(Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position;
        }
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
    }



}
