
[System.Serializable]
public class EnemyModel
{
    public string enemyName;
    public float hp, speed, start, end, damage;
    public float[] position;

    public EnemyModel(Enemy enemy)
    {
        enemyName = enemy.enemyName;
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
}
