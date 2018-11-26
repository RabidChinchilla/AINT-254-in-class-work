using UnityEngine;
using System.Collections;

public class Bullet : GameEntity
{
	Vector3		m_Translation;

	public override void Initialise()
	{
		base.Initialise();
		m_Translation.Set(0, 0.2f, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();

		if(transform.position.y > 15.0f)
			TriggerDeath();
	}

	protected override void Move()
	{
		transform.Translate(m_Translation);
	}

	public void OnTriggerEnter(Collider other)
	{
		GameEntity		OtherEntity = other.GetComponent<GameEntity>();

		if (OtherEntity == null)
			return;

		if (OtherEntity is Baddie)
		{
			UIManager.Instance.Score += 500;
			OtherEntity.ReceiveDamage(0.5f);
			TriggerDeath();
		}
	}
}
