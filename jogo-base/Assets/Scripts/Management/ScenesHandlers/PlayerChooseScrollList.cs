using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PlayerChooseScrollList : MonoBehaviour {
    /* list of users */
    public List<User> userList;
    /* Object pool of button */
    public ObjectPool btnObjectPool;
    /* Content panel where buttons are */
    public Transform contentPanel;
    /* list of buttons */
    public List<Button> buttons;

    private void Start()
    {
        refreshDisplay();
    }

    public void refreshDisplay()
    {
        addButtons();
    }

    /* Add friends buttons to content panel */
    public void addButtons()
    {
        loadFriends();
        for (int i = 0; i < userList.Count; i++)
        {
            User user = userList[i];
            GameObject newButton = btnObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            ButtonUI button = newButton.GetComponent<ButtonUI>();
            button.setup(user.name);
            buttons.Add(button.button);
            button.button.onClick.AddListener(() => addColor(button));
        }
    }

    /* Put the colors of buttons to white */
    private void resetColorButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
    }

    /* Add color cyan to button passed */
    private void addColor(ButtonUI button)
    {
        resetColorButtons();
        button.button.GetComponent<Image>().color = Color.cyan;
        string username = button.textInfo.text;
        GameManager.instance.userAgainst = haveUser(username);
    }

    /* Check if username is in exists in list */
    private User haveUser(string username)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            if (userList[i].name.Trim() == username.Trim())
            {
                return userList[i];
            }
        }
        return null;

    }

    /* Load friends from a user logged, adding to a list */
    private void loadFriends()
    {
        ServerHandler server = GameManager.instance.serverManager;
        Dictionary<string, object> friendsDic = server.request("/friends/"+ GameManager.instance.userLogged.id);
        List<Dictionary<string, object>> friendsInformation = ((List<Dictionary<string, object>>)friendsDic["content"]);
        PlayerChooseScrollList list = new PlayerChooseScrollList();
        for (int i = 0; i < friendsInformation.Count; i++)
        {
            int id = int.Parse(friendsInformation[i]["idUtilizador"].ToString());
            string name = removeQuontationsMarks(friendsInformation[i]["username"].ToString());
            string email = removeQuontationsMarks(friendsInformation[i]["email"].ToString());
            DateTime dataN = DateTime.ParseExact(friendsInformation[i]["dataNascimento"].ToString(), "'\"'yyyy-MM-dd'T'HH:mm:ss.fff'Z\"'", null);
            string pais = removeQuontationsMarks(friendsInformation[i]["pais"].ToString());
 
            User user= new User(id, name, email, dataN, "", pais);
            userList.Add(user);
        }
    }


    private string removeQuontationsMarks(string info){
        var index = info.Length;
        info = info.Remove(0, 1);
        info = info.Remove(index - 2, 1);
        return info;
    }
}
