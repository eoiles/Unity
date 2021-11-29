using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager Instance;

    public int combo;
    
    public void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
