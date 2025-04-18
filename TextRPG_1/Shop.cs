using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Shop // 상점 클래스
{
    private List<Item> shopItems; // 상점에서 판매하는 아이템 목록

    public Shop()
    {
        shopItems = new List<Item> // 상점 아이템 초기화
        {
            // 아이템 이름, 공격력 보너스, 방어력 보너스, 설명, 가격, 아이템 타입
            new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor), 
            new Item("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, ItemType.Armor),
            new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor),
            new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon),
            new Item("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Armor),
            new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, ItemType.Armor)
        };
    }

    public void ShowShop(Player player, Inventory inventory) // 상점 보여주기
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < shopItems.Count; i++) // 상점 아이템 목록 출력
            {
                Item item = shopItems[i]; // 상점 아이템
                bool alreadyOwned = inventory.HasItem(item.Name); // 인벤토리에 있는지 확인
                string statText = item.AtkBonus > 0 
                    ? $"공격력 +{item.AtkBonus}"
                    : $"방어력 +{item.DefBonus}";
                string priceText = alreadyOwned ? "구매완료" : $"{item.Price} G";

                Console.WriteLine($"- {i + 1} {item.Name,-12} | {statText,-10} | {item.Description,-35} | {priceText}"); // 아이템 정보 출력
            }

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    BuyItem(player, inventory);
                    break;
                case "2":
                    SellItem(player, inventory);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르세요...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public void BuyItem(Player player, Inventory inventory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < shopItems.Count; i++) // 상점 아이템 목록 출력
            {
                Item item = shopItems[i];
                bool alreadyOwned = inventory.HasItem(item.Name); // 인벤토리에 있는지 확인
                string statText = item.AtkBonus > 0
                    ? $"공격력 +{item.AtkBonus}"
                    : $"방어력 +{item.DefBonus}";
                string priceText = alreadyOwned ? "구매완료" : $"{item.Price} G";

                Console.WriteLine($"- {i + 1} {item.Name,-12} | {statText,-10} | {item.Description,-35} | {priceText}"); // 아이템 정보 출력
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= shopItems.Count)
            {
                Item selectedItem = shopItems[index - 1]; // 선택한 아이템

                if (inventory.HasItem(selectedItem.Name))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (player.Gold >= selectedItem.Price)
                {
                    // 구매 성공
                    player.Gold -= selectedItem.Price;

                    // 새 인스턴스를 인벤토리에 추가
                    Item itemCopy = new Item(
                        selectedItem.Name,
                        selectedItem.AtkBonus,
                        selectedItem.DefBonus,
                        selectedItem.Description,
                        selectedItem.Price,
                        selectedItem.Type
                    );

                    inventory.AddItem(itemCopy);

                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                {
                    Console.WriteLine("Gold 가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }

    public void SellItem(Player player, Inventory inventory) // 아이템 판매
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            List<Item> sellableItems = inventory.GetItems(); // 인벤토리 목록 가져오기
            List<Item> ownedShopItems = sellableItems.FindAll(item => item.Price > 0);

            if (ownedShopItems.Count == 0)
            {
                Console.WriteLine("판매 가능한 아이템이 없습니다.");
                Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < ownedShopItems.Count; i++) // 판매 가능한 아이템 목록 출력
            {
                Item item = ownedShopItems[i];
                string statText = item.AtkBonus > 0
                    ? $"공격력 +{item.AtkBonus}"
                    : $"방어력 +{item.DefBonus}";
                int sellPrice = (int)(item.Price * 0.85f);

                Console.WriteLine($"- {i + 1} {item.Name,-12} | {statText,-10} | {item.Description,-35} |  {sellPrice} G");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= ownedShopItems.Count)
            {
                Item itemToSell = ownedShopItems[index - 1];
                int sellPrice = (int)(itemToSell.Price * 0.85f);

                if (itemToSell.IsEquipped)
                {
                    itemToSell.IsEquipped = false;
                    Console.WriteLine($"'{itemToSell.Name}' 장착을 해제했습니다.");
                }

                player.Gold += sellPrice;
                inventory.RemoveItem(itemToSell);

                Console.WriteLine($"'{itemToSell.Name}' 을(를) 판매했습니다. +{sellPrice} G");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }

}

