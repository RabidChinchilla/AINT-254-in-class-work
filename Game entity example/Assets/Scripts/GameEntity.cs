using UnityEngine;
using System.Collections;

public class GameEntity : MonoBehaviour
{
	protected float		m_Health;

	// Use this for initialization
	void Start ()
	{
		Initialise();
	}

	public virtual void Initialise()
	{
		m_Health = 1.0f;
	}

	protected virtual void Move()
	{
	}

	public virtual void ReceiveDamage(float DamageValue)
	{
		m_Health -= DamageValue;
		if(m_Health <= 0)
			TriggerDeath();
	}

	public virtual void TriggerDeath()
	{
		GameObject.Destroy(gameObject);
	}
}
