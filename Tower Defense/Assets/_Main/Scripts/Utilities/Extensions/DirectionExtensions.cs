using UnityEngine;

using Utilities.TouchDetection;

namespace Utilities.Extensions
{
    public static class DirectionExtensions
    {
        #region BEHAVIORS

        public static Vector2 ToVector2(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return Vector2.right;
                case Direction.UpRight:
                    return Vector2.up + Vector2.right;
                case Direction.Up:
                    return Vector2.up;
                case Direction.UpLeft:
                    return Vector2.up + Vector2.left;
                case Direction.Left:
                    return Vector2.left;
                case Direction.DownLeft:
                    return Vector2.down + Vector2.left;
                case Direction.Down:
                    return Vector2.down;
                case Direction.DownRight:
                    return Vector2.down + Vector2.right;
                default:
                    return Vector2.right;
            }
        }

        #endregion
    }
}