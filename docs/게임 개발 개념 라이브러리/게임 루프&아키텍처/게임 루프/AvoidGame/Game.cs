using System.Diagnostics;

public class Game
{
    private const int Width = 44;
    private const int Height = 18;

    private bool _isRunning;
    private bool _isGameOver;
    private float _survivalTime;
    private float _spawnTimer;
    private float _spawnInterval = 0.35f;

    private readonly InputHandler _input;
    private readonly Player _player;
    private readonly List<Poop> _poops;
    private readonly Renderer _renderer;

    public Game()
    {
        _input = new InputHandler();
        _player = new Player(Width / 2f, Height / 2f, Width, Height);
        _poops = new List<Poop>();
        _renderer = new Renderer(Width, Height);

        _input.OnMoveAxis += (dx, dy) => _player.SetDirection(dx, dy);
        _input.OnExitRequested += () => _isRunning = false;
    }

    public static void Run()
    {
        var game = new Game();
        game.Start();
    }

    public void Start()
    {
        _isRunning = true;
        _isGameOver = false;
        Console.Clear();
        Console.CursorVisible = false;

        Stopwatch stopwatch = Stopwatch.StartNew();
        double previousTime = stopwatch.Elapsed.TotalSeconds;

        // 게임 루프: 입력 -> 갱신 -> 렌더링
        while (_isRunning)
        {
            double currentTime = stopwatch.Elapsed.TotalSeconds;
            float deltaTime = (float)(currentTime - previousTime);
            previousTime = currentTime;

            ProcessInput();
            Update(deltaTime);
            Render();

            Thread.Sleep(16);
        }

        if (_isGameOver)
        {
            ShowGameOverBelow();
        }
        else
        {
            Console.CursorVisible = true;
        }
    }

    private void ProcessInput()
    {
        _input.PollEvents();
    }

    private void Update(float deltaTime)
    {
        _survivalTime += deltaTime;
        _spawnTimer += deltaTime;

        _player.Update(deltaTime);

        _spawnInterval = Math.Max(0.12f, 0.35f - (_survivalTime * 0.008f));

        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0f;
            _poops.Add(Poop.CreateRandom(Width, Height, _player.GridX, _player.GridY));
        }

        for (int i = _poops.Count - 1; i >= 0; i--)
        {
            _poops[i].Update(deltaTime);

            // 충돌 판정
            if (_poops[i].GridX == _player.GridX && _poops[i].GridY == _player.GridY)
            {
                _isGameOver = true;
                _isRunning = false;
                return;
            }

            if (_poops[i].IsOutOfBounds(Width, Height))
            {
                _poops.RemoveAt(i);
            }
        }
    }

    private void Render()
    {
        _renderer.ClearBuffer();

        foreach (var poop in _poops)
        {
            _renderer.Draw(poop.GridX, poop.GridY, poop.Symbol);
        }

        char playerSymbol = _isGameOver ? 'X' : 'P';
        _renderer.Draw(_player.GridX, _player.GridY, playerSymbol);

        _renderer.Present(_survivalTime);
    }

    private void ShowGameOverBelow()
    {
        _renderer.DrawGameOverPanel(_survivalTime);
        Thread.Sleep(2000);

        while (Console.KeyAvailable) Console.ReadKey(true);
        Console.CursorVisible = true;
    }
}
