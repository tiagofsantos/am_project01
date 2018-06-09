using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Session
{
    public int id;

    private DateTime date;
    public float elapsedTime;

    public User user;
    public Character character;
    public Session opponentSession;

    public List<PlayerAction> actions;

    public Session(User user, Character character, Session opponentSession)
    {
        id = new System.Random().Next(999999);
        date = DateTime.Now;
        actions = new List<PlayerAction>();
        elapsedTime = 0;

        this.user = user;
        this.character = character;
        this.opponentSession = opponentSession;
    }

    public void end()
    {
        TimeSpan elapsed = DateTime.Now - date;
        elapsedTime = elapsed.Milliseconds;
    }
}
