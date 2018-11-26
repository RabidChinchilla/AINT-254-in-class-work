using UnityEngine;
using System.Collections;

public static class Message
{
	//delegate for target aquired when user touches anything in the game world
	public delegate void TargetAquiredHandler(RaycastHit hit); 
	public static event TargetAquiredHandler onTargetAquired;
	
	//broadcast message when a target is aquired
	public static void BroadcastTargetAquired(RaycastHit hit)
	{
		if(onTargetAquired == null) return;
		onTargetAquired(hit);
	}



	// message event for updating the rest of the system scores --------------------------
	public delegate void UpdatedScoreHandler(); 
	public static event UpdatedScoreHandler onUpdatedScore;
	
	public static void BroadcastUpdatedScore()
	{
		if(onUpdatedScore == null) return;
		onUpdatedScore();
	}

	// message event for notifying change to present count --------------------------
	public delegate void PresentCountUpdated(); 
	public static event PresentCountUpdated onPresentCountUpdated;
	
	public static void BroadcastPresentCountUpdated()
	{
		if(onPresentCountUpdated == null) return;
		onPresentCountUpdated();
	}

	// message event for adding to the score --------------------------
	public delegate void GameStateChangeHandler(GameSystem.GameState _gameState ); 
	public static event GameStateChangeHandler onGameStateChange;
	
	public static void BroadcastGameStateChange(GameSystem.GameState _gameState)
	{
		if(onGameStateChange == null) return;
		onGameStateChange(_gameState);
	}

	// message event for resetting the game --------------------------
	public delegate void ResetGameHandler( ); 
	public static event ResetGameHandler onResetGame;
	
	public static void BroadcastResetGame()
	{
		if(onResetGame == null) return;
		onResetGame();
	}

	// message event for changing meter --------------------------
	public delegate void UpdateMeterHandler(float _meterValue ); 
	public static event UpdateMeterHandler onUpdateMeter;
	
	public static void BroadcastUpdateMeter(float _meterValue)
	{
		if(onUpdateMeter == null) return;
		onUpdateMeter(_meterValue);
	}

	// message event for toggle debug message --------------------------
	public delegate void DebugToggleHandler(bool _debugToggle); 
	public static event DebugToggleHandler onDebugToggle;
	
	public static void BroadcastDebugToggle(bool _debugToggle)
	{
		if(onDebugToggle == null) return;
		onDebugToggle(_debugToggle);
	}

	// message event for toggle debug message --------------------------
	public delegate void ScreenMessageHandler(string _msg, string _hexcol, ScoringLabel.Size _size, Vector3 _trans); 
	public static event ScreenMessageHandler onScreenMessage;
	
	public static void BroadcastScreenMessage(string _msg, string _hexcol, ScoringLabel.Size _size, Vector3 _trans)
	{
		if(onScreenMessage == null) return;
		onScreenMessage(_msg, _hexcol, _size, _trans);
	}
	
	public delegate void FireHandler(); 
	public static event FireHandler onFire;
	
	public static void BroadcastFire()
	{
		if(onFire == null) 
			return;
			
		onFire();
	}
	
	// message event for new score --------------------------
	public delegate void NewPlayerScoreHandler(int _playerScore ); 
	public static event NewPlayerScoreHandler onNewPlayerScore;
	
	public static void BroadcastNewPlayerScore(int _playerScore)
	{
		if(onNewPlayerScore == null) 
			return;
			
		onNewPlayerScore(_playerScore);
	}
	
	// message event for total hits --------------------------
	public delegate void NewTotalHitsHandler(int _totalHits ); 
	public static event NewTotalHitsHandler onNewTotalHits;
	
	public static void BroadcastNewTotalHits(int _totalHits)
	{
		if(onNewTotalHits == null) 
			return;
		
		onNewTotalHits(_totalHits);
	}
	
	// message event for distance --------------------------
	public delegate void NewDistanceHandler(int _distance ); 
	public static event NewDistanceHandler onNewDistance;
	
	public static void BroadcastNewDistance(int _distance)
	{
		if(onNewDistance == null) 
			return;
		
		onNewDistance(_distance);
	}
	
	// message event for accuracy --------------------------
	public delegate void NewAccuracyHandler(float _accuracyValue ); 
	public static event NewAccuracyHandler onNewAccuracy;
	
	public static void BroadcastNewAccuracy(float _accuracyValue)
	{
		if(onNewAccuracy == null) 
			return;
			
		onNewAccuracy(_accuracyValue);
	}
}
