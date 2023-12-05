using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
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


    public void Spawn(string namePrefab, float[] potision)
    {

        Instantiate(Resources.Load<GameObject>(namePrefab), new Vector3(potision[0], potision[1], potision[2]), Quaternion.identity);


    }

    public void saveToJson()
    {
        Time.timeScale = 0;
        try
        {

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
            string file = System.IO.File.ReadAllText(Application.dataPath + "Data/user" + PlayerPrefs.GetString("username") + ".json");
            User user = JsonConvert.DeserializeObject<User>(file);
            List<MapModel> mapModels = user.data;
            bool isExist = false;
            foreach (MapModel map in mapModels)
            {
                if (map.sceneName == mapdata.sceneName)
                {
                    map.player = mapdata.player;
                    map.enermy = mapdata.enermy;
                    isExist = true;
                    Debug.Log("Map đã được ghi đè");
                }
            }
            if (!isExist)
            {
                mapModels.Add(mapdata);
                Debug.Log("Đã lưu map");
            }




        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void loadFromJson(string sceneName)
    {
        //xóa quái mặc định trong scene
        if (GameObject.FindGameObjectsWithTag("enemy") != null)
        {
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject e in enemy)
            {
                Destroy(e);
            }
        }
        //xóa player mặc định trong scene
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player");
            Destroy(player1);
        }
        try
        {
            string json = System.IO.File.ReadAllText(Application.dataPath + "/Data/user/" + PlayerPrefs.GetString("username") + ".json");
            User user = JsonConvert.DeserializeObject<User>(json);
            List<MapModel> mapModels = user.data;
            foreach (MapModel map in mapModels)
            {
                if (map.sceneName == sceneName)
                {
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
                        Spawn(enemyName, enemyPosition);

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
                    Spawn(player.playerName, player.position);
                    PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
                    playerScript.hp = player.hp;
                    playerScript.mana = player.mana;
                }
            }
        }
        catch
        {
            Debug.Log("No file");
        }
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
