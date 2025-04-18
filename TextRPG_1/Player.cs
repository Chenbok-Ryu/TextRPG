using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player // 플레이어 클래스
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Gold { get; set; }
    public float BaseAtk { get; set; }
    public int BaseDef { get; set; }
    public int DungeonClearCount { get; set; }
    public Inventory Inventory { get; set; }

    public Player(string name)
    {
        Name = name;
        Job = "전사";
        Level = 1;
        HP = 100;
        MaxHP = 100;
        Gold = 0;

        BaseAtk = 10;
        BaseDef = 5;
        Atk = (int)Math.Floor(BaseAtk);
        Def = BaseDef;
    }

    public void ApplyItemStatus(List<Item> items)
    {
        Atk = (int)Math.Floor(BaseAtk);
        Def = BaseDef;

        if (items == null)
        {
            Console.WriteLine("[경고] 장착 아이템 리스트가 null입니다.");
            return;
        }

        foreach (var item in items)
        {
            if (item == null) continue; // 아이템 자체가 null일 수도 있음

            if (item.IsEquipped)
            {
                Atk += item.AtkBonus;
                Def += item.DefBonus;
            }
        }
    }

    public void ShowStatus() // 상태 보기
    {
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

        int totalAtk = (int)Math.Floor(BaseAtk); // 정수로 변환
        int totalDef = BaseDef;

        foreach (var item in Inventory.GetItems())
        {
            if (item.IsEquipped)
            {
                totalAtk += item.AtkBonus; // 아이템의 공격력 보너스 적용
                totalDef += item.DefBonus; // 아이템의 방어력 보너스 적용
            }
        }

        Console.WriteLine($"Lv. {Level}");
        Console.WriteLine($"{Name} ( {Job} )");
        Console.WriteLine($"공격력 : {totalAtk} (+{totalAtk - (int)Math.Floor(BaseAtk)})");
        Console.WriteLine($"방어력 : {totalDef} (+{totalDef - BaseDef})");
        Console.WriteLine($"체  력 : {HP}");
        Console.WriteLine($"Gold : {Gold} G");

        Console.WriteLine("\n0. 나가기");
        Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        Console.ReadLine();
    }

    public void CheckLevelUp() // 레벨업 체크
    {
        int requiredClearCount = 0; // 던전 클리어 횟수

        switch (Level)
        {
            case 1: requiredClearCount = 1; break; // 레벨 1 -> 2
            case 2: requiredClearCount = 2; break; // 레벨 2 -> 3
            case 3: requiredClearCount = 3; break; // 레벨 3 -> 4
            case 4: requiredClearCount = 4; break; // 레벨 4 -> 5
            default: return; // 레벨 5 이상은 안 올림
        }

        if (DungeonClearCount >= requiredClearCount)  
        {
            Level++;
            DungeonClearCount = 0; // 초기화
            BaseAtk += 0.5f;
            BaseDef += 1;
            Console.WriteLine($"\n레벨업! Lv.{Level}이 되었습니다!");
            Console.WriteLine("기본 공격력 +0.5 / 기본 방어력 +1 상승!");
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }

}
