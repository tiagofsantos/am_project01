using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SessionManager
{
    private Session currentSession;

    public void create(User user, Character character, Session opponentSession)
    {
        currentSession = new Session(user, character, opponentSession);
    }

    public Session getCurrentSession()
    {
        return currentSession;
    }

    public Session getOpponentSession()
    {
        return currentSession.opponentSession;
    }

    public Session load(string username, int id)
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

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Data/Users/" + currentSession.user.name + "/Sessions/" + currentSession.id + ".dat");

        bf.Serialize(file, currentSession);
        file.Close();

        Debug.Log("Saved on " + currentSession.id + ".dat");
    }
}
