using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Alteruna;

public class Menus : CommunicationBridge
{
    private GameObject go;
    private GameObject gocanvas;
    private bool mostrar = true;
    void Start()
    {

    }

    public void controlarMenu()
    {
        go = GameObject.Find("Canvas/Menu");
        gocanvas = GameObject.Find("Canvas/FondoMenu");

        if (mostrar)
        {
            go.GetComponentInChildren<TMP_Text>().text = "Abrir Menú";
            go.GetComponent<CanvasRenderer>().SetAlpha(0.5f);
            mostrar = false;
            gocanvas.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            go.GetComponentInChildren<TMP_Text>().text = "Cerrar Menú";
            go.GetComponent<CanvasRenderer>().SetAlpha(1f);
            mostrar = true;
            gocanvas.transform.localScale = new Vector3(1, 1, 1);

            Multiplayer.CurrentRoom.Leave();
        }
        EventSystem.current.SetSelectedGameObject(gocanvas);
    }

    void OnApplicationQuit()
    {
        Multiplayer.CurrentRoom.Leave();
    }
}
