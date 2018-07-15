using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* O player ao qual este script está associado */
    private Player localPlayer;

    private void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            localPlayer.movement.jump();
            localPlayer.tracker.addAction(ActionType.JUMP, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            localPlayer.inventory.consume(0);
            localPlayer.tracker.addAction(ActionType.CONSUME_1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            localPlayer.inventory.consume(1);
            localPlayer.tracker.addAction(ActionType.CONSUME_2, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            localPlayer.inventory.consume(2);
            localPlayer.tracker.addAction(ActionType.CONSUME_3, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            localPlayer.inventory.consume(3);
            localPlayer.tracker.addAction(ActionType.CONSUME_4, 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            localPlayer.movement.sprinting = true;
            localPlayer.tracker.addAction(ActionType.SPRINT);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            localPlayer.movement.sprinting = false;
            localPlayer.tracker.closeAction(ActionType.SPRINT);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            localPlayer.movement.moveRight();
            localPlayer.tracker.addAction(ActionType.MOVE_RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            localPlayer.movement.moveLeft();
            localPlayer.tracker.addAction(ActionType.MOVE_LEFT);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            localPlayer.movement.stop();
            localPlayer.tracker.closeAction(ActionType.MOVE_RIGHT);
            localPlayer.tracker.addAction(ActionType.STOP, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            localPlayer.movement.stop();
            localPlayer.tracker.closeAction(ActionType.MOVE_LEFT);
            localPlayer.tracker.addAction(ActionType.STOP, 1);
        }
    }
}
