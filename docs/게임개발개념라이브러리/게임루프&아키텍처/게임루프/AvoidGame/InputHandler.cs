using System.Runtime.InteropServices;

public class InputHandler
{
    public event Action<float, float>? OnMoveAxis;
    public event Action? OnExitRequested;

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    private const int VK_W = 0x57;
    private const int VK_A = 0x41;
    private const int VK_S = 0x53;
    private const int VK_D = 0x44;
    private const int VK_ESCAPE = 0x1B;

    private static bool IsKeyDown(int vKey) => (GetAsyncKeyState(vKey) & 0x8000) != 0;

    public void PollEvents()
    {
        if (IsKeyDown(VK_ESCAPE))
        {
            OnExitRequested?.Invoke();
            return;
        }

        float dx = 0f;
        float dy = 0f;

        if (IsKeyDown(VK_W)) dy -= 1f;
        if (IsKeyDown(VK_S)) dy += 1f;
        if (IsKeyDown(VK_A)) dx -= 1f;
        if (IsKeyDown(VK_D)) dx += 1f;

        if (dx != 0f && dy != 0f)
        {
            const float invSqrt2 = 0.70710678f;
            dx *= invSqrt2;
            dy *= invSqrt2;
        }

        OnMoveAxis?.Invoke(dx, dy);
    }
}
