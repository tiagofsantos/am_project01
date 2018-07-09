using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Login : MonoBehaviour {

    public TMPro.TMP_InputField txtUsername;
    public InputField txtPassword;
    public Text informationText;

    private void Start()
    {
        informationText.enabled=false;
        txtPassword.contentType = InputField.ContentType.Password;
    }

    /* Button to login, request to server the user with the credetencials from inputs*/
    public void LogginButton()
    {
        ServerHandler server = GameManager.instance.serverManager;
        WWWForm body = userToWWWForm(txtUsername.text, txtPassword.text);
        Dictionary<string, object> resp = server.postRequest("/users/",body);
        if (resp["result"].ToString() == "true")
        {
            Dictionary<string, object> userInformation = ((List<Dictionary<string, object>>)resp["content"])[0];
            int id = int.Parse(userInformation["idUtilizador"].ToString());
            string name = removeQuontationsMarks(userInformation["username"].ToString());
            string email = removeQuontationsMarks(userInformation["email"].ToString());
            DateTime dataN = DateTime.ParseExact(userInformation["dataNascimento"].ToString(), "'\"'yyyy-MM-dd'T'HH:mm:ss.fff'Z\"'", null);
            string pw = removeQuontationsMarks(userInformation["pw"].ToString());
            string pais = removeQuontationsMarks(userInformation["pais"].ToString());
            GameManager.instance.userLogged = new User(id, name, email, dataN, pw, pais);
            SceneManager.LoadScene("Scenes/GameTypeChoose");
        }
        else
        {
            informationText.text = removeQuontationsMarks(resp["content"].ToString());
            informationText.enabled = true;
        }
    }

    /* Create a WWWform with the credentials information */
    private WWWForm userToWWWForm(string username,string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        return form;
    }

    private string removeQuontationsMarks(string info)
    {
        var index = info.Length;
        info = info.Remove(0, 1);
        info = info.Remove(index - 2, 1);
        return info;
    }
}
