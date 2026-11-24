public class Poop
{
    private float _x;
    private float _y;
    private readonly float _vx;
    private readonly float _vy;
    public char Symbol { get; }

    public int GridX => (int)Math.Round(_x);
    public int GridY => (int)Math.Round(_y);

    private static readonly Random _rand = new();

    public Poop(float x, float y, float vx, float vy, char symbol = 'O')
    {
        _x = x;
        _y = y;
        _vx = vx;
        _vy = vy;
        Symbol = symbol;
    }

    public static Poop CreateRandom(int width, int height, int targetX, int targetY)
    {
        int edge = _rand.Next(4);
        float startX = 0, startY = 0;

        switch (edge)
        {
            case 0: startX = _rand.Next(0, width); startY = 0; break;
            case 1: startX = _rand.Next(0, width); startY = height - 1; break;
            case 2: startX = 0; startY = _rand.Next(0, height); break;
            case 3: startX = width - 1; startY = _rand.Next(0, height); break;
        }

        float speed = _rand.Next(9, 16);
        int pattern = _rand.Next(3);

        if (pattern == 0)
        {
            return edge switch
            {
                0 => new Poop(startX, startY, 0, speed, 'O'),
                1 => new Poop(startX, startY, 0, -speed, 'O'),
                2 => new Poop(startX, startY, speed, 0, 'O'),
                _ => new Poop(startX, startY, -speed, 0, 'O')
            };
        }
        else if (pattern == 1)
        {
            float dirX = targetX - startX;
            float dirY = targetY - startY;
            float length = MathF.Sqrt(dirX * dirX + dirY * dirY);

            if (length > 0.001f)
            {
                dirX /= length;
                dirY /= length;
            }

            return new Poop(startX, startY, dirX * speed, dirY * speed, 'O');
        }
        else
        {
            float diagVx = (startX < width / 2f ? 1 : -1) * speed * 0.707f;
            float diagVy = (startY < height / 2f ? 1 : -1) * speed * 0.707f;
            return new Poop(startX, startY, diagVx, diagVy, 'O');
        }
    }

    public void Update(float deltaTime)
    {
        _x += _vx * deltaTime;
        _y += _vy * deltaTime;
    }

    public bool IsOutOfBounds(int width, int height)
    {
        return GridX < 0 || GridX >= width || GridY < 0 || GridY >= height;
    }
}
