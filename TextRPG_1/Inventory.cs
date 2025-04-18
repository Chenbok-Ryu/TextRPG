using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Inventory // 인벤토리 클래스
{
    private List<Item> items; // 인벤토리 아이템 목록

    public Inventory()
    {
        items = new List<Item>();

        items.Add(new Item("무쇠갑옷", 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, ItemType.Armor) { IsEquipped = true });
        items.Add(new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, ItemType.Weapon) { IsEquipped = true });
        items.Add(new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 1000, ItemType.Weapon));
    }

    public void ShowInventory(Player player) // 인벤토리 보여주기
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("[아이템 목록]");

        if (items.Count == 0) // 인벤토리에 아이템이 없을 경우
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            foreach (Item item in items) 
            {
                string equipTag = item.IsEquipped ? "[E]" : "   "; 
                string statText = item.AtkBonus > 0
                    ? $"공격력 +{item.AtkBonus}"
                    : $"방어력 +{item.DefBonus}";
                Console.WriteLine($"- {equipTag}{item.Name,-14} | {statText} | {item.Description}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if (input == "1")
            ManageEquip(player);
    }

    public List<Item> GetItems() // 인벤토리 아이템 목록 가져오기
    {
        return items;
    }

    public void ManageEquip(Player player) // 장착 관리
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < items.Count; i++) // 인벤토리 아이템 목록 출력
            {
                Item item = items[i];
                string equipTag = item.IsEquipped ? "[E]" : "   ";
                string statText = item.AtkBonus > 0
                    ? $"공격력 +{item.AtkBonus}"
                    : $"방어력 +{item.DefBonus}";
                Console.WriteLine($"- {i + 1} {equipTag}{item.Name,-14} | {statText} | {item.Description}");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= items.Count)
            {
                Item selectedItem = items[index - 1];

                if (selectedItem.IsEquipped)
                {
                    selectedItem.IsEquipped = false;
                    Console.WriteLine($"{selectedItem.Name} 장착을 해제했습니다.");
                }
                else
                {
                    // 같은 타입의 장비가 이미 장착되어 있다면 해제
                    foreach (Item item in items)
                    {
                        if (item.Type == selectedItem.Type && item.IsEquipped)
                        {
                            item.IsEquipped = false;
                            Console.WriteLine($"{item.Name} 장착을 해제했습니다.");
                        }
                    }

                    selectedItem.IsEquipped = true;
                    Console.WriteLine($"{selectedItem.Name} 을(를) 장착했습니다.");
                }

                // 장비 변경 후 능력치 재계산
                player.ApplyItemStatus(items);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }

    public bool HasItem(string itemName) // 인벤토리에 아이템이 있는지 확인
    {
        foreach (var item in items) 
        {
            if (item.Name == itemName) 
                return items.Exists(item => item.Name == itemName);
        }
        return false;
    }

    public void AddItem(Item item) // 인벤토리에 아이템 추가
    {
        items.Add(item);
    }
    public void RemoveItem(Item item) // 인벤토리에서 아이템 제거
    {
        items.Remove(item);
    }
    public void SetItems(List<Item> loadedItems) // 인벤토리에 아이템 설정
    {
        items = loadedItems;
    }

}
