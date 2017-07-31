# What is This
It is Library that performs processing in the background in Unity.
It is used the [Thread](https://en.wikipedia.org/wiki/Thread_(computing), "Thread") and [Queue](https://en.wikipedia.org/wiki/Queue_(abstract_data_type) "Queue") system.

# Install
Since unitypackage is in release tag, install it.
https://github.com/TakuLibraries/UnityThreadQueue/releases

# Usage
Created samples with AsyncSample.cs (asynchronously) and SyncSample.cs (synchronized ones), as a example.
So for details, please look over there.

 * Basic usage

```
UnityThreadQueue.Instance.Enqueue (() => {
  //Please describe what you want to do asynchronously here
});
```

* the property that determines whether there is a process being executed asynchronously (Thread)

```
public bool UnityThreadQueue.Instance.ExistQueueEvent;
```

 * An example of combining the above two and synchronizing with the processing done in Thread

```
void Start(){
  UnityThreadQueue.Instance.Enqueue (() => {
    //Please describe what you want to do asynchronously here
  });
  StartCoroutine (SyncCorutine());
}

private IEnumerator SyncCorutine(){
  while (UnityThreadQueue.Instance.ExistQueueEvent) {
    yield return null;
  }
  // Write the process you want to write after synchronizing
}
```

# Others

I wrote a more detailed article, However it is in Japanese.
http://qiita.com/taptappun/items/debb9d36099184a0b92e
