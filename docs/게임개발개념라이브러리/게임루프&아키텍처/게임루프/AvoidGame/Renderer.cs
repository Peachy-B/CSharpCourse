using System.Text;

public class Renderer
{
    private readonly int _width;
    private readonly int _height;
    private readonly char[,] _buffer;

    public Renderer(int width, int height)
    {
        _width = width;
        _height = height;
        _buffer = new char[height, width];
    }

    public void ClearBuffer()
    {
        for (int y = 0; y < _height; y++)
            for (int x = 0; x < _width; x++)
                _buffer[y, x] = ' ';
    }

    public void Draw(int x, int y, char symbol)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            _buffer[y, x] = symbol;
        }
    }

    public void Present(float survivalTime)
    {
        Console.SetCursorPosition(0, 0);
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"생존 시간: {survivalTime:F1}초 | 이동: WASD | 종료: ESC");
        sb.AppendLine(new string('=', _width + 2));

        for (int y = 0; y < _height; y++)
        {
            sb.Append('|');
            for (int x = 0; x < _width; x++)
            {
                sb.Append(_buffer[y, x]);
            }
            sb.AppendLine("|");
        }

        sb.AppendLine(new string('=', _width + 2));
        Console.Write(sb.ToString());
    }

    public void DrawGameOverPanel(float survivalTime)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("==============================================");
        sb.AppendLine("                 GAME OVER!                   ");
        sb.AppendLine($"           최종 생존 시간: {survivalTime,6:F2}초           ");
        sb.AppendLine("     (충돌 지점 'X' 확인 후 2초 뒤 종료)      ");
        sb.AppendLine("==============================================");
        Console.Write(sb.ToString());
        Console.ResetColor();
    }
}