using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EnemyAI
{
    public class Room : MonoBehaviour 
    {
        public int roomNo;
        public float timeToSpawn;
        float originalTime;
        public Transform spawnPoint;
    
        EnemySpawnManager esm;
        
        private void Start() 
        {
            esm = FindObjectOfType<EnemySpawnManager>();
            spawnPoint = transform.GetChild(0).transform;
            originalTime = timeToSpawn;
        }
        void Update()
        {
            if (esm.currentRoom.GetComponent<Room>().roomNo == roomNo && esm.canSpawn)
            {
                timeToSpawn -= Time.deltaTime;
            }
            else
            {
                timeToSpawn -= 0;
            }

            if (timeToSpawn <= 0)
            {
                timeToSpawn = originalTime;
                EventManager.SpawnChecker();
                //print("fn check");
            }
        }
    }
}