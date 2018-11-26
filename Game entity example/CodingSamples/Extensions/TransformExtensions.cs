using UnityEngine;
using System.Collections;
using System;

namespace ISS
{
	public static class TransformExtensions 
	{
		/// <summary>
		/// counts down to zero from any number and triggers a method when the countdown is finised.
		/// Add this to the code:
		/// StartCoroutine(transform.CountDownFrom(5.0f, () => { OnCompleteCountdown(); }));
		/// and implement the OnCompleteCountDown() method 
		/// </summary>
		public static IEnumerator CountDownFrom(this Transform theTransform, float timeToCountDownFrom, Action onComplete)
		{
			float tempTimer = timeToCountDownFrom;
			
			while(tempTimer > 0)
			{
				tempTimer -= Time.deltaTime;
				yield return null;
			}
			
			if(onComplete != null)
				onComplete();
		}
	}
}
