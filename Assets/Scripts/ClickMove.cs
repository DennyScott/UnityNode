using UnityEngine;
using System.Collections;

public class ClickMove : MonoBehaviour
{

    public GameObject player;
	
	public void OnClick (Vector3 position)
	{
	    var navPosition = player.GetComponent<NavigatePosition>();
	    var netMove = player.GetComponent<NetworkMove>();
        netMove.OnMove(position);
        navPosition.NavigateTo(position);
	}
}
