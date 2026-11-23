public class DinosourGame
{
    // 화면 및 게임 설정 상수
    private const int ScreenWidth = 60;
    private const int ScreenHeight = 15;
    private const int GroundY = 12;
    private const int DinoX = 6;

    // 공룡 상태 변수
    private float dinoY;
    private float velocityY;
    private bool isJumping;
    private const float Gravity = 0.8f;
    private const float JumpForce = -3.2f;

    // 장애물 및 게임 진행 변수
    private List<int> obstacles;
    private Random random;
    private int spawnTimer;
    private int score;
    private bool isGameOver;
    private int gameSpeedMs;

    public DinosourGame()
    {
        Initialize();
    }

    // 게임 초기화
    private void Initialize()
    {
        Console.CursorVisible = false;
        dinoY = GroundY;
        velocityY = 0;
        isJumping = false;
        obstacles = new List<int>();
        random = new Random();
        spawnTimer = 0;
        score = 0;
        isGameOver = false;
        gameSpeedMs = 50; // 프레임 간격 (작을수록 빠름)
    }

    // 메인 게임 루프
    public void Run()
    {
        Console.Clear();

        while (!isGameOver)
        {
            Input();
            Update();
            Rendering();

            Thread.Sleep(gameSpeedMs);
        }

        ShowGameOver();
    }

    // 1. 사용자 입력 처리
    private void Input()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // 스페이스바 또는 위쪽 화살표로 점프
            if ((keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.UpArrow) && !isJumping)
            {
                isJumping = true;
                velocityY = JumpForce;
            }
        }
    }

    // 2. 게임 로직 및 물리 업데이트
    private void Update()
    {
        // 점프 및 중력 적용
        if (isJumping)
        {
            dinoY += velocityY;
            velocityY += Gravity;

            // 바닥에 착지했을 때
            if (dinoY >= GroundY)
            {
                dinoY = GroundY;
                isJumping = false;
                velocityY = 0;
            }
        }

        // 장애물 생성 타이머
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            obstacles.Add(ScreenWidth - 2);
            // 다음 장애물 생성까지 랜덤 간격 (최소 15프레임 ~ 최대 35프레임)
            spawnTimer = random.Next(15, 35);
        }

        // 장애물 이동 및 충돌 판정
        for (int i = obstacles.Count - 1; i >= 0; i--)
        {
            obstacles[i]--;

            // 충돌 체크 (공룡의 X좌표와 장애물 X좌표가 겹치고, 공룡이 바닥 근처에 있을 때)
            if (obstacles[i] == DinoX && Math.Round(dinoY) >= GroundY - 1)
            {
                isGameOver = true;
            }

            // 화면 왼쪽 끝을 벗어난 장애물 제거 및 점수 증가
            if (obstacles[i] < 0)
            {
                obstacles.RemoveAt(i);
                score += 10;

                // 점수가 오를수록 게임 속도 약간씩 증가
                if (score % 100 == 0 && gameSpeedMs > 20)
                {
                    gameSpeedMs -= 2;
                }
            }
        }
    }

    // 3. 화면 그리기 (더블 버퍼링 방식의 문자열 조합으로 깜빡임 최소화)
    private void Rendering()
    {
        char[,] screenBuffer = new char[ScreenHeight, ScreenWidth];

        // 버퍼 공백으로 초기화
        for (int y = 0; y < ScreenHeight; y++)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                screenBuffer[y, x] = ' ';
            }
        }

        // 바닥 그리기
        for (int x = 0; x < ScreenWidth; x++)
        {
            screenBuffer[GroundY + 1, x] = '=';
        }

        // 공룡 그리기 (2칸 높이)
        int currentDinoY = (int)Math.Round(dinoY);
        if (currentDinoY >= 1 && currentDinoY < ScreenHeight)
        {
            screenBuffer[currentDinoY - 1, DinoX] = 'O'; // 머리
            screenBuffer[currentDinoY, DinoX] = 'M';     // 몸통
        }

        // 장애물(선인장) 그리기
        foreach (int obsX in obstacles)
        {
            if (obsX >= 0 && obsX < ScreenWidth)
            {
                screenBuffer[GroundY, obsX] = '#';
            }
        }

        // 커서를 좌측 상단으로 이동 후 한 번에 출력
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"SCORE: {score:D5}   [Jump: Space / Up Arrow]");

        for (int y = 0; y < ScreenHeight; y++)
        {
            char[] row = new char[ScreenWidth];
            for (int x = 0; x < ScreenWidth; x++)
            {
                row[x] = screenBuffer[y, x];
            }
            Console.WriteLine(new string(row));
        }
    }

    // 게임 오버 화면 및 재시작 처리
    private void ShowGameOver()
    {
        Console.SetCursorPosition(ScreenWidth / 2 - 5, ScreenHeight / 2);
        Console.WriteLine("GAME OVER!");
        Console.SetCursorPosition(ScreenWidth / 2 - 11, ScreenHeight / 2 + 1);
        Console.WriteLine("Press 'R' to Restart");

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.R)
            {
                Initialize();
                Run();
                break;
            }
            else if (key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }
}