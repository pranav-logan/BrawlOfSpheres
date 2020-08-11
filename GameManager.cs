﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MatchSettings matchSettings;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one GameManager in scene");

        }
        else
        {
            instance = this;
        }

    }



    #region Player Tracking
    private const string PlAYER_ID_PREFIX = "Player ";
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer (string _netID , Player _player)
    {
        string _playerID = PlAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer (string _playerID)
    {
        players.Remove(_playerID);

    }
    public static Player GetPlayer (string _playerID)
    {
        return players[_playerID];
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 200, 500));
        GUILayout.BeginVertical();
        foreach (string _playerId in players.Keys)
        {
            GUILayout.Label(_playerId + "  -  " + players[_playerId].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    #endregion

}
