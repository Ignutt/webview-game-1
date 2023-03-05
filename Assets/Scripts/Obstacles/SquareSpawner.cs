using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Player;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class SquareSpawner : MonoBehaviour
    {
        [Header("Movement properties")] 
        [SerializeField] private Range randomMovementX;
        [SerializeField] private Range randomMovementY;

        [Header("Spawn properties")] 
        [SerializeField] private float spawnRate = 2;
        [SerializeField] private Square[] squaresPrefabs;
        [SerializeField] private Transform beginSpawnPoint;
        [SerializeField] private Transform endSpawnPoint;
        
        private readonly List<Square> _spawnedSquaresList = new List<Square>();

        private void Start()
        {
            StartCoroutine(SpawnDelay());

            GameManager.Instance.OnGameOver += () =>
            {
                foreach (var obj in _spawnedSquaresList)
                {
                    if (obj == null) continue;
                        
                    Destroy(obj.gameObject);
                }

                gameObject.SetActive(false);
            };
        }

        private void Spawn()
        {
            Square randSquare = squaresPrefabs[Random.Range(0, squaresPrefabs.Length)];
            
            Vector3 spawnPosition = new Vector2(
                Random.Range(beginSpawnPoint.position.x, endSpawnPoint.position.x),
                beginSpawnPoint.position.y);

            Square spawnedSquare = Instantiate(randSquare, spawnPosition, Quaternion.identity);
            spawnedSquare.TargetVelocity = new Vector2(
                randomMovementX.Random(),
                randomMovementY.Random());
            
            _spawnedSquaresList.Add(spawnedSquare);
        }

        private IEnumerator SpawnDelay()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}
