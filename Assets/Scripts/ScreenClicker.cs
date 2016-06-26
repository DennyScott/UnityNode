using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

public class ScreenClicker : MonoBehaviour {

	// Use this for initialization
    private void Start () {
	
	}
	
	// Update is called once per frame
    private void Update () {
	    if (Input.GetButtonDown("Fire2"))
	    {
	        Clipped();
	    }	
	}

    private void Clipped()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            var clickable = hit.collider.GetComponent<IClickable>();
            clickable.OnClick(hit);
        }
    }
}
