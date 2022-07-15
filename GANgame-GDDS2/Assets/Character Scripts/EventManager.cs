using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action OpenInventory;
    public static event Action CloseInventory;
    public static event Action EquipItem;

    public static event Action PaintingCompleted;

    public static event Action InteractEvent;

    public static event Action EnemyCanSpawn;
    public static event Action EnemyDeath;
    public static event Action EnemySearch;
    public static event Action PauseScreen;

    public static void InventoryToggle()
    {
        OpenInventory?.Invoke();
    }

    public static void InventoryClose()     //UNUSED
    {
        CloseInventory?.Invoke();
    }

    public static void ItemEquip()
    {
        EquipItem?.Invoke();
    }

    public static void CompletePainting() //UNUSED
    {
        PaintingCompleted?.Invoke();
    }

    public static void Interact()
    {
        InteractEvent?.Invoke();
    }

    public static void SpawnChecker()
    {
        EnemyCanSpawn?.Invoke();
    }

    public static void EnemyDied()
    {
        EnemyDeath?.Invoke();
    }

    public static void EnemyIsSearching()
    {
        EnemySearch?.Invoke();
    }

    public static void PauseMenu()
    {
        PauseScreen?.Invoke();
    }
   
}
