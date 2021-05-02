using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace custom
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

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
        }
    }
}
