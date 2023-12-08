
using Newtonsoft.Json;

[System.Serializable]
public class EnemyModel
{
    public string enemyName;
    public float hp, speed, start, end, damage;
    public float[] position;

    public EnemyModel(EnemyScript enemy)
    {
        enemyName = enemy.nameEnemy;
        hp = enemy.hp;
        speed = enemy.speed;
        start = enemy.start;
        end = enemy.end;
        damage = enemy.damage;
        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }

    [JsonConstructor]
    public EnemyModel(string enemyName, float hp, float speed, float start, float end, float damage, float[] position)
    {
        this.enemyName = enemyName;
        this.hp = hp;
        this.speed = speed;
        this.start = start;
        this.end = end;
        this.damage = damage;
        this.position = position;
    }
}
