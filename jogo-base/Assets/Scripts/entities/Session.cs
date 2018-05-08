using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Session {

    private static int uniqueId = 0;

    private int id;

    private DateTime date;
    private float elapsedTime;

    private Player player;
    private Player shadow;

    private PlayerActions playerActions;

    public Session(Player player, Player shadow) {
        id = uniqueId++;
        playerActions = new PlayerActions();
    }

    public void start() {
        date = DateTime.Now;
    }

    public void end() {
        TimeSpan elapsed = DateTime.Now - date;
        elapsedTime = elapsed.Milliseconds;
    }

    public void save(String username)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Data/Users/" + username + "/Sessions/" + id + ".dat");
        bf.Serialize(file, this);
        Debug.Log("Saved on " + id + ".dat");
        file.Close();
    }
    
    public static PlayerActions load(String username, int id)
    {
        if (File.Exists(Application.dataPath + "/Data/Users/" + username + "/Sessions/" + id + ".dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Data/Users/" + username + "/Sessions/" + id + ".dat", FileMode.Open);

            PlayerActions data = (PlayerActions)bf.Deserialize(file);
            file.Close();

            return data;
        }

        return null;
    }

}
