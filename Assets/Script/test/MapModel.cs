[System.Serializable]
public class MapModel
{
    public PlayerModel player;
    public enermy enermy;
    public MapModel(PlayerModel player, enermy enermy)
    {
        this.player = player;
        this.enermy = enermy;
    }

}
