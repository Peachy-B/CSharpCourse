```chsarp
using System.Security.Cryptography;

namespace note
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# 공부를 시작합니다");
            Console.WriteLine("오늘의 목표: 직접 실행하기");

            Console.WriteLine("입문자");
            Console.WriteLine("C# 배우기");

            int health; // 선언
            health = 100; // 첫 값 대입
            health = 75; // 기존 값을 새 값으로 바꿈
            // 오른쪽을 먼저 계산하고 그 결과를 왼쪽 변수에 저장
            Console.WriteLine(health); // 변수의 값인 75 출력

            int count = 10;
            string label = "10";
            Console.WriteLine(count + 5); // 숫자끼리 덧셈
            Console.WriteLine(label + "5"); // 글자끼리 이어붙임

            Console.WriteLine("health"); // health라는 글자 출력
            Console.WriteLine($"현재 체력은 {health}입니다."); // 문자열 보간

            int a = 1;
            int b = 5;
            Console.WriteLine(a); // 1
            Console.WriteLine(b); // 5

            Swap(ref a, ref b); // a와 b의 값을 서로 바꿈
            // ref : 값만 복사하는 것이 아닌 실제 Main 의 a와 b의 값이 바뀜
            Console.WriteLine(a); // 5
            Console.WriteLine(b); // 1
            
            Add(4, 10); // 14

            var value = Add2(3, 5); // 8

            Console.WriteLine(value);
        }

        static void Swap(ref int a, ref int b)
        {
            int temp = a; // temp에 a의 값 1을 저장
            a = b; // a에 b의 값 5를 덮어씀
            b = temp; // b에 temp인 a의 값 1을 넣음
        }

        static void Add(int a1, int a2) // void : 화면에 출력만 하고 끝냄
        {
            Console.WriteLine(a1 + a2);
        }

        static int Add2(int a1, int a2) // int + return : 계산 결과를 돌려줌
        {
            return a1 + a2; // swap과 다르게 return은 한번에 한개의 값만 돌려줌
        }
    }
}

```