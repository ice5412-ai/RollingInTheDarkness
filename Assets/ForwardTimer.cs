using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForwardTimer : MonoBehaviour
{
    Text _timer;
    public bool IsTimerUpdate;
    void Start()
    {
        IsTimerUpdate = true;
        _timer = GetComponent<Text>();
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void Update()
    {
        if (IsTimerUpdate)
        {
            DisplayTime(Time.timeSinceLevelLoad);
        }
        else
        {
            _timer.color = Color.white;
        }
    }
}
