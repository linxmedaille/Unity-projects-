using UnityEngine;

public class ressourceManager : MonoBehaviour
{
    private int wood = 0;
    private int stone = 0;
    private int gold = 0;
    private int iron = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int getWood()
    {
        return wood;
    }
    public int getStone()
    {
        return stone;
    }
    public int getGold()
    {
        return gold;
    }
    public int getIron()
    {
        return iron;
    }
    public void addWood(int wood)
    {
        this.wood += wood;
    }
    public void addStone(int stone)
    {
        this.stone += stone;
    }
    public void addGold(int gold)
    {
        this.gold += gold;
    }
    public void addIron(int iron)
    {
        this.iron += iron;
    }
    public void breakTree()
    {
        addWood(Random.Range(1, 5));
        Debug.Log(wood);
    }
    public void breakStone()
    {
        addStone(Random.Range(1, 5));
        Debug.Log(stone);
    }
}
