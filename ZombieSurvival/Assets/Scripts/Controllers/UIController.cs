using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace GameClient.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public GameObject startMenu;
        public InputField usernameField;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("only one instance should exist");
                Destroy(this);
            }
        }

        public void ConnectToServer()
        {
            startMenu.SetActive(false);
            usernameField.interactable = false;
            Client.instance.ConnectToServer();
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}