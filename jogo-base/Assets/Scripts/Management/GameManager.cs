using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int testSessionId = 0;
    private String testUsername = "Ruben";

    public static Session session;
    public static Session shadowSession;

    public Player player;
    public Player shadow;

    public GameObject playerObj;
    public GameObject shadowObj;

    private EntitySpawner spawner;

    void Awake()
    {
        spawner = new EntitySpawner();

        User ruben = new User(0, testUsername, "ruben.amendoeira@gmail.com", DateTime.ParseExact("06/12/1996", "dd/MM/yyyy", null), "teste123", "PT");
        User hugo = new User(0, "Hugo", "hugobenfiquista@hotmail.com", DateTime.ParseExact("25/05/1997", "dd/MM/yyyy", null), "teste123", "PT");

        player = new Player(ruben, new Scout());
        shadow = new Player(hugo, new Scout());

        playerObj = spawner.spawnPlayer(player);
        shadowObj = spawner.spawnShadow(shadow);

        session = new Session(1, player, shadow);
        shadowSession = Session.load(testUsername, testSessionId);

        ActionReplay replayScript = shadowObj.GetComponentInParent<ActionReplay>();
        replayScript.actions = shadowSession.playerActions;

    }


    private void OnApplicationQuit()
    {
        session.save(testUsername, playerObj.GetComponentInParent<ActionTracker>());
    }
}
