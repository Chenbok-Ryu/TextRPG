using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

[Serializable] 
public class SaveData
{
    public string Name;
    public string Job;
    public int Level;
    public float BaseAtk;
    public int BaseDef;
    public int HP;
    public int MaxHP;
    public int Gold;
    public int DungeonClearCount;

    public List<Item> InventoryItems;
}

