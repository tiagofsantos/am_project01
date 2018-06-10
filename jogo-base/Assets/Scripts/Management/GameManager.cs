using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EntitySpawner spawner;
    public SessionManager sessionManager;
    public ServerHandler serverManager;

    private GameObject playerObject;
    private GameObject opponentObject;

    /* Test values */
    //private int testSessionId = 1;
    private String testUsername = "Ruben";

    private Character testCharacter = new Scout();

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
        serverManager = new ServerHandler();
        spawner =  new EntitySpawner();
        sessionManager = new SessionManager();

        User ruben = new User(2, testUsername, "ruben.amendoeira@gmail.com", DateTime.ParseExact("06/12/1996", "dd/MM/yyyy", null), "teste123", "PT");
        User hugo = new User(1, "Hugo", "hugobenfiquista@live.com.pt", DateTime.ParseExact("25/05/1997", "dd/MM/yyyy", null), "teste123", "PT");

        testOpSession = new Session(1,hugo, new Buster(), null);

        testOpSession.actions.Add(new PlayerAction(ActionType.JUMP, 1.2f, 1));
        testOpSession.actions.Add(new PlayerAction(ActionType.MOVE_LEFT, 2f, 1));
        testOpSession.actions.Add(new PlayerAction(ActionType.JUMP, 2.5f, 1));

        sessionManager.create(2,ruben, testCharacter, testOpSession);
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
    }

    void Update()
    {
        sessionManager.getCurrentSession().elapsedTime += Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        ActionTracker tracker = playerObject.GetComponent<ActionTracker>();
        sessionManager.getCurrentSession().actions.AddRange(tracker.actions);
        sessionManager.save();
    }

    public User getLocalUser() {
        return sessionManager.getCurrentSession().user;
    }

    public Session getLocalSession() {
        return sessionManager.getCurrentSession();
    }

}
