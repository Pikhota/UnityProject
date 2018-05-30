﻿using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    private Data data;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject super;
    [SerializeField] private GameObject simple;

    public void Awake()
    {
        data = ParseData.ParseXmlFileToObject("DataGames");
        AddOptionsToDropdown(dropdown);
        Initialization(dropdown.value);
    }

    private void AddOptionsToDropdown(Dropdown dropdownObject)
    {
        List<string> optionsGameList = new List<string>();
        foreach (Game game in data.Roomlist.Games)
        {
            optionsGameList.Add(game.Name);
        }
        dropdownObject.GetComponent<Dropdown>().AddOptions(optionsGameList);
    }

    private void Initialization(int index)
    {
        foreach (SuperGame superGame in data.Roomlist.Games[index].Supergame)
        {
            super.transform.GetChild(1).GetComponent<Text>().text = superGame.Price;
            super.transform.GetChild(2).GetComponentInChildren<Text>().text = superGame.AmountPlayers + " / " + superGame.MaxPlayers;
            float value = GetPercent(float.Parse(superGame.MaxPlayers), float.Parse(superGame.AmountPlayers));
            super.transform.GetChild(2).GetComponent<Slider>().value = value == 1 ? 0.1f : value / 100;
            Instantiate(
                super,
                new Vector3(Panel.transform.position.x, Panel.transform.position.y, Panel.transform.position.z),
                Quaternion.identity, Panel.transform);
        }
        foreach (Room room in data.Roomlist.Games[index].Rooms)
        {
            simple.transform.GetChild(1).GetComponent<Text>().text = room.Price;
            simple.transform.GetChild(2).GetComponent<Text>().text = room.Text;
            simple.transform.GetChild(3).GetComponentInChildren<Text>().text =
                Int32.Parse(room.MaxPlayers) > Int32.Parse(room.Players) ? "Join" : "Full";
            simple.transform.GetChild(3).GetComponent<Button>().interactable =
                Int32.Parse(room.MaxPlayers) > Int32.Parse(room.Players) ? true : false;
            Instantiate(
                simple,
                new Vector3(Panel.transform.position.x, Panel.transform.position.y, Panel.transform.position.z),
                Quaternion.identity, Panel.transform);
        }
    }

    private float GetPercent(float max, float current)
    {
        float percent = max / 100;
        return current / percent;
    }

    private void Clear(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (child != dropdown.transform)
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