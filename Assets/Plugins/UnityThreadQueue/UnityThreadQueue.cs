using System;
using System.Collections.Generic;
using System.Threading;

public class UnityThreadQueue : IDisposable{
	private static UnityThreadQueue instance;

	public static UnityThreadQueue Instance{
		get
		{
			if (instance == null){
				instance = new UnityThreadQueue();
			}
			return instance;
		}
	}

	private UnityThreadQueue(){
		ThreadPool.QueueUserWorkItem(new WaitCallback(Execute));
	}

	private Queue<Action> taskQueue = new Queue<Action>();
	private volatile bool isExistQueueEvent = false;
	private volatile bool running = true;
	private ManualResetEvent queueEvent = new ManualResetEvent(false);

	private void Execute(object o){
		while (queueEvent.WaitOne()){
			if (!running) { break; }

			Action actor = null;
			lock (taskQueue){
				if (taskQueue.Count > 0){
					actor = taskQueue.Dequeue();
				}else{
					isExistQueueEvent = false;
					queueEvent.Reset();
				}
			}
			if (actor != null){
				try{
					actor();
				}catch (Exception e){
					lock (taskQueue) {
						running = false;
						taskQueue.Clear ();
					}
					throw;
				}
			}
		}
	}

	public bool ExistQueueEvent{
		get{
			return isExistQueueEvent;
		}
	}

	public void Enqueue(Action task)
	{
		lock (taskQueue)
		{
			taskQueue.Enqueue(task);
			queueEvent.Set();
			isExistQueueEvent = true;
		}
	}

	public void Dispose(){
		running = false;
		isExistQueueEvent = false;
		queueEvent.Set();
	}
}