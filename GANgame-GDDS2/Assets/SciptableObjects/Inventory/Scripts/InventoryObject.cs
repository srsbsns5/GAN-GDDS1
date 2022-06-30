using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;


//Creates and handles item management in the player's inventory.
[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    [SerializeField]
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>(10); 
    public ItemObject equippedItem = null;

    private void OnEnable()
    {
        equippedItem = null;
#if UNITY_EDITOR //elyza says This means that this part will ONLY run in the UNITY EDITOR and NOT THE BUILD VERSION. (TELL ME IF THE CAPS LOCK IS USEFUL!!!!)
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public void AddItem(ItemObject _item, int _amount) //Adds item to player inventory
    {
        //For loop that checks if item already exists in inventory,
        //Then adds to amount if item is already in inventory. NOT CURRENTLY IN USE
        //for (int i = 0; i < Container.Count; i++) 
        //{
        //    if (Container[i].item == _item)
        //    {
        //        Container[i].AddAmount(_amount); //If item already exists, adds to the amount 
        //        return;
        //    }
        //}
        //If item is not already in inventory
        //Adds an inventory slot and passes item variables to constructor
        if (Container.Count > 10) 
        {
            Container.RemoveAt(10); return; //elyza says This is to NOT pick up any inventory item if maximum slots has been reached
        }
        Container.Add(new InventorySlot(database.GetID[_item], _item, _amount)); 
        Debug.Log("added " + _item);
        
    }

    public void DropItem(int _is, Vector2 pos)  //elyza says Drops item at position. _is stands for inventory system rember plz.
    {
        //Gets an ItemObject from GetItemObject(), Then drops the item.
        ItemObject IO = GetItemObject(_is);
        Debug.Log(Container[_is].amount);
        Instantiate(IO.itemPrefab, new Vector2(pos.x, pos.y - 2.5f), Quaternion.identity);  //elyza says Instantiates the called item at the Vector2 position
        Container.RemoveAt(_is); //elyza says Removes item from the Container (i.e. the inventory slots)
        if(IO == equippedItem) { equippedItem = null; } //elyza says Ensures the equipped item resets
        EventManager.ItemEquip(); 
    }

    public ItemObject GetItemObject(int i)//gets item id in inventory and returns it
    {
        //The keypress passes an integer into this function,
        //it then returns an ItemObject corresponding to it's order in the inventory
        ItemObject _io = Container[i].item;     
        Debug.Log("Getting " + _io);

        //If there is no item, returns a null. elyza says This is an opportune moment to add VISUAL and AUDIO FEEDBACK
        //If there is item, returns ItemObject.
        if (_io != null)
            return _io;
        else { Debug.Log("nth in this bish"); return null;  }
    }


    public void RemoveItem( ItemObject io)   //Ridiculously inefficient way to remove an item from the inventory. But it works
    {
        foreach(InventorySlot _is in Container) //elyza says If it works it works perfectly. Not necessary to understand!
        {
            if (_is.item == io)
            {
                if (_is.amount > 1)
                {
                    _is.ReduceAmount(1);
                }
                else
                {
                    Container.Remove(_is);
                    Debug.Log("Removed " + _is.item);
                    break;
                }
            }


        }
    }

    //Save-Load system for inventory. Tbh I don't quite get it either. elyza says Me neither. Greg, MX, just give up.
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true); //elyza says The only json i know is derulo
        BinaryFormatter bf = new BinaryFormatter(); //binary formatter converts data types e.g. int, float etc into binary for file saving - greg
        FileStream fs = File.Create(string.Concat(Application.persistentDataPath, savePath)); //creates or overwrites save file in chosen savePath - greg
        bf.Serialize(fs, saveData);
        fs.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) //elyza says you lost me after file exists
        {
            BinaryFormatter bf = new BinaryFormatter(); //converts the data types to be read by the program so that it can be loaded - greg
            FileStream fs = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open); //opens file in savePath to load - greg
            JsonUtility.FromJsonOverwrite(bf.Deserialize(fs).ToString(), this); // the fuck what - greg
            fs.Close();
        }
    }


    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++) Container[i].item = database.GetItem[Container[i].ID];

    }

    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class InventorySlot  //Inventory class
{
    public int ID;
    public ItemObject item;
    public int amount;

    public InventorySlot(int _id, ItemObject _item, int _amount) //Item constructor
    {
        item = _item;
        amount = _amount;
        ID = _id;
        Debug.Log("New " + _item + " Created");
    }
    public void AddAmount(int value)
    {
        amount += value; //elyza says Self explanatory
    }

    public void ReduceAmount(int value)
    {
         amount -= value;
         
    }
}