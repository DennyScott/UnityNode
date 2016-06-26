using UnityEngine;
using System.Collections;

public class Navigator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator _anim;

	// Use this for initialization
	void Awake ()
	{
	    agent = GetComponent<NavMeshAgent>();
	    _anim = GetComponent<Animator>();
	}
	
	public void NavigateTo (Vector3 position)
	{
        Debug.Log(agent);
	    agent.SetDestination(position);
	}

    private void Update()
    {
        _anim.SetFloat("Distance", agent.remainingDistance);
    }
}
