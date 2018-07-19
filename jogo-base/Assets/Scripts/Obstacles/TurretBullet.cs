using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    public GameObject spawnOrigin;
    public SpriteRenderer renderer;
    public bool hitPlayer;
    public bool startedDestruction;
    public const float LIFESPAN = 10f;
    
    void Start () {
        renderer = this.GetComponent<SpriteRenderer>();
        hitPlayer = false;
        startedDestruction = false;
        StartCoroutine(timespan());
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

            AudioManager.instance.playSound("turret_hit.wav");
            StartCoroutine(player.movement.fadeout());
            hitPlayer = true;

            color.a -= 0.5f;
            renderer.material.color = color;

        }
        
        if(renderer.material.color.a == 0 || (renderer.material.color.a == 0.5 && other.transform.tag == "Ground"))
        {
            StartCoroutine(destroyBullet());
            return;
        }

        if (!hitPlayer && other.transform.tag == "Ground")
        {
            startedDestruction = true;
            Destroy(this.gameObject);
        }
    }

    private IEnumerator timespan()
    {
        yield return new WaitForSeconds(LIFESPAN);

        if(!startedDestruction) {
            if (hitPlayer)
            {
                StartCoroutine(destroyBullet());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator destroyBullet()
    {
        startedDestruction = true;
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.GetComponent<Rigidbody2D>());
        Destroy(this.GetComponent<Collider2D>());
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    
}
