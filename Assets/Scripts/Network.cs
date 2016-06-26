using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using System;

public class Network : MonoBehaviour
{
    private static SocketIOComponent _socket;

    public GameObject myPlayer;

    public Spawner spawner;

    // Use this for initialization
    private void Start()
    {
        _socket = GetComponent<SocketIOComponent>();
        _socket.On("open", OnConnected);
        _socket.On("spawn", OnSpawned);
        _socket.On("move", OnMove);
        _socket.On("disconnected", OnDisconnected);
        _socket.On("requestPosition", OnRequestPosition);
        _socket.On("updatePosition", OnUpdatePosition);
    }

    private void OnUpdatePosition(SocketIOEvent e)
    {
        Debug.Log("Updating Position " + e.data);

        var position = GetPlayerPosition(e.data);

        var id = e.data["id"].ToString();
        var player = spawner.FindPlayer(id);

        player.transform.position = position;
    }

    private void OnRequestPosition(SocketIOEvent e)
    {
        Debug.Log("Server is request our position");

        _socket.Emit("updatePosition", new JSONObject(VectorToJson(myPlayer.transform.position)));
    }

    private void OnDisconnected(SocketIOEvent e)
    {
        Debug.Log("we disconnected here" + e.data.ToString());
        var id = e.data["id"].ToString();
        spawner.Remove(id);
    }

    private void OnMove(SocketIOEvent e)
    {
        Debug.Log("Player is moving " + e.data);

        var position = GetPlayerPosition(e.data);

        var id = e.data["id"].ToString();
        var player = spawner.FindPlayer(id);


        var navigatePos = player.GetComponent<Navigator>();
        navigatePos.NavigateTo(position);
    }

    private void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
    }

    private void OnSpawned(SocketIOEvent e)
    {
        Debug.Log("Spawned" + e.data);
        var player = spawner.SpawnPlayer(e.data["id"].ToString());

        if (e.data["x"])
        {
            var movePosition = GetPlayerPosition(e.data);
            var navigatePos = player.GetComponent<Navigator>();
            navigatePos.NavigateTo(movePosition);
        }
    }

    private Vector3 GetPlayerPosition(JSONObject data)
    {
        var x = GetFloatFromJson(data, "x");
        var y = GetFloatFromJson(data, "y");
        return new Vector3(x, 0, y);
    }

    private float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }

    public static string VectorToJson(Vector3 vector)
    {
        return string.Format(@"{{""x"":""{0}"", ""y"": ""{1}""}}", vector.x, vector.z);
    }
}
