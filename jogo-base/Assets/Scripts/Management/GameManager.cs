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
  
    /* Test values */

    private Session testOpSession;

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

    public void playMulti()
    {   
        spawner =  new EntitySpawner();
        sessionManager = new SessionManager();
        
        testOpSession = new Session(1, userAgainst , new Buster(), null);

        testOpSession.actions.Add(new PlayerAction(ActionType.JUMP, 1.2f, 1));
        testOpSession.actions.Add(new PlayerAction(ActionType.MOVE_LEFT, 2f, 1));
        testOpSession.actions.Add(new PlayerAction(ActionType.JUMP, 2.5f, 1));

        sessionManager.create(2, userLogged, characterChoosed, testOpSession);
        sessionManager.load(2);
       
        Session currentSession = sessionManager.getCurrentSession();
        Session opponentSession = sessionManager.getOpponentSession();

        playerObject = spawner.spawnPlayer(currentSession.user, currentSession.character);

        if (opponentSession != null)
        {
            opponentObject = spawner.spawnShadow(opponentSession.user, opponentSession.character);

            ActionReplay replay = opponentObject.GetComponent<ActionReplay>();
            replay.actions = new List<PlayerAction>();
            replay.actions.AddRange(opponentSession.actions);
        }
        state = GameState.INGAME;

    }

    public void playSolo()
    {
        Debug.Log("Completar");
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
            sessionManager.getCurrentSession().actions.AddRange(tracker.actions); 
            sessionManager.save();
        }
    }

    public User getLocalUser() {
        return sessionManager.getCurrentSession().user;
    }

    public Session getLocalSession() {
        return sessionManager.getCurrentSession();
    }


}
