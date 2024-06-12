using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using TMPro;
public class Rondas : AttributesSync
{
       [SynchronizableMethod]
       public void CambiarTexto(string r){
       GameObject.Find("Canvas/Ronda").GetComponent<TextMeshProUGUI>().SetText(r);
       Commit();
       }
}
