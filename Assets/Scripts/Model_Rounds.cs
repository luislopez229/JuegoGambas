using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class Model_Rounds
{
    public ObjectId _id { set; get; }
    public string jug1 { set; get; }
    public string jug2 { set; get; }
    public int numronda { set; get; }
    public int ptosjug1 { set; get; }
    public int ptosjug2 { set; get; }
}
