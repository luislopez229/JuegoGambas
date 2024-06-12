using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
// Model_User Sample
public class Model_User
{
    public ObjectId _id { set; get; }
    public string UserId { set; get; }
    public string pass { set; get; }
    public int points { set; get; }

}
