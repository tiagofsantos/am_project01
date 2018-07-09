using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChooseHandler : MonoBehaviour {

    /* Button confirm */
    public void confirm()
    {
        if (GameManager.instance.userAgainst != null)
        {
            SceneManager.LoadScene("Scenes/LevelCharacterChoose");
        }      
    }

    /* Button back */
    public void back()
    {
        SceneManager.LoadScene("Scenes/GameTypeChoose");

    }
}
