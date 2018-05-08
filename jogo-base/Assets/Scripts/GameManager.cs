using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private string username = "ruben";
    private int testSessionId = 0;

    public static Session session;

	void Start () {

        Player player = new Player(new Scout());
        Player shadow = new Player(new Scout());

        session = new Session(player, shadow);

    }

    private void OnApplicationQuit()
    {
        session.save(username);
    }

    void Update () {
		
	}
}
