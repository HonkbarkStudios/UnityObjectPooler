using UnityEngine;
using System.Collections.Generic;

public class GameObjectPooler : MonoBehaviour {

	public static GameObjectPooler Instance;
	private Dictionary<string, List<GameObject>> ObjectPool;

	void Awake() {
		Instance = this;
		this.ObjectPool = new Dictionary<string, List<GameObject>>();
	}

	public GameObject GetObject(GameObject prefab) {
		var nameOfPrefab = prefab.name.Replace("(Clone)", "");
		GameObject gameObject;
		if(this.ObjectPool.ContainsKey(nameOfPrefab) && this.HasInactiveGameObject(nameOfPrefab)) {
			gameObject = this.GetInactiveGameObject(nameOfPrefab);
		}
		else {
			gameObject = this.InstatiateNewGameObject(prefab);
			this.AddToObjectPool(gameObject, nameOfPrefab);
		}
		return gameObject;
	}

	private bool HasInactiveGameObject(string nameOfPrefab) {
		var hasInactiveGameObject = false;
		foreach(GameObject gameObject in this.ObjectPool[nameOfPrefab]) {
			if(!gameObject.activeInHierarchy) {
				hasInactiveGameObject = true;
			}
		}
		return hasInactiveGameObject;
	}

	private GameObject GetInactiveGameObject(string nameOfPrefab) {
		GameObject gameObject = null;
		foreach(GameObject pooledGameObject in this.ObjectPool[nameOfPrefab]) {
			if(!pooledGameObject.activeInHierarchy) {
				gameObject = pooledGameObject;
			}
		}
		return gameObject;
	}

	private void AddToObjectPool(GameObject gameObject, string nameOfPrefab) {
		if(this.ObjectPool.ContainsKey(nameOfPrefab)) {
			this.AddObjectToExistingPool(gameObject, nameOfPrefab);
		}
		else {
			this.AddToNewObjectPool(gameObject, nameOfPrefab);
		}
	}

	private void AddToNewObjectPool(GameObject gameObject, string nameOfPrefab) {
		var list = this.GetNewGameObjectList(gameObject);
		this.ObjectPool.Add(nameOfPrefab, list);
	}

	private void AddObjectToExistingPool(GameObject gameObject, string nameOfPrefab) {
		this.ObjectPool[nameOfPrefab].Add(gameObject);
	}

	private List<GameObject>GetNewGameObjectList(GameObject gameObject) {
		var list = new List<GameObject>();
		list.Add(gameObject);
		return list;
	}
	
	private GameObject InstatiateNewGameObject(GameObject gameObject) {
		gameObject = (GameObject)Instantiate(gameObject);
		gameObject.SetActive(false);
		return gameObject;
	}		          
}
