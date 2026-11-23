class Game
{
    private static bool isRunning = true;
    private static int width = 30;
    private static int height = 20;

    // 플레이어 및 게임 데이터
    private static int playerX = width / 2;
    private static int playerY = height - 2;
    private static int score = 0;

    // 떨어지는 별들의 좌표 리스트 (X, Y)
    private static List<(int x, int y)> stars = new List<(int x, int y)>();
    private static Random rand = new Random();

    public static void Run()
    {
        Console.CursorVisible = false; // 콘솔 커서 깜빡임 숨김
        Console.SetWindowSize(width + 5, height + 5);

        // 게임 루프
        while (isRunning)
        {
            Input();     // 1. 좌우 이동 입력
            Update();    // 2. 별 생성, 낙하, 충돌 및 점수 계산
            Rendering(); // 3. 화면 그리기

            Thread.Sleep(50); // 게임 속도 조절 (숫자가 작을수록 빨라짐)
        }

        // 게임 오버 화면 출력
        Console.Clear();
        Console.WriteLine("=====================");
        Console.WriteLine("     GAME OVER!      ");
        Console.WriteLine($"   최종 점수: {score}점   ");
        Console.WriteLine("=====================");
    }

    // 1. 입력 기능: 좌우 방향키로 플레이어 이동
    private static void Input()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.LeftArrow && playerX > 1)
            {
                playerX--;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow && playerX < width - 2)
            {
                playerX++;
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                isRunning = false;
            }
        }
    }

    // 2. 업데이트 기능: 별 생성, 이동, 충돌 판정
    private static void Update()
    {
        // 약 30% 확률로 화면 맨 위(y=1)에 새로운 별 생성
        if (rand.Next(0, 10) < 3)
        {
            stars.Add((rand.Next(1, width - 1), 1));
        }

        // 생성된 별들을 한 칸씩 아래로 이동
        for (int i = stars.Count - 1; i >= 0; i--)
        {
            int nextY = stars[i].y + 1;

            // 플레이어와 별의 좌표가 겹치면 게임 오버
            if (stars[i].x == playerX && nextY == playerY)
            {
                isRunning = false;
                return;
            }

            // 바닥에 닿으면 별을 제거하고 점수 획득
            if (nextY >= height - 1)
            {
                stars.RemoveAt(i);
                score += 10;
            }
            else
            {
                stars[i] = (stars[i].x, nextY);
            }
        }
    }

    // 3. 렌더링 기능: 테두리, 플레이어, 별, 점수 출력
    private static void Rendering()
    {
        // Console.Clear() 대신 커서 위치만 (0,0)으로 옮겨 화면 깜빡임 방지
        Console.SetCursorPosition(0, 0);

        // 화면 버퍼용 2차원 배열 생성
        char[,] screen = new char[height, width];

        // 빈 공간 및 테두리 채우기
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    screen[y, x] = '#'; // 벽
                else
                    screen[y, x] = ' '; // 빈 공간
            }
        }

        // 별 배치
        foreach (var star in stars)
        {
            screen[star.y, star.x] = '*';
        }

        // 플레이어 배치
        screen[playerY, playerX] = 'A';

        // 화면 출력
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(screen[y, x]);
            }
            Console.WriteLine();
        }

        Console.WriteLine($"점수: {score}  (조작: ← → / 종료: ESC)");
    }
}

    // Game 클래스. 시작 중. 게임 영역 크기, 플레이어 좌표, 몬스터좌표, 점수

    // Run()핵심. 게임이다.

