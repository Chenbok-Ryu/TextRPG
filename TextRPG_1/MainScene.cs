using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Game // 게임 클래스
{
    private Player player; // 플레이어 객체
    private Inventory inventory; // 인벤토리 객체
    private Shop shop; // 상점 객체
    private Dungeon dungeon; // 던전 객체

    public void Start() // 게임 시작 메서드
    {
        player = new Player("Chad"); // 플레이어 이름 설정
        inventory = new Inventory(); // 인벤토리 초기화
        shop = new Shop(); // 상점 초기화
        dungeon = new Dungeon(); // 던전 초기화

        player.Inventory = inventory; // 플레이어의 인벤토리 설정

        MainLoop(); // 메인 루프 시작
    }

    private void MainLoop() // 메인 루프
    {
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 저장 후 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player.ShowStatus(); // 플레이어 상태 보기
                    break;
                case "2":
                    inventory.ShowInventory(player); // 인벤토리 보기
                    break;
                case "3":
                    shop.ShowShop(player, inventory); // 상점 보기
                    break;
                case "4":
                    dungeon.Enter(player); // 던전 입장
                    break;
                case "5":
                    Rest(player); // 휴식하기
                    break;
                case "6":
                    SaveSystem.Save(player, inventory); // 저장 후 나가기
                    Console.WriteLine("저장 후 게임을 종료합니다."); 
                    Environment.Exit(0); 
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }
    }


    public void Rest(Player player) // 휴식하기 메서드
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)\n");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            if (input == "0") 
                break;
            else if (input == "1")
            {
                if (player.Gold >= 500) 
                {
                    player.Gold -= 500;
                    player.HP = player.MaxHP;
                    Console.WriteLine("휴식을 완료했습니다.");
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
}
