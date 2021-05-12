using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using GameClient.UI;
using Data;

namespace GameClient.Controllers
{
    class UIController : MonoBehaviour
    {
        public static UIController instance;
        public GameObject StartMenu, LobbyMenu;
        public InputField UsernameField;
        public InputField PortField;
        public InputField IPField;
        public Text UsernameError, PortError, IPError;
        public LobbyListView listView;
        private Dictionary<byte, (string, bool)> playersReady;

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

        private void Start() {
            playersReady = new Dictionary<byte, (string, bool)>();
        }

        public void ConnectToServer()
        {
            if (!validate())
                return;
            StartMenu.SetActive(false);
            LobbyMenu.SetActive(true);
            UsernameField.interactable = false;
            PlayerController.PlayerName = UsernameField.text;
            Client.instance.ConnectToServer(IPField.text, PortField.text);
        }

        public void onConnect()
        {
            GameController.instance.outgoingQueue.Enqueue(new PlayerReady(PlayerController.MyID, false, PlayerController.PlayerName));
        }

        Regex iprx = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
        private bool validate() 
        {
            UsernameError.text = "";
            PortError.text = "";
            IPError.text = "";

            bool val = true;
            if (String.IsNullOrWhiteSpace(UsernameField.text)) 
            {
                UsernameError.text = "Username cannot be empty";
                val = false;
            }
            if (String.IsNullOrWhiteSpace(PortField.text)) 
            {
                PortError.text = "Host Port cannot be empty";
                val = false;
            }
            if (!iprx.IsMatch(IPField.text)) {
                IPError.text = "Host IP is not a valid IP";
                val = false;
            }
            if (String.IsNullOrWhiteSpace(IPField.text))
            {
                IPError.text = "Host IP cannot be empty";
                val = false;
            }



            return val;
        }

        public void setPlayerReadiness(PlayerReady player) 
        {
            string text = $"{player.name.Trim('\0')} is {(player.ready ? "" : "not ")}ready";
            if (playersReady.ContainsKey(player.ID)) 
            {
                playersReady[player.ID] = (player.name, player.ready);
            } else 
            {
                playersReady.Add(player.ID, (player.name, player.ready));
            }
            listView.SetItemText(player.ID, text);
        }

        public void RemovePlayer(byte ID) 
        {
            listView.RemovePlayer(ID);
            if (playersReady.ContainsKey(ID))
                playersReady.Remove(ID);
        }

        public void toggleReady(Text readyButtonText) 
        {
            if (readyButtonText.text.Equals("Ready")) 
            {
                readyButtonText.text = "Unready";
                GameController.instance.outgoingQueue.Enqueue(new PlayerReady(PlayerController.MyID, true, PlayerController.PlayerName));
            } else
            {
                readyButtonText.text = "Ready";
                GameController.instance.outgoingQueue.Enqueue(new PlayerReady(PlayerController.MyID, false, PlayerController.PlayerName));
            }
        }

        private bool allReady() 
        {
            foreach ((string, bool) playerReady in playersReady.Values)
                if (!playerReady.Item2)
                    return false;
            return true;
        }

        public void StartGame() 
        {
            if (allReady())
                GameController.instance.outgoingQueue.Enqueue(new StartServer());
        }
    }
}