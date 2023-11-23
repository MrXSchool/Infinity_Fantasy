using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWJson : MonoBehaviour
{
    public GameObject player;
    public enermy enermy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public void SaveToJson()
    {
        PlayerScript players = this.player.GetComponent<PlayerScript>();
        PlayerModel playerdata = new PlayerModel(players);
        enermy enermydata = new enermy(enermy);
        MapModel map = new MapModel(playerdata, enermydata);
        string json = JsonUtility.ToJson(map);
        System.IO.File.WriteAllText(Application.dataPath + "/Data/test/Map.json", json);
    }


}
