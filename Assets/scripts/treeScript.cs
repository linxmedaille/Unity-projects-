using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private Color[][] startColor; 
    private Renderer[] rends;    
    private bool isMouseOver = false; 
    private float holdTime = 0f;  
    public float requiredHoldTime = 2f; 
    private bool treeBroken = false;
    private ressourceManager ressourceManager;
    void Start()
    {
        ressourceManager = FindAnyObjectByType<ressourceManager>();
        rends = GetComponentsInChildren<Renderer>();
        startColor = new Color[rends.Length][]; 
        for (int i = 0; i < rends.Length; i++)
        {
            startColor[i] = new Color[rends[i].materials.Length]; 
            for (int j = 0; j < rends[i].materials.Length; j++)
            {
                startColor[i][j] = rends[i].materials[j].color;  // Store the original color of each material
            }
        }
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;

            if (holdTime >= requiredHoldTime && !treeBroken)
            {
                BreakTree();
            }
        }
        else
        {
            // Reset the timer when the mouse is not held down or not over the tree
            holdTime = 0f;
        }
    }

    // When the mouse enters the tree collider, change color
    void OnMouseEnter()
    {
        if (rends != null)
        {
            // Change color for all materials in all renderers
            foreach (var rend in rends)
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    // Change each material's color to yellow
                    rend.materials[i].color = Color.yellow;
                }
            }
        }
        isMouseOver = true;  // Mark that the mouse is over the tree
    }

    void OnMouseExit()
    {
        if (rends != null)
        {
            // Revert color for all materials in all renderers
            for (int i = 0; i < rends.Length; i++)
            {
                for (int j = 0; j < rends[i].materials.Length; j++)
                {
                    rends[i].materials[j].color = startColor[i][j];
                }
            }
        }
        isMouseOver = false; 
        holdTime = 0f;  
    }


    void BreakTree()
    {
        if (!treeBroken)
        {
            treeBroken = true;
            Destroy(gameObject);
            ressourceManager.breakTree();
        }
    }

}