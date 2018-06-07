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


    public Session(int id, User user, Character character, Session opponentSession) :
        this(id, user, character, opponentSession, new List<PlayerAction>()){ }
    

    public Session(int id, User user, Character character, Session opponentSession, List<PlayerAction> actions)
    {
        this.id = id;
        date = DateTime.Now;
        this.actions = actions;
        elapsedTime = 0;
        this.user = user;
        this.character = character;
        this.opponentSession = opponentSession;
    }

}
