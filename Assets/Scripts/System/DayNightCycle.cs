using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] 
    private Gradient lightColor;
    [SerializeField] 
    private Light2D globalLight2D;
    private TimeManager clock;

    void Start()
    {
        clock = GameObject.Find("WorldClock").GetComponent<TimeManager>();
    }
    
    void Update()
    {
        Debug.Log("The current hour is ... " + clock.TotalGameHour);
        globalLight2D.color = lightColor.Evaluate((float)clock.TotalGameHour/24);   
    }
}