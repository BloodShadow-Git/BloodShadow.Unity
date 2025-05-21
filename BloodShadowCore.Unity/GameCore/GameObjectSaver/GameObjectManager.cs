using BloodShadow.GameCore.IDable;
using System.Collections.Generic;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.GameObjectSaver
{
    public class GameObjectManager : IIDManager<string, GameObject>
    {
        public GameObject this[string key] { get => _gameObjects[key]; set => _gameObjects[key] = value; }
        public IDictionary<string, GameObject> Items => _gameObjects;

        private Dictionary<string, GameObject> _gameObjects;

        public GameObjectManager() { _gameObjects = new Dictionary<string, GameObject>(); }
        public GameObjectManager(Dictionary<string, GameObject> gameObjects) : this() { _gameObjects = gameObjects; }

        public bool Add(string key, GameObject item) => _gameObjects.TryAdd(key, item);
        public bool Add(IDableGameObject gameObject) => _gameObjects.TryAdd(gameObject.ID, gameObject.GameObject);
        public bool Contains(string key) => _gameObjects.ContainsKey(key);
        public bool Remove(string key) => _gameObjects.Remove(key);
    }
}
