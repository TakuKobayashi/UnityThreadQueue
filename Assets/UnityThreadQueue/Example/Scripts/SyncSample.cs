using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SyncSample : MonoBehaviour {
	[SerializeField] private Text counterText;

	void Start () {
		// first task
		UnityThreadQueue.Instance.Enqueue (() => {
			for(int i = 0;i < 10;++i){
				Thread.Sleep(1000);
			}
		});
		StartCoroutine (SyncCorutine ());
	}

	private IEnumerator SyncCorutine(){
		while (UnityThreadQueue.Instance.ExistQueueEvent) {
			yield return null;
		}
		counterText.text = "complete";
	}
}
