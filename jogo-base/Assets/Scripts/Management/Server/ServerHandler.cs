using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class ServerHandler 
{
    private static string path = "localhost:";
    private static string port = "8080";

    public Dictionary<string, object> request(string endpoint)
    {
        WWW www = new WWW(path + port + endpoint);
        WaitForSeconds w;
        while (!www.isDone)
            w = new WaitForSeconds(0.1f);
        //StartCoroutine(WaitForRequest(www));
        
        return parseToDicionary(www.text);
    }

    public Dictionary<string, object> postRequest(string endpoint, WWWForm body)
    {
        body.headers["Content-Type"]= "application/json";
        WWW www = new WWW(path + port + endpoint, body);
        WaitForSeconds w;

        while (!www.isDone)
            w = new WaitForSeconds(0.1f);

        return parseFromPostToDicionary(www.text);
    }


    private Dictionary<string, object> parseToDicionary(string response)
    {
        if (response == "")
            return null;

        JSONObject jsonObject = new JSONObject(response);

        Dictionary<string, object> mainDicionary = new Dictionary<string, object>();
        mainDicionary.Add("result", jsonObject.list[0]);

        List<Dictionary<string, object>> contentDicionary = new List<Dictionary<string, object>>();
        List<JSONObject> information = jsonObject.list[1].list;

        if (information.Count == 0)
            return null;
        else
        {
            for (int x = 0; x < information.Count; x++)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                for (int i = 0; i < information[x].Count; i++)
                {
                    dic.Add(information[x].keys[i], information[x].list[i]);
                }
                contentDicionary.Insert(x, dic);
            }
            mainDicionary.Add("content", contentDicionary);
        }

        return mainDicionary;
    }

    private Dictionary<string, object> parseFromPostToDicionary(string response)
    {
        if (response == "")
            return null;

        JSONObject jsonObject = new JSONObject(response);

        Dictionary<string, object> mainDicionary = new Dictionary<string, object>();
        mainDicionary.Add("result", jsonObject.list[0]);
        mainDicionary.Add("content", jsonObject.list[1]);

        return mainDicionary;
    }
}
