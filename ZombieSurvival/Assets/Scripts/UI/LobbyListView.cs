using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameClient.UI
{
    class LobbyListView : MonoBehaviour
    {
        public VerticalLayoutGroup verticalLayoutGroup;
        private Dictionary<byte, GameObject> ListItems;
        // Start is called before the first frame update
        void Start()
        {
            ListItems = new Dictionary<byte, GameObject>();
        }

        public void AddListItem(byte ID, string text)
        {
            RectTransform parent = verticalLayoutGroup.GetComponent<RectTransform>();
            GameObject item = new GameObject(string.Format($"{ID}"));
            Text itemText = item.AddComponent<Text>();
            RectTransform itemTransfrom = itemText.GetComponent<RectTransform>();

            itemTransfrom.sizeDelta = new Vector2(100, 40);

            Font ArialFont = (Font)Resources.GetBuiltinResource<Font>("Arial.ttf");
            itemText.fontSize = 30;
            itemText.font = ArialFont;
            itemText.text = text;
            itemText.color = Color.black;
            itemTransfrom.SetParent(parent);
            ListItems.Add(ID, item);
        }

        public void SetItemText(byte ID, string text)
        {
            if (ListItems.ContainsKey(ID))
                ListItems[ID].GetComponent<Text>().text = text;
            else
                AddListItem(ID, text);
        }
    }
}