using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
	private int m_hitCounter;
	public LayerMask layerMask;
	
	// Use this for initialization
	void Start () 
	{
		m_hitCounter = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckTouch();

		CheckDebugToggle();

		CheckBack();
	}

	private void CheckDebugToggle()
	{
		if (Application.isEditor)
		{
			if(Input.GetKeyUp(KeyCode.Space))
			{
				GameSystem.instance.ToggleDebugMode();
			}
		}
	}

	private void CheckBack()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Message.BroadcastGameStateChange(GameSystem.GameState.PAUSE);	
		}
	}
	
	public void CheckTouch()
	{
		if(Application.isEditor)
		{
			if(Input.GetMouseButtonDown(0))
			{
				CheckCollision(Input.mousePosition);
			}
		}
		else
		{
			if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
			{
				CheckCollision(Input.GetTouch(0).position);
			}
		}
		
	}
	
	private void CheckCollision(Vector3 position)
	{
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(camera.ScreenPointToRay(position),  out hit, 300, layerMask))
		{
			if(hit.collider.tag == "elf")
			{
				hit.transform.GetComponent<ElfBehaviour>().Touched();
			}
			else
			{
				Message.BroadcastTargetAquired(hit);
			}
		}
	}
	
}
