using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public TMP_Text woodText;
    public TMP_Text stoneText;

    public ressourceManager ressourceManager;

    void Update()
    {
        woodText.text = "Wood: " + ressourceManager.getWood();
        stoneText.text = "Stone: " + ressourceManager.getStone();
    }
}