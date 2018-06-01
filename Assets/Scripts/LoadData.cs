using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    private Data data;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Transform super;
    [SerializeField] private Transform simple;

    public void Awake()
    {
        //get data from the DataGames.xml
        data = ParseData.ParseXmlFileToObject("DataGames");
        //fill options in the dropdown 
        List<string> optionsGameList = new List<string>();
        foreach(RoomList roomlist in data.Roomlist)
        {
            optionsGameList.Add(roomlist.Type);
        }
        dropdown.GetComponent<Dropdown>().AddOptions(optionsGameList);
        //initiaze other objects
        Initialization(dropdown.value);
    }

    private void Initialization(int index)
    {
        
        foreach (Game game in data.Roomlist[index].Games)
        {
            foreach(SuperGame superGame in game.Supergame)
            {
                super.GetChild(1).GetComponent<TextMeshProUGUI>().text = superGame.Price.ToString();
                super.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = superGame.AmountPlayers + " / " + superGame.MaxPlayers;
                float value = GetPercent(superGame.MaxPlayers, superGame.AmountPlayers);
                super.GetChild(2).GetComponent<Slider>().value = value == 1 ? 0.1f : value / 100;
                Instantiate(
                    super,
                    new Vector3(Panel.transform.position.x, Panel.transform.position.y, Panel.transform.position.z),
                    Quaternion.identity, Panel.transform);
            }

            foreach (Room room in game.Room.OrderBy(room => room.Text))
            {
                simple.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.Price.ToString();
                simple.GetChild(2).GetComponent<TextMeshProUGUI>().text = room.Text;
                simple.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text =
                    room.MaxPlayers > room.Players ? "Join" : "Full";
                simple.GetChild(3).GetComponent<Button>().interactable =
                    room.MaxPlayers > room.Players;
                Instantiate(
                    simple,
                    new Vector3(Panel.transform.position.x, Panel.transform.position.y, Panel.transform.position.z),
                    Quaternion.identity, Panel.transform);
            }
        }
    }

    private float GetPercent(float max, float current)
    {
        return current / (max / 100);
    }

    private void Clear(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (!child.GetComponent<Dropdown>())
                Destroy(child.gameObject);
        }
    }

    private void DropdownValueChanged(Dropdown change)
    {
        Clear(Panel);
        Initialization(change.value);
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable Change " + dropdown.value);
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable Change " + dropdown.value);
        dropdown.onValueChanged.RemoveAllListeners();
    }
}
