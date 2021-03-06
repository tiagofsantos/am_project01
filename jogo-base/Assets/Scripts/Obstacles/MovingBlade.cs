﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlade : MonoBehaviour {

    public const float MOVEMENT_SPEED = 5f;

    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    public const float STOPPED_TIME = 5f; // Tempo que a lâmina fica parada quando chega aos destinos.

    private bool stopped; // Verifica se a lâmina chegou a um dos destinos, para que ela fique parada.
   
    void Start()
    {
        stopped = false;
        pointSelection = 1;
        currentPoint = points[pointSelection];
    }
    
    void Update()
    {
        if (!stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * MOVEMENT_SPEED);

            if (transform.position == currentPoint.position)
            {
                StartCoroutine(wait());
                pointSelection++;

                if (pointSelection == points.Length)
                {
                    pointSelection = 0;
                }

                currentPoint = points[pointSelection];
            }
        }

    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            
            if (!player.movement.respawning)
            {
                AudioManager.instance.playSound("blade_hit.wav");
                StartCoroutine(player.movement.fadeout());
                knockback(other);
            }
        }
    }

    public void knockback(Collider2D col)
    {
        float speed = 350;

        Vector3 direction = col.transform.position - transform.position;
        Rigidbody2D body = col.GetComponent<Rigidbody2D>();

        body.velocity = Vector3.zero;
        body.AddForce(direction * speed);
    }

    private IEnumerator wait()
    {
        stopped = true;
        yield return new WaitForSeconds(STOPPED_TIME);
        stopped = false;
    }
}
