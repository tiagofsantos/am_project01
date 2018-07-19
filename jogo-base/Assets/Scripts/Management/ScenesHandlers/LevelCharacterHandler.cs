using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCharacterHandler : MonoBehaviour
{
    /* Buster character button */
    public Button btnBuster;
    /* Scout character button */
    public Button btnScout;
    /* Sargent character button */
    public Button btnSargent;

    /* Put the colors of buttons to white */
    private void resetColorButtons()
    {
        btnBuster.GetComponent<Image>().color = Color.white;
        btnScout.GetComponent<Image>().color = Color.white;
        btnSargent.GetComponent<Image>().color = Color.white;
    }

    /* Buster button clicked, add to characterChossed a new instance of a Buster character */
    public void chooseBuster()
    {
        resetColorButtons();
        GameManager.instance.characterChoosen = new Buster();
        btnBuster.GetComponent<Image>().color = Color.cyan;
    }

    /* Scout button clicked, add to characterChossed a new instance of a Scout character */
    public void chooseScout()
    {
        resetColorButtons();
        GameManager.instance.characterChoosen = new Scout();
        btnScout.GetComponent<Image>().color = Color.cyan;
    }

    /* Sargent button clicked, add to characterChossed a new instance of a Sargent character */
    public void chooseSargent()
    {
        resetColorButtons();
        GameManager.instance.characterChoosen = new Sargent();
        btnSargent.GetComponent<Image>().color = Color.cyan;
    }

    /* if the level and character are chossed inicialize the game */
    public void play()
    {
        if (GameManager.instance.characterChoosen != null)
        {
            if (GameManager.instance.levelChoosen == 1)
            {
                SceneManager.LoadScene("Scenes/Level1");
            }
            else if (GameManager.instance.levelChoosen == 2)
            {
                SceneManager.LoadScene("Scenes/Level2");
            }
        }
    }

    /* button back */
    public void back()
    {
        SceneManager.LoadScene("Scenes/GameTypeChoose");

    }
}
