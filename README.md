# UnityObjectPooler
A generic handler for pooling game objects in unity

## Adding to your project:
1. Download GameObjectPooler.
2. Add Script to unity.
3. Add an empty game object and add GameObjectPooler as a script component

## How to use:

This is an examplescript of how to use the pooler.
to instantiate a new game object use __GameObjectPooler.Instance.GetObject(LargeCloud);__
then set its position and rotation. After that set the game object to active.
When you don't need the object set it to inactive with this code:

largeCloud.SetActive(false);

```csharp
using UnityEngine;
using System.Collections;

public class EnvironmentSpawner : MonoBehaviour {

	public GameObject LargeCloud;

	void Start () {
		StartCoroutine(SpawnLargeClouds());
	}

	IEnumerator SpawnLargeClouds() {
		yield return new WaitForSeconds(2);
		while(true) {
			var largeCloud = GameObjectPooler.Instance.GetObject(LargeCloud);
			largeCloud.transform.position = new Vector3(0.98f, 5.68f, -0f);
			largeCloud.transform.rotation = Quaternion.identity;
			largeCloud.SetActive(true);
			yield return new WaitForSeconds(2);
		}
	}
}
```

###Preloading objects
To load one or more objects into the object pool before using __GameObjectPooler.Instance.GetObject(LargeCloud);__ use the method __PreloadGameObject(Gameobject Prefab);__
Do this when starting the game instead of when playing and you will avoid drops in performance because of instantiating new gameobjects

```csharp
using UnityEngine;
using System.Collections;

public class EnvironmentSpawner : MonoBehaviour {

	public GameObject LargeCloud;

	void Start () {
		this.PreloadGameObjectsIntoPool();
	}

	private void PreloadGameObjectsIntoPool() {
		this.AddFiveCopiesOfGameObjectToPool(LargeCloud);
	}

	private void AddFiveCopiesOfGameObjectToPool(GameObject gameObject) {
		for(int i = 0; i < 5; i++) {
			GameObjectPooler.Instance.PreloadGameObject(gameObject);
		}
	}
}
```