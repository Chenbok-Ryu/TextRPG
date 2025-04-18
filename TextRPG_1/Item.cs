using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ItemType // 아이템 타입
{
    Weapon,
    Armor
}

[Serializable]
public class Item // 아이템 클래스
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int AtkBonus { get; set; }
    public int DefBonus { get; set; }
    public int Price { get; set; }
    public bool IsEquipped { get; set; }
    public ItemType Type { get; set; }

    public Item(string name, int atk, int def, string description, int price, ItemType type) // 생성자
    {
        Name = name;
        AtkBonus = atk;
        DefBonus = def;
        Description = description;
        Price = price;
        IsEquipped = false;
        Type = type;
    }
}

