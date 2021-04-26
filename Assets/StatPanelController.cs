using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatPanelController : MonoBehaviour
{

    public TMP_Text statName;
    public TMP_Text value;
    public void OnValidate()
    {
        //UpdateInfo();
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        statName = texts[0];
        value = texts[1];
    }

    
    
}
