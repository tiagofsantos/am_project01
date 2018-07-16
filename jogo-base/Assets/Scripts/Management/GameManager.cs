using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState { MENU, INGAME }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EntitySpawner spawner;
    public SessionManager sessionManager;
    public ServerHandler serverManager;

    private GameObject playerObject;
    private GameObject opponentObject;

    public User userLogged;
    public GameType gameType;
    public User userAgainst;
    public int levelChoosed;
    public Character characterChoosed;

    private GameState state;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        state = GameState.MENU;
        userAgainst = null;
        characterChoosed = null;
        serverManager = new ServerHandler();
    }

    public void play()
    {
        spawner = new EntitySpawner();
        sessionManager = new SessionManager();
        Session opponentSession;
        Session currentSession;

        if (userAgainst == null){
            opponentSession = null;
        }else{
            opponentSession = sessionManager.load(5);
        }

        currentSession = sessionManager.create(userLogged, characterChoosed, opponentSession);
        
        playerObject = spawner.spawnPlayer(currentSession.user, currentSession.character);

        if (opponentSession != null)
        {
            opponentObject = spawner.spawnShadow(opponentSession.user, opponentSession.character);

            if (opponentSession.actions != null)
            {
                ActionReplay replay = opponentObject.GetComponent<ActionReplay>();
                replay.actions = new List<PlayerAction>();
                replay.actions.AddRange(opponentSession.actions);
            }
        }

        state = GameState.INGAME;
    }

    void Update()
    {
        if (state == GameState.INGAME)
        {
            sessionManager.getCurrentSession().elapsedTime += Time.deltaTime;
        }
    }

    private void OnApplicationQuit()
    {
        if (state == GameState.INGAME)
        {
            ActionTracker tracker = playerObject.GetComponent<ActionTracker>();
            Debug.Log(tracker.actions.Count);
            sessionManager.getCurrentSession().actions.AddRange(tracker.actions);
            sessionManager.save();
        }
    }

    public User getLocalUser()
    {
        return sessionManager.getCurrentSession().user;
    }

    public Session getLocalSession()
    {
        return sessionManager.getCurrentSession();
    }
}
