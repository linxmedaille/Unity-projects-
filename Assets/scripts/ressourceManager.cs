using UnityEngine;

public class ressourceManager : MonoBehaviour
{
    private int wood = 0;
    private int stone = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int getWood()
    {
        return wood;
    }
    public int getStone()
    {
        return stone;
    }
    public void addWood(int wood)
    {
        this.wood += wood;
    }
    public void addStone(int stone)
    {
        this.stone += stone;
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
