using UnityEngine;
using System.Collections;

public class FiringMechanism : MonoBehaviour 
{
	public Transform missile;
	private float m_firingSpeed = 4.5f;
	private bool m_canFire = true;
	private RaycastHit m_hit;
	private Vector3 m_currentVector;
	private float m_interpolationValue = 0.0f;
	
	void OnEnable()
	{
		Message.onTargetAquired += OnTargetAquired;
	}
	
	void OnDisable()
	{
		Message.onTargetAquired -= OnTargetAquired;
	}
	
	private void OnTargetAquired(RaycastHit hit)
	{
		StopCoroutine("Fire");
		//m_canFire = false;
		m_hit = hit;
		StartCoroutine("Fire");
	}

	private IEnumerator Fire()
	{
		if(GameSystem.instance.CurrentGameState == GameSystem.GameState.PLAY)
		{
			//consume present
			//GameSystem.instance.RemovePresent(1);
			//Vector3 randomVector = new Vector3(Random.Range(-0.4f, 0.4f), 0.5f, 1.0f);
			Vector3 randomVector = m_hit.point;
			if(randomVector.z < 0.1f)
				randomVector.z = 0.1f;
			
			if(randomVector.x > 0.4f)
				randomVector.x = 0.4f;
			
			if(randomVector.x < -0.4f)
				randomVector.x = -0.4f;
			
			randomVector.y = 4.7f;
			randomVector.z = 1.0f;

			m_interpolationValue = 0.0f;

			while(m_currentVector != randomVector)
			{
				m_interpolationValue += m_firingSpeed * Time.deltaTime;
				//transform.forward = randomVector.normalized;
				m_currentVector = Vector3.Slerp(m_currentVector, randomVector, m_interpolationValue);
				transform.LookAt(m_currentVector);
				yield return null;
			}
			//Debug.Log("fire!!!");
			if(SoundManager.Instance.GetFXVolume() > 0)
				SoundManager.Instance.PlaySoundFXOnce("cannon-shot", SoundManager.Instance.GetFXVolume());
			
			Message.BroadcastFire();
			GameSystem.instance.ChangeMeter(MeterController.METER_FIRE_PRESENT);
			Transform instanceMissile = Instantiate(missile, transform.position + transform.forward * 1.0f, transform.rotation) as Transform;
			instanceMissile.transform.GetComponent<MissileBehaviour>().Launch(m_hit);
			GameSystem.instance.AddShot();
		}

		m_canFire = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
