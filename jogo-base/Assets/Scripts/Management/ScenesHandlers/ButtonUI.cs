using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour {

    public Button button;
    public Text textInfo;

	public void setup(string info) {
        textInfo.text = info;   
    }

   
}
