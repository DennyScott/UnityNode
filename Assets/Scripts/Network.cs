using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour
{
    private static SocketIOComponent _socket;
    public GameObject playerPrefab;

    private Dictionary<string, GameObject> players;

	// Use this for initialization
    private void Start ()
	{
	    _socket = GetComponent<SocketIOComponent>();
        _socket.On("open", OnConnected);
	    _socket.On("spawn", OnSpawned);
        _socket.On("move", OnMove);
        _socket.On("disconnected", OnDisconnected);
        players = new Dictionary<string, GameObject>();
	}

    private void OnDisconnected(SocketIOEvent e)
    {
        Debug.Log("we disconnected here" + e.data.ToString());
        var id = e.data["id"].ToString();
        var player = players[id];
        Destroy(player);
        players.Remove(id);
    }

    private void OnMove(SocketIOEvent e)
    {
        Debug.Log("Player is moving " + e.data);

        var x = GetFloatFromJson(e.data, "x");
        var y = GetFloatFromJson( e.data, "y");
        var position = new Vector3(x, 0, y);

        var id = e.data["id"].ToString();
        var player = players[id];


        var navigatePos = player.GetComponent<NavigatePosition>();
        navigatePos.NavigateTo(position);
    }

    private void OnRegistered(SocketIOEvent e)
    {
        
    }

    private void OnConnected (SocketIOEvent e)
	{
	    Debug.Log("connected");
	}

    private void OnSpawned(SocketIOEvent e)
    {
        Debug.Log("Spawned" + e.data);
        var player = Instantiate(playerPrefab);

        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count " + players.Count);
    }

    private float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }
}
