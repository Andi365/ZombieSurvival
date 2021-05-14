using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Image CurrentHP;
    public static GameUIController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("only one instance should exist");
            Destroy(this);
        }
    }

    public void setHPPercent(float hp) 
    {
        CurrentHP.fillAmount = hp;
    }
}
