using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UI;

public class Usernames : CommunicationBridge
{
    private Text tm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IniciarSesion(){
      tm = GameObject.Find("Canvas/FondoMenu/IniciarSesion/Texto").GetComponent<Text>();
      base.OnEnable();
      Multiplayer.SetUsername(tm.text);
      Multiplayer.Connect();
    }

    public void IniciarSesionSinUser(){
        Multiplayer.Connect();
    }
}
