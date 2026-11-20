// 분기문
// 코드를 원하는 부분만 실행하겠다.

/*
 *  if( 조건문)
 *  {
 *  
 *  }
 */

// 무엇이 조건이냐? True / false

// 보유돈, 강화할 때 필요한 돈

int wallet = 1000;
int cost = 500;

bool condition = wallet > cost;
if (condition) // 문장 끝에 ; 넣으면 중괄호 한줄이 더 생겨버리니 주의!
{
    Console.WriteLine("강화를 합니다."); // 조건이 참일 경우
}
else
{
    Console.WriteLine("돈이 부족합니다."); // 조건이 거짓일 경우
}
// 키보드로 입력받은 글자를 int.Parse를 통해 숫자로 바꾸어 choice에 저장
int choice = int.Parse(Console.ReadLine());// 약속> 1주먹 2가위 3보 4. 치트

// 상수. 변하지 않게 강제한다.


//const int ROCK = 1;
//const int SCISSORS = 2;
//const int PAPER = 3;

Random rand =  new Random();

int otherChoice = rand.Next(1,4);
Console.WriteLine(otherChoice);

if(choice == (int)note.Choice.ROCK)
{
    Console.WriteLine("주먹을 냈습니다.");
}
else if (choice == (int)note.Choice.SCISSORS)
{
    Console.WriteLine("가위를 냈습니다.");
}
else if (choice == (int)note.Choice.PAPER)
{
    Console.WriteLine("보를 냈습니다.");
}
else
{
    Console.WriteLine("잘못된 입력을 받았습니다.");
}

