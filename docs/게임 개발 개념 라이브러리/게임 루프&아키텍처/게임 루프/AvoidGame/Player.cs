public class Player
{
    private float _x;
    private float _y;
    private float _dirX;
    private float _dirY;

    private readonly int _maxWidth;
    private readonly int _maxHeight;
    private const float Speed = 18f;

    public int GridX => (int)Math.Round(_x);
    public int GridY => (int)Math.Round(_y);

    public Player(float startX, float startY, int maxWidth, int maxHeight)
    {
        _x = startX;
        _y = startY;
        _maxWidth = maxWidth;
        _maxHeight = maxHeight;
    }

    public void SetDirection(float dx, float dy)
    {
        _dirX = dx;
        _dirY = dy;
    }

    public void Update(float deltaTime)
    {
        if (_dirX == 0f && _dirY == 0f) return;

        _x = Math.Clamp(_x + _dirX * Speed * deltaTime, 0, _maxWidth - 1);
        _y = Math.Clamp(_y + _dirY * (Speed * 0.6f) * deltaTime, 0, _maxHeight - 1);
    }
}
