using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public GameObject prefabToSpawn;
    // public string namePrefab;
    public GameObject player;
    public GameObject[] enemy;
    public string namePlayer;
    public string hp;
    public string mp;
    public float[] potision;

    LoadingScript loadingScript;


    // Start is called before the first frame update

    void Start()
    {

        loadingScript = GameObject.Find("Loading").GetComponent<LoadingScript>();
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void testSpawn(string namePrefab, float[] potision)
    {

        Instantiate(Resources.Load<GameObject>(namePrefab), new Vector3(potision[0], potision[1], potision[2]), Quaternion.identity);


    }

    public void saveToJson()
    {
        Time.timeScale = 0;
        string sceneName = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectsWithTag("enemy");
        List<EnemyModel> enemies = new List<EnemyModel>();
        foreach (GameObject e in enemy)
        {
            Enemy enemy = e.GetComponent<Enemy>();
            EnemyModel enemydata = new EnemyModel(enemy);
            enemies.Add(enemydata);
        }
        PlayerScript players = this.player.GetComponent<PlayerScript>();
        PlayerModel playerdata = new PlayerModel(players);
        MapModel mapdata = new MapModel(sceneName, playerdata, enemies);
        string json = JsonUtility.ToJson(mapdata);
        System.IO.File.WriteAllText(Application.dataPath + "/Data/test/" + sceneName + ".json", json);
        Time.timeScale = 1;
    }

    public void loadFromJson(string sceneName)
    {
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Data/test/" + sceneName + ".json");
        MapModel map = JsonConvert.DeserializeObject<MapModel>(json);
        PlayerModel player = map.player;
        int Count = 0;
        foreach (EnemyModel enemy in map.enermy)
        {
            //hp, speed, start, end, damage;

            string enemyName = enemy.enemyName;
            float enemyHp = enemy.hp;
            float enemySpeed = enemy.speed;
            float enemyStart = enemy.start;
            float enemyEnd = enemy.end;
            float enemyDamage = enemy.damage;
            float[] enemyPosition = enemy.position;
            testSpawn(enemyName, enemyPosition);

            string enemyNameClone = enemyName + "(Clone)";

            GameObject enemyObject = GameObject.Find(enemyNameClone);
            enemyObject.name = enemyNameClone + Count.ToString();
            Enemy enemyScript = enemyObject.GetComponent<Enemy>();
            enemyScript.hp = enemyHp;
            enemyScript.speed = enemySpeed;
            enemyScript.start = enemyStart;
            enemyScript.end = enemyEnd;
            enemyScript.damage = enemyDamage;

            Count++;
        }
        testSpawn(player.playerName, player.position);
        PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        playerScript.hp = player.hp;
        playerScript.mana = player.mana;
    }

    public int getLevel(string sceneName)
    {
        //intro 0 map1 1 map2 2 map3 3 
        if (sceneName == "intro")
        {
            return 0;
        }
        else if (sceneName == "map1")
        {
            return 1;
        }
        else if (sceneName == "map2")
        {
            return 2;
        }
        else if (sceneName == "map3")
        {
            return 3;
        }
        else
        {
            return 0;
        }

    }
}
