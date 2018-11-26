using UnityEngine;
using System.Collections;

public class Baddie : GameEntity
{
	Vector3		m_Translation;

	public override void Initialise()
	{
		base.Initialise();

		m_Translation.Set(0, -0.05f, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();

		if (transform.position.y < -1.0f)
			TriggerDeath();
	}

	protected override void Move()
	{
		transform.Translate(m_Translation);
	}
}
