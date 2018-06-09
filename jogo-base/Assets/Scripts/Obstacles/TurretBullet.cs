using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    public GameObject spawnOrigin;
    SpriteRenderer renderer;
    bool started;
    
    void Start () {
        renderer = this.GetComponent<SpriteRenderer>();
    }
	
	void Update () {
        started = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject == spawnOrigin)
        {
            return;
        }

        Color color = renderer.material.color;

        if(other.transform.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.movement.respawning || player.movement.isStunned() || player.movement.phasing)
            {
                return;
            }

            StartCoroutine(player.movement.fadeout());
            started = true;

            color.a -= 0.5f;
            renderer.material.color = color;

        }
        
        if(renderer.material.color.a == 0 || (renderer.material.color.a == 0.5 && other.transform.tag == "Ground"))
        {
            StartCoroutine(destroyBullet());
            return;
        }

        if (!started && other.transform.tag == "Ground")
            Destroy(this.gameObject);
    }

    private IEnumerator destroyBullet()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.GetComponent<Rigidbody2D>());
        Destroy(this.GetComponent<Collider2D>());
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    
}
