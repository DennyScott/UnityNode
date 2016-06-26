using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

public class ClickMove : MonoBehaviour, IClickable
{

    public GameObject MyPlayer;
	
	public void OnClick (RaycastHit hit)
	{
	    var navigator = MyPlayer.GetComponent<Navigator>();
	    var netMove = MyPlayer.GetComponent<NetworkMove>();
        netMove.OnMove(hit.point);
        navigator.NavigateTo(hit.point);
	}
}
