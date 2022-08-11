using System.Collections;
using System.Collections.Generic;
using System;
using Dialogue;
using UnityEngine;

namespace EnemyAI
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public bool canSpawn;
        public GameObject enemy;
        public GameObject currentRoom;
        PlayerController player;
        public GameObject dialogBox;
        public Conversation con_enemyspawn;

        private void Start() 
        {
            player = FindObjectOfType<PlayerController>();
            canSpawn = true;

            dialogBox = EventManager.dialogBox;
            EventManager.EnemyCanSpawn += SpawnEnemy; //Subscribed!
            EventManager.EnemyDeath += DespawnEnemy;
        }
        
        private void Update()
        {
            currentRoom = FindRoom();
        }

        void SpawnEnemy()
        {
            if (currentRoom != null) { 
            dialogBox.SetActive(true);
            DialogDisplay dd = dialogBox.GetComponentInChildren<DialogDisplay>();
            dd.conversation = con_enemyspawn;
            dd.simulateClick = true;
            
            Instantiate(enemy, currentRoom.GetComponent<Room>().spawnPoint);
            canSpawn = false;
                
                AudioManager.instance.Play("Monster");
            }
        }

        void DespawnEnemy()
        {
            canSpawn = true;
            EventManager.EnemyCanSpawn -= SpawnEnemy;
            EventManager.EnemyCanSpawn += SpawnEnemy;
            AudioManager.instance.Stop("Monster");
            

        }

        public GameObject FindRoom()
        {
            GameObject[] rooms;
            rooms = GameObject.FindGameObjectsWithTag("Room");

            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = player.transform.position;
            foreach (GameObject active in rooms)
            {
                Vector3 diff = active.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = active;
                    distance = curDistance;
                }
            }
            return closest;
        }
    }
}
