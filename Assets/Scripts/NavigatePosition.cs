using UnityEngine;
using System.Collections;

public class NavigatePosition : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator _anim;

	// Use this for initialization
	void Start ()
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
