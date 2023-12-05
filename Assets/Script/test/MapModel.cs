using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class MapModel
{
    public string sceneName;
    public PlayerModel player;
    public List<EnemyModel> enermy;

    [JsonConstructor]
    public MapModel(string sceneName, PlayerModel player, List<EnemyModel> enermy)
    {
        this.sceneName = sceneName;
        this.player = player;
        this.enermy = enermy;
    }
}
