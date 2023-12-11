using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerModel
{
    public float hp, mana, maxHP, maxMana;
    public float[] position;
    public string playerName;
    public string playerAvatar;


    public PlayerModel(PlayerScript player)
    {
        playerName = player.playerName.Split('(')[0];
        playerAvatar = player.playerAvatar;
        hp = player.hp;
        mana = player.mana;
        maxHP = player.maxHP;
        maxMana = player.maxMana;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
    [JsonConstructor]
    public PlayerModel(float hp, float mana, float maxHP, float maxMana, float[] position, string playerName, string playerAvatar)
    {
        this.hp = hp;
        this.mana = mana;
        this.maxHP = maxHP;
        this.maxMana = maxMana;
        this.position = position;
        this.playerName = playerName;
        this.playerAvatar = playerAvatar;
    }


}

