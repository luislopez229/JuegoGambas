using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using MongoDB.Driver;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class Match : CommunicationBridge
{
    private List<Transform> tl;
    private List<Alteruna.Avatar> la;
    private GameObject go;
    private Rondas r;
    private const string MONGO_URI = "mongodb+srv://admin:admin@clusterprueba.bs7yuuj.mongodb.net/";
    private const string DATABASE_NAME = "juego";
    private MongoClient client;
    private IMongoDatabase db;
    private Model_Rounds e;
    private IMongoCollection<Model_Rounds> userCollection;
    Collider2D col;
    bool bo;
    bool unavez;
    

    void Awake()
    {
        unavez=false;
        r = new Rondas();
    }
    void Start()
    {
        la = new List<Alteruna.Avatar>();

        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        userCollection = db.GetCollection<Model_Rounds>("rondas");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        la = Multiplayer.GetAvatars();
        int a = Random.Range(0, 2);
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<Rigidbody2DSynchronizable>().velocity = Vector2.zero;
            other.transform.GetComponent<Rigidbody2DSynchronizable>().position = Multiplayer.AvatarSpawnLocations[a].position;
            TextMesh t = other.transform.GetChild(1).GetComponent<TextMesh>();


            if (t.text.Equals(la[0].Possessor.Name))
            {
                userCollection.UpdateOne(ro => (ro.jug1 == t.text && ro.jug2 == la[1].Possessor.Name), Builders<Model_Rounds>.Update.Inc(ro => ro.ptosjug2, 1));
                userCollection.UpdateOne(ro => (ro.jug1 == t.text && ro.jug2 == la[1].Possessor.Name), Builders<Model_Rounds>.Update.Inc(ro => ro.numronda, 1));
            }
            if (t.text.Equals(la[1].Possessor.Name))
            {
                userCollection.UpdateOne(ro => (ro.jug2 == t.text && ro.jug1 == la[0].Possessor.Name), Builders<Model_Rounds>.Update.Inc(ro => ro.ptosjug1, 1));
                userCollection.UpdateOne(ro => (ro.jug2 == t.text && ro.jug1 == la[0].Possessor.Name), Builders<Model_Rounds>.Update.Inc(ro => ro.numronda, 1));
            }

        }

        if (la.Count == 2)
        {
            List<Model_Rounds> mr = userCollection.Find(r => r.jug1 == la[0].Possessor.Name && r.jug2 == la[1].Possessor.Name).ToList();
            if (mr.Count == 0)
            {
                e = new Model_Rounds();
                e.jug1 = la[0].Possessor.Name;
                e.jug2 = la[1].Possessor.Name;
                e.numronda = 1;

                e.ptosjug1 = 0;
                e.ptosjug2 = 0;
                if(!other.transform.GetChild(1).GetComponent<TextMesh>().text.Equals(e.jug1))
                {e.ptosjug1 = 1;}
                
                else{
                e.ptosjug2 = 1;
                }
                userCollection.InsertOne(e);
            }
            foreach (Model_Rounds x in mr)
            {
                r.CambiarTexto("Ronda: " + x.numronda + "\n" + x.jug1 + ": " + x.ptosjug1 + "\n" + x.jug2 + ": " + x.ptosjug2);
                Multiplayer.Sync(r);
            }

        }
    }

    public void comprobarPuntos()
    {
        string nombre = Multiplayer.Me.Name;

        if (nombre.Equals("") || nombre == null)
        {
            GameObject.Find("Canvas/FondoMenu/Puntos/TextoPuntos").GetComponent<TextMeshProUGUI>().text = "El usuario no existe";
        }
        else
        {
            int puntos = 0;
            List<Model_Rounds> mr = userCollection.Find(r => r.jug1 == nombre).ToList();
            foreach (Model_Rounds x in mr)
            {
                puntos += x.ptosjug1;
            }

            mr = userCollection.Find(r => r.jug2 == nombre).ToList();
            foreach (Model_Rounds x in mr)
            {
                puntos += x.ptosjug2;
            }

            GameObject.Find("Canvas/FondoMenu/Puntos/TextoPuntos").GetComponent<TextMeshProUGUI>().text = "Tienes: "+puntos+" puntos";
        }
    }
}
