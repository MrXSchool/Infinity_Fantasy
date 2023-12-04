using System.Collections.Generic;

[System.Serializable]
public class MapModel
{
    public PlayerModel player;
    public List<EnemyModel> enermy;
    public string sceneName;

    public MapModel(PlayerModel player, List<EnemyModel> enermy, string sceneName)
    {
        this.player = player;
        this.enermy = enermy;
        this.sceneName = sceneName;
    }
}
