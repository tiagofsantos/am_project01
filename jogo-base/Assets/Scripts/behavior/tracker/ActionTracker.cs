using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ActionTracker : MonoBehaviour {

    private static PlayerActions actions;

	void Start () {
        actions = new PlayerActions();
	}

    /* Adiciona uma nova ação, no tempo actual. */
    public static void addAction(ActionType type) {
        actions.actionList.Add(new PlayerAction(type, Time.time));
    }

    private void OnApplicationQuit()
    {
        //Save();  desativado temporáriamente, deverá ser ativado quando este sistema estiver completo.
    }

    /* Guarda as ações desta última sessão em ficheiro. */
    public void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerActionsInfo.dat");
        bf.Serialize(file, actions);
        file.Close();
    }

    /* Faz load da última sessão em ficheiro e retorna uma instancia de PlayerActions equivalente. */
    public static PlayerActions Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerActionsInfo.dat")) {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerActionsInfo.dat", FileMode.Open);

            PlayerActions data = (PlayerActions) bf.Deserialize(file);
            file.Close();

            return data;
        }

        return null;
    }
}
