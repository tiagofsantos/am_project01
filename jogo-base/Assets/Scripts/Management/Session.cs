using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Session
{
    private int id;

    private DateTime date;
    private float elapsedTime;

    private Player player;
    private Player shadow;

    public List<PlayerAction> playerActions;

    public Session(int id, Player player, Player shadow)
    {
        this.id = id;
        this.player = player;
        this.shadow = shadow;
        playerActions = new List<PlayerAction>();
    }

    public void start()
    {
        date = DateTime.Now;
    }

    public void end()
    {
        TimeSpan elapsed = DateTime.Now - date;
        elapsedTime = elapsed.Milliseconds;
    }

    public void save(String username, ActionTracker tracker)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Data/Users/" + username + "/Sessions/" + id + ".dat");

        playerActions = tracker.actions;

        bf.Serialize(file, this);

        Debug.Log("Saved on " + id + ".dat (" + playerActions.Count + ")");

        file.Close();
    }

    public static Session load(String username, int id)
    {
        if (File.Exists(Application.dataPath + "/Data/Users/" + username + "/Sessions/" + id + ".dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Data/Users/" + username + "/Sessions/" + id + ".dat", FileMode.Open);

            Session data = (Session)bf.Deserialize(file);
            file.Close();

            return data;
        }

        return null;
    }

}
