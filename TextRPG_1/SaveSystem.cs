using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class SaveSystem
{
    private static string path = "save.json";

    public static void Save(Player player, Inventory inventory)
    {
        SaveData data = new SaveData
        {
            Name = player.Name,
            Job = player.Job,
            Level = player.Level,
            BaseAtk = player.BaseAtk,
            BaseDef = player.BaseDef,
            HP = player.HP,
            MaxHP = player.MaxHP,
            Gold = player.Gold,
            DungeonClearCount = player.DungeonClearCount,
            InventoryItems = inventory.GetItems()
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() } // 🔥 enum 변환 처리
        };

        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
        Console.WriteLine("게임이 저장되었습니다.");
    }

    public static void Load(Player player, Inventory inventory)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("저장된 게임이 없습니다.");
            return;
        }

        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };

        string json = File.ReadAllText(path);
        SaveData data = JsonSerializer.Deserialize<SaveData>(json, options);

        // 1. Player 정보 복원
        player.Name = data.Name;
        player.Job = data.Job;
        player.Level = data.Level;
        player.BaseAtk = data.BaseAtk;
        player.BaseDef = data.BaseDef;
        player.HP = data.HP;
        player.MaxHP = data.MaxHP;
        player.Gold = data.Gold;
        player.DungeonClearCount = data.DungeonClearCount;

        inventory.SetItems(data.InventoryItems);

        player.ApplyItemStatus(inventory.GetItems());

        Console.WriteLine("게임을 불러왔습니다.");
    }
}
