using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SessionManager 
{
    private Session currentSession;

    public void create(int id,User user, Character character, Session opponentSession)
    {
        currentSession = new Session(id,user, character, opponentSession);
    }

    public Session getCurrentSession()
    {
        return currentSession;
    }

    public Session getOpponentSession()
    {
        return currentSession.opponentSession;
    }

    public Session load( int id)
    {
        return getSessionFromDB(id,false);
    }

    private List<PlayerAction> getPlayerActions(int sessionID)
    {
        List<PlayerAction> actions = new List<PlayerAction>() ;
        Dictionary<string, object> actionsDic = GameManager.instance.serverManager.request("/actions/" + sessionID);

        if (actionsDic == null || actionsDic["result"].ToString() == false.ToString())
            return null;

        List <Dictionary<string, object>> actionsInformation = ((List<Dictionary<string, object>>)actionsDic["content"]);

        for(int i=0; i < actionsInformation.Count; i++)
        {
            ActionType actionType = ActionTypeHandler.getActionType(actionsInformation[i]["açao"].ToString());
            float timestamp = float.Parse(actionsInformation[i]["tempoAtual"].ToString());
            int previousExecutions = int.Parse(actionsInformation[i]["anteriorExecucao"].ToString());
            PlayerAction playerAction = new PlayerAction(actionType, timestamp, previousExecutions);
            actions.Insert(i,playerAction);
        }

        return actions;
    }

    private User getUSerFromDB(int idUser){

        Dictionary<string, object> userDic = GameManager.instance.serverManager.request("/users/" + idUser);

        if (userDic == null || userDic["result"].ToString() == false.ToString())
            return null;

        Dictionary<string, object> userInformation = ((List<Dictionary<string, object>>)userDic["content"])[0];

        int id = int.Parse(userInformation["idUtilizador"].ToString());
        string name = userInformation["username"].ToString();
        string email = userInformation["email"].ToString();
        DateTime dataN = DateTime.ParseExact(userInformation["dataNascimento"].ToString(), "'\"'yyyy-MM-dd'T'HH:mm:ss.fff'Z\"'", null);  
        string pw = userInformation["pw"].ToString();
        string pais = userInformation["pais"].ToString();

        return new User(id, name, email, dataN, pw, pais);
    }


    private Session getSessionFromDB(int id, bool isAgainstSession) {
        Dictionary<string, object> sessionDic = GameManager.instance.serverManager.request("/sessions/" + id);
        if (sessionDic == null ||  sessionDic["result"].ToString() == false.ToString())
            return null;
   
        Dictionary<string, object> sessionInformation = ((List<Dictionary<string, object>>)sessionDic["content"])[0];

        User user=getUSerFromDB(int.Parse(sessionInformation["idUtilizador"].ToString()));

        Session sessionAgainst=null;
        if(!isAgainstSession && sessionInformation["idSessaoContra"].ToString() != "-1"){
            sessionAgainst=getSessionFromDB(int.Parse(sessionInformation["idSessaoContra"].ToString()),true);
        }

        List<PlayerAction> playerActions = getPlayerActions(id);

        Session session = new Session(int.Parse(sessionInformation["idSessao"].ToString()),user, getCharacter(sessionInformation["personagem"].ToString()), sessionAgainst, playerActions);
        return session;
   }


    private Character getCharacter(String name)
    {
        switch (name)
        {
            case "Scout":return new Scout();
            case "Buster": return new Buster();
            case "Sargent": return new Sargent();
            default: return new Scout();
        }
    }


    private WWWForm sectionToWWWForm(Session session)
    {
        WWWForm form = new WWWForm();
        string date = session.date.ToString("yyyy/MM/dd");
        string id = session.user.id.ToString();
        form.AddField("elapsedTime", session.elapsedTime.ToString());
        form.AddField("date", date);
        form.AddField("level", 1.ToString());
        form.AddField("opponentSession", ((session.opponentSession != null) ? session.opponentSession.id : -1).ToString());
        form.AddField("character", session.character.name);
        form.AddField("userID", session.user.id.ToString());
        return form;
    }

    private WWWForm actionsToWWWForm(PlayerAction action,int sessionID)
    {      
        WWWForm form = new WWWForm();
        form.AddField("action", action.type.ToString());
        form.AddField("timestamp", action.timestamp.ToString());
        form.AddField("anteriorExecucao", action.previousExecutions.ToString());
        form.AddField("sectionID",sessionID.ToString());
        return form;
    }


    public void save()
    {
        WWWForm body = sectionToWWWForm(currentSession);
        Dictionary<string, object>  dic = GameManager.instance.serverManager.postRequest("/sessions", body);
        if(dic==null)
            Debug.Log("Não guardado");
        else
        {
            currentSession.id = int.Parse(dic["content"].ToString());
            Debug.Log("Saved, id: " + currentSession.id + "");
        }

        List<PlayerAction> actions = currentSession.actions;
        actions.Add(new PlayerAction(ActionType.JUMP, 1.2f, 1));

        for (int i = 0; i < actions.Count;i++){
            WWWForm actionBody = actionsToWWWForm(actions[i], currentSession.id);
            Dictionary<string, object> dicAction = GameManager.instance.serverManager.postRequest("/actions", actionBody);
            if(dicAction==null)
                Debug.Log("Não guardado");
            else
                Debug.Log("Saved, id: " + dicAction["content"].ToString() + "");
        }
    }
}
