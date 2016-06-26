using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class Spawner : MonoBehaviour
{
    public GameObject MyPlayer;
    public GameObject playerPrefab;
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public GameObject SpawnPlayer(string id)
    {
        var player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.GetComponent<ClickFollow>().MyPlayerFollower = MyPlayer.GetComponent<Follower>();
        players.Add(id, player);
        return player;
    }

    public GameObject FindPlayer(string id)
    {
        return players[id];
    }

    public void Remove(string id)
    {
        var player = FindPlayer(id);
        Destroy(player);
        players.Remove(id);
    }
}
