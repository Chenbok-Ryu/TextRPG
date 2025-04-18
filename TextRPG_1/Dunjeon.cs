using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dungeon // 던전 클래스
{
    public void Enter(Player player) // 던전 입장 메서드
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            int recommendedDef = 0; 
            int baseReward = 0; 
            string difficultyName = ""; 

            switch (input) // 던전 난이도 선택
            {
                case "1":
                    recommendedDef = 5;
                    baseReward = 1000;
                    difficultyName = "쉬운 던전";
                    break;
                case "2":
                    recommendedDef = 11;
                    baseReward = 1700;
                    difficultyName = "일반 던전";
                    break;
                case "3":
                    recommendedDef = 17;
                    baseReward = 2500;
                    difficultyName = "어려운 던전";
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    continue;
            }

            HandleDungeonRun(player, recommendedDef, baseReward, difficultyName);
        }
    }

    private void HandleDungeonRun(Player player, int recommendedDef, int baseReward, string dungeonName) // 던전 탐험 처리 메서드
    {
        Console.Clear();
        Random rand = new Random();

        int defDiff = player.Def - recommendedDef;
        bool failed = false;

        if (player.Def < recommendedDef)
        {
            if (rand.Next(0, 100) < 40)
            {
                failed = true;
            }
        }

        int hpBefore = player.HP;
        int goldBefore = player.Gold;

        if (failed)
        {
            // 던전 실패
            int damage = player.HP / 2;
            player.HP -= damage;

            if (player.HP < 0) player.HP = 0;

            Console.WriteLine("던전 실패...");
            Console.WriteLine($"[탐험 결과]\n체력 {hpBefore} -> {player.HP} (절반 감소)\n보상 없음");
        }
        else
        {
            // 던전 성공
            int baseMin = 20 + Math.Max(0, -defDiff);
            int baseMax = 35 + Math.Max(0, -defDiff);
            int hpLoss = rand.Next(baseMin, baseMax + 1);

            player.HP -= hpLoss;
            if (player.HP < 0) player.HP = 0;

            float bonusPercent = (float)rand.Next(player.Atk, player.Atk * 2 + 1) / 100f;
            int bonusGold = (int)(baseReward * bonusPercent);
            int totalGold = baseReward + bonusGold;

            player.Gold += totalGold;

            Console.WriteLine("던전 클리어\n축하합니다!!");
            Console.WriteLine($"{dungeonName}을 클리어 하였습니다.");
            Console.WriteLine($"\n[탐험 결과]");
            Console.WriteLine($"체력 {hpBefore} -> {player.HP}");
            Console.WriteLine($"Gold {goldBefore} G -> {player.Gold} G");
            player.DungeonClearCount++;
            player.CheckLevelUp();
        }

        Console.WriteLine("\n0. 나가기");
        Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        Console.ReadLine();
    }
}
