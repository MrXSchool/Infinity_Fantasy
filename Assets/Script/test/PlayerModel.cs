[System.Serializable]
public class PlayerModel
{
    public float hp, mana;
    public float[] position;

    public PlayerModel(PlayerScript player)
    {
        hp = player.hp;
        mana = player.mana;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }



}
