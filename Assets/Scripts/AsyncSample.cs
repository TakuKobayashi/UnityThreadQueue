using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AsyncSample : MonoBehaviour {
	private int counter = 0;

	void Start () {
		// first task
		UnityThreadQueue.Instance.Enqueue (() => {
			for(int i = 0;i < 10;++i){
				Thread.Sleep(1000);
				counter++;
			}
		});
		// second task
		UnityThreadQueue.Instance.Enqueue (() => {
			counter = -129389152;
			Thread.Sleep(3000);
		});
	}
	
	void OnGUI() {
		if (UnityThreadQueue.Instance.ExistQueueEvent) {
			GUI.Label (new Rect (300, 0, 200, 200), counter.ToString ());
		} else {
			GUI.Label (new Rect (300, 0, 200, 200), "complete!!!!");
		}
	}
}
