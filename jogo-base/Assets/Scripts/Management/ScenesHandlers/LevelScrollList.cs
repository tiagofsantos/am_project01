using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelScrollList : MonoBehaviour {
    /* list of levels */
    public List<string> levelList;
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

    /* Add level buttons to content panel */
    public void addButtons()
    {
        loadLevels();
        for (int i=0;i< levelList.Count; i++)
        {
            string info = levelList[i];
            var index = levelList[i].Length;
            info = info.Remove(0, 1);
            info = info.Remove(index - 2, 1);
            GameObject newButton = btnObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            ButtonUI button = newButton.GetComponent<ButtonUI>();
            button.setup(info);
            buttons.Add(button.button);
            button.button.onClick.AddListener(() => addColor(button)); 
        }
    }

    /* Put the colors of buttons to white */
    private void resetColorButtons()
    {
        for (int i = 0; i < buttons.Count; i++) {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
    }

    /* Add color cyan to button passed */
    private void addColor(ButtonUI button)
    {
        resetColorButtons();
        button.button.GetComponent<Image>().color = Color.cyan;
        string[] splitString = button.textInfo.text.Split(' ');
        GameManager.instance.levelChoosed = int.Parse(splitString[1]);
    }

    /* request levels to server and add to a list of levels */
    private void loadLevels()
    {
        ServerHandler server = GameManager.instance.serverManager;
        Dictionary<string, object> dic;
        if (GameManager.instance.userAgainst != null){
            dic = server.request("/sessions/levels/" + GameManager.instance.userAgainst.id);
        } else {
            dic = server.request("/levels/");
        }
           
        List<Dictionary<string, object>> information = ((List<Dictionary<string, object>>)dic["content"]);
        LevelScrollList list = new LevelScrollList();
        for (int i = 0; i < information.Count; i++)
        {
            levelList.Add(information[i]["nome"].ToString());
        }
    }

}
