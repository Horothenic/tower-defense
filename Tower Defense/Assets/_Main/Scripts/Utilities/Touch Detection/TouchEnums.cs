namespace Utilities.TouchDetection
{
    public enum Direction
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,

        UpLeft = Up | Left,
        DownLeft = Down | Left,
        UpRight = Up | Right,
        DownRight = Down | Right
    }
}
