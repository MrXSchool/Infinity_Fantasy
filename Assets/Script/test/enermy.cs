[System.Serializable]

public class enermy
{
    public float hp = 100;
    public float mana = 100;
    public float walkSpeed = 5f;

    public enermy(enermy enermy)
    {
        hp = enermy.hp;
        mana = enermy.mana;
        walkSpeed = enermy.walkSpeed;
    }

}
