using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public bool playerDead = false;
    public Image CurrentHP;
    public Image CurrentAmmo;
    public GameObject ESCMenu, HUD;
    private bool ESCActive = false, HUDActive = true;
    public Text AmmoText;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESCActive = !ESCActive;
            HUDActive = !HUDActive;
            ESCMenu.SetActive(ESCActive);
            HUD.SetActive(HUDActive);
        }
        if (playerDead)
            HUD.SetActive(false);
        Cursor.visible = ESCActive;
        Cursor.lockState = CursorLockMode.Locked;
        if (ESCActive)
            Cursor.lockState = CursorLockMode.None;
    }

    public void setHPPercent(float hp)
    {
        CurrentHP.fillAmount = hp;
    }

    public void setAmmoAmt(int curr, int max)
    {
        CurrentAmmo.fillAmount = curr / (float)max;
        AmmoText.text = $"Ammo\n{curr}/{max}";
    }

    public void Quit()
    {
        Application.Quit();
    }
}
