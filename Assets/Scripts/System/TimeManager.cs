using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class TimeManager : MonoBehaviour 
{
    // Start is called before the first frame update

    //boolean to be set true in scenes aside from main menu
    private bool isTimeRunning;

    //variables for real world tracking of time spent in game
    private int realSec, realMin, realHr, realDay;
    //variable for time since start of game
    private int cRealSec, cRealMin, cRealHr, cRealDay;
    //variable for in-game tracking of time in game
    private int gameMin, gameHr, gameDay;
    //variables storing current game time since last level loaded
    private int cGameMin, cGameHr, cGameDay;
    //Holds total seconds within game thus far
    private int totalGameSec, totalRealSec;
    private string[] weekDays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    
    void Start() 
    {
        isTimeRunning = true;
        totalRealSec = 0;
        totalGameSec = 0;

        //will store and retrieve these values from files
        cRealSec = 0;
        cRealMin = 0;
        cRealHr = 0;
        cRealDay = 0;

        realSec = 0;
        realMin = 0;
        realHr = 0;
        realDay = 0;


        gameMin = 0;
        gameHr = 0;
        gameDay = 0;

        cGameMin = 0;
        cGameHr = 0;
        cGameDay = 0;
        
        //getTime();
    }

    // Update is called once per frame

    void Update() 
    {   
        if(isTimeRunning == true) 
        {
            UpdateRealWorldTime();
            UpdateInGameTime();
            //print(TotalRealSec);
        }  
    }

    //increments how much time has passed in the video game since launch
    //1 game day = 24 minutes
    void UpdateInGameTime() 
    {
        
        //totalRealSec = (int)Time.realtimeSinceStartup;
        //+realSec+(realMin*60)+(realHr*60*60)+(realDay*60*60*24);
        totalGameSec = (int)Time.timeSinceLevelLoad;
        //+ gameMin + (gameHr*60) +(gameDay*60*60);

        cGameMin = totalGameSec % 60;
        cGameHr = totalGameSec / 60 % 24;
        cGameDay = totalGameSec / 60 / 24 % 7;

        //print("REal Hour: "+cRealHr+", REal Minutes: "+cRealMin+"Real sec: "+cRealSec);
     }

    void TotalInGameTime() 
    {
        print("Game Hour: " + (gameHr + cGameHr) + ", Game Minutes: " + (gameMin + cGameMin));
    }

    void UpdateRealWorldTime() 
    {
        totalRealSec = (int)Time.realtimeSinceStartup;

        cRealSec = totalRealSec % 60;
        cRealMin = totalRealSec / 60 % 60;
        cRealHr = totalRealSec / 60 / 60 % 60;
        cRealDay = totalRealSec / 60 / 60 / 24;
        //print("Real Hour: " + cRealHr + ", REal Minutes: " + cRealMin + "Real sec: " + cRealSec);
    }

    void TotalRealWorldTime() 
    {
        print("Real Hour: " + (realHr+cRealHr) + ", REal Minutes: " + (realMin + cRealMin) + "Real sec: " + (realSec+cRealSec));
    }

    public int TotalRealSec {
        get { return (realSec+cRealSec); }
    }

    public int TotalRealMin {
        get { return (realMin+cRealMin); }
    }

    public int TotalRealHour {
        get { return (realHr+cRealHr); }
    }

    public int TotalRealDay {
        get { return (realDay+cRealDay); }
    }

    public int TotalGameMin {
        get { return (gameMin+cGameMin); }
    }

    public int TotalGameHour {
        get { return (gameHr+cGameHr); }
    }

    public int TotalGameDay {
        get { return (gameDay+cGameDay); }
    }

    public string GameWeekDay {
        get { return weekDays[(gameDay - 1) % 7]; }
    }
}