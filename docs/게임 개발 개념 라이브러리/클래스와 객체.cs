Player hero = new Player("용사", 100);
//hero.Name = "용사";
Player slime = new Player("슬라임", 30); // slime이름으로 player크기만큼 공간 할당
//slime.Name = "슬라임";
//slime.Hp = 30;

// 체력, 이름 공격했으면 어떤 식으로 데미지 log 찍어라.
// cw tab => console.writeline

// malloc 메모리할당
// ~Player() 소멸자

hero.TakeDamage(30);
hero.PrintStatus();
slime.PrintStatus();

class Player
{
    public string Name = "";
    public int Hp = 100;

    public Player(string Name, int Hp)
    {
        this.Name = Name;
        this.Hp = Hp;
    }

    public void TakeDamage(int HP)
    {
        this.Hp -= HP;
    }
    public void PrintStatus()
    {
        Console.WriteLine($"{this.Name}: Hp {Hp}");
    }
}