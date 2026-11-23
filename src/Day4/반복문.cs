// 반복문
// 반복표현식을 줄일 수 있는 방법이 있을까?
// 프로그래머 반복을 진짜 싫어한다.

// for반복문

// while반복문
// 9x9단

for (int dan = 2; dan <= 9; dan++)
{
    Console.WriteLine($"=== {dan}단 ===");

    // 안쪽 for문, 각 단마다 1부터 9까지 곱하기
    for (int num = 1; num <= 9; num++)
    {
        Console.WriteLine($"{dan} x {num} = {dan * num}");
    }
    Console.WriteLine(); // 단 사이 줄바꿈
}

// for반복문은, 100번 반복할거야. 반복횟수 눈에 보일때만 쓰면 효율적이다.

// while반복문
// 무한 반복문.
// 게임을 종료할 조건
// while반복문은 언제 끝이 날지 모를때 쓰면 좋다.

int grade = 0;
bool gamefinish = grade >= 10;
while(true)
{

    grade++;
    Console.WriteLine($"현재 등급 : {grade}");


    if(grade >= 10)
    {
        break;
    }
}

Console.WriteLine("게임 종료");