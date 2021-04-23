using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldPanelController : MonoBehaviour
{

    public TMP_Text gold;

    public void OnValidate()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();

        gold = text;
    }
}
