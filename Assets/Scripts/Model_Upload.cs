using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alteruna;
public class Model_Upload : CommunicationBridge
{
    private const string MONGO_URI = "mongodb+srv://admin:admin@clusterprueba.bs7yuuj.mongodb.net/";
    private const string DATABASE_NAME = "juego";
    private MongoClient client;
    private IMongoDatabase db;
    private Model_User e;
    public InputField infi;
    public InputField inise;
    private IMongoCollection<Model_User> userCollection;

    void Start()
    {
        Application.targetFrameRate = 60;
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        userCollection = db.GetCollection<Model_User>("juego");
    }

    public void Registrar()
    {
        if (infi.text != "¡Registrado!" || infi.text != "Ya existe")
        {
            List<Model_User> userModelList = userCollection.Find(user => user.UserId == infi.text).ToList();
            if(userModelList.Count == 1){
                infi.text = "Ya existe";
            }else{
            e = new Model_User();
            e.UserId = infi.text;
            userCollection.InsertOne(e);

            infi.text = "¡Registrado!";
            }
        }

    }

    public void IniciarSesion()
    {
        List<Model_User> usu = userCollection.Find(user => user.UserId == inise.text).ToList();
        foreach (Model_User x in usu)
        {
            inise.text = "Hola, " + x.UserId;
            Multiplayer.SetUsername(x.UserId);
            Multiplayer.Connect();
        }
    }
}

