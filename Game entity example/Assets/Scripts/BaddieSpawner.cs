using UnityEngine;
using System.Collections;

public class BaddieSpawner : GameEntity
{
	private static BaddieSpawner	sInstance;
	public static BaddieSpawner		Instance { get { return sInstance; } }

	static float					sHowOftenToCreateBaddies = 2.0f;

	public GameObject		m_BaddiePrefab;

	float					m_BaddieCreationCountdown;
	Vector3					m_NewSpawnLocation;
	float					m_CreationTime;

	void Awake()
	{
		sInstance = this;
	}

	public override void Initialise()
	{
		base.Initialise();
		m_BaddieCreationCountdown = sHowOftenToCreateBaddies;
		m_NewSpawnLocation = transform.position;
		m_CreationTime = 0;
	}

	// Update is called once per frame
	void Update()
	{
		m_BaddieCreationCountdown -= Time.deltaTime;
		m_CreationTime += Time.deltaTime;;

		if(m_BaddieCreationCountdown <= 0)
		{
			if (m_CreationTime < 5.0f)
				m_BaddieCreationCountdown = sHowOftenToCreateBaddies;
			else
				m_BaddieCreationCountdown = sHowOftenToCreateBaddies - ((m_CreationTime - 5) / 5.0f) * 0.2f;

			GameObject		NewBaddie = Instantiate(m_BaddiePrefab) as GameObject;
			Bullet			NewBulletEntity = NewBaddie.GetComponent<Bullet>();

			m_NewSpawnLocation.x = Random.Range(-10.0f, 10.0f);
			NewBaddie.transform.position = m_NewSpawnLocation;
		}
	}

	internal void Reset()
	{
		m_CreationTime = 0;
	}
}
