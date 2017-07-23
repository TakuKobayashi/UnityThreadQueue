using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SyncSample : MonoBehaviour {
	[SerializeField] private Text counterText;
	private bool complete = false;

	void Start () {
		// first task
		UnityThreadQueue.Instance.Enqueue (() => {
			for(int i = 0;i < 10;++i){
				Thread.Sleep(1000);
			}
			complete = true;
		});
		StartCoroutine (SyncCorutine ());
	}

	private IEnumerator SyncCorutine(){
		while (UnityThreadQueue.Instance.ExistQueueEvent && !complete) {
			yield return null;
		}
		counterText.text = "complete";
	}
}
