using UnityEngine;
using UnityEngine.InputSystem;

public class Buildmode : MonoBehaviour
{
    public GameObject objectToPlace;
    public LayerMask placementMask;
    public float rotateSpeed = 90f;
    private GameObject previewObject;
    private bool buildMode = false;
    public Material previewMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            ToggleBuildMode();

        }
        UpdatePreview();
        if (buildMode && Input.GetButtonDown("Fire1"))
        {
            PlaceObject();
        }
        if (Input.GetKeyDown(KeyCode.R) && previewObject != null)
        {
            previewObject.transform.Rotate(Vector3.up, 90f);
        }

    }
    void ToggleBuildMode()
    {
        buildMode = !buildMode;

        if (buildMode)
        {
            previewObject = Instantiate(objectToPlace);
            ApplyPreviewMaterial(previewObject);

        }
        else
        {
            if (previewObject != null) Destroy(previewObject);
        }
    }
    void UpdatePreview()
    {
            if (!buildMode || previewObject == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, placementMask))
            {
                Vector3 worldPos = hit.point;
                previewObject.transform.position = worldPos;
        }

    }
    void PlaceObject()
    {
        if (previewObject == null) return;
        Instantiate(objectToPlace, previewObject.transform.position, previewObject.transform.rotation);
    }
    void ApplyPreviewMaterial(GameObject obj)
    {
        foreach (Renderer rend in obj.GetComponentsInChildren<Renderer>())
        {
            rend.material = previewMaterial;
        }
    }

}
