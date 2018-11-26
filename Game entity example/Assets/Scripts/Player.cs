using UnityEngine;
using System.Collections;

public class Player : GameEntity
{
	public GameObject		m_BulletPrefab;
	public Transform		m_BulletLaunchLocation;

	Vector3		m_Translation;
	float		m_MaxHorizontalVelocity;

	public override void Initialise()
	{
		base.Initialise();

		m_Translation.Set(0, 0, 0);
		m_MaxHorizontalVelocity = 0.2f;

		UIManager.Instance.Score = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UIManager.Instance.Score += 1;
		Move();
	}

	protected override void Move()
	{
		if(Input.GetKey(KeyCode.A))
			m_Translation.x = -m_MaxHorizontalVelocity;
		else if(Input.GetKey(KeyCode.D))
			m_Translation.x = m_MaxHorizontalVelocity;
		else
			m_Translation.x = 0;

		if (Input.GetKeyDown(KeyCode.Return))
		{
			GameObject		NewBullet = Instantiate(m_BulletPrefab) as GameObject;
			Bullet			NewBulletEntity = NewBullet.GetComponent<Bullet>();

			NewBullet.transform.position = m_BulletLaunchLocation.position;
		}

		transform.Translate(m_Translation);
	}

	public override void TriggerDeath()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		GameEntity		OtherEntity = other.GetComponent<GameEntity>();

		if (OtherEntity == null)
			return;

		if (OtherEntity is Baddie)
		{
			UIManager.Instance.Score = 0;
			BaddieSpawner.Instance.Reset();

			OtherEntity.TriggerDeath();
		}
	}
}
