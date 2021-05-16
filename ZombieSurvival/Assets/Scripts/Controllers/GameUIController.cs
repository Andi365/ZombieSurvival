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
    public Text RespawnText;
    private float RespawnTime = 0;
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
        RespawnTime -= Time.deltaTime;
        if (RespawnTime >= 1)
            RespawnText.text = $"Respawn\n{(int)RespawnTime}";
        else
            RespawnText.text = "";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESCActive = !ESCActive;
            HUDActive = !HUDActive;
            updateGUIS();
        }
        if (playerDead)
            HUD.SetActive(false);
        else
            updateGUIS();
        Cursor.visible = ESCActive;
        Cursor.lockState = CursorLockMode.Locked;
        if (ESCActive)
            Cursor.lockState = CursorLockMode.None;
    }

    private void updateGUIS()
    {
        ESCMenu.SetActive(ESCActive);
        HUD.SetActive(HUDActive);
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

    public void ShowRespawnTimer() 
    {
        RespawnTime = 10.999f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
