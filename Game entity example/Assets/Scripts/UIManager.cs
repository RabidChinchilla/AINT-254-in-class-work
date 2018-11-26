using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager sInstance = null;
	public static UIManager Instance
	{
		get
		{
			if(sInstance == null)
			{
				GameObject singleton = new GameObject();

				sInstance = singleton.AddComponent<UIManager>();
			}
			return sInstance;
		}
	}

	int				m_HighScore;
	int				m_Score;
	public int		Score
	{
		get { return m_Score; }
		set
		{
			if(value == 0)
			{
				if(m_HighScore < m_Score)
					m_HighScore = m_Score;
			}
			m_Score = value;
		}
	}

	void Awake()
	{
		if(sInstance == null)
		{
			sInstance = this;
			name = "UIManager";
		}
		else
			enabled = false;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 70, 100), "score");
		GUI.Label(new Rect(80, 10, 100, 100), m_Score.ToString());

		GUI.Label(new Rect(10, 40, 70, 100), "high score");
		GUI.Label(new Rect(80, 40, 100, 100), m_HighScore.ToString());
	}
}
