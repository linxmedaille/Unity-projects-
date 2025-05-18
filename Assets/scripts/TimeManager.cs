using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject directionalLight;
    private int days;
    private int minutes;
    private int hours;
    private float timeSeconds = 0;
    private float daySpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSeconds += Time.deltaTime;
        if (timeSeconds >= 1f)
        {
            Debug.Log("1s");
            minutes++;
            timeSeconds = 0;
            
        }
        directionalLight.transform.Rotate(Vector3.right * daySpeed * Time.deltaTime);
    }

    private int getMinutes () {
        return minutes;
    }
    private int getHours () {
        return hours; 
    }
    private int getDays()
    {
        return days;
    }
    public void changeSkybox(int time)
    {
        if (time == 6)
        {

        }
        else if (time == 9)
        {

        }
        else if (time == 18)
        {

        }
        else if (time == 21)
        {

        }
    }
}
