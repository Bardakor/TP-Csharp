using System;
using System.Collections.Generic;
using System.Linq;

namespace AsciiDot
{
    /**
     * <summary>
     *     A small function to iterate over value of an enum (not taken from stake overflow):
     *     https://stackoverflow.com/questions/972307/how-to-loop-through-all-enum-values-in-c
     * </summary>
     */
    public static class EnumUtil
    {
        // Yes it is a dark magic (no problem if you don't understand now)
        public static IEnumerable<T> GetValues<T>() =>
            Enum.GetValues(typeof(T)).Cast<T>();
    }

    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public static class DirUtils
    {
        /**
          * <summary>
          * check if 2 directions are on the same axes.
          * </summary>
          *
          * <example>
          * <code>
          *     Console.WriteLine(DirUtils.SameAxis(Direction.Up, Direction.Down));
          *     Console.WriteLine(DirUtils.SameAxis(Direction.Up, Direction.Left));
          * </code>
          * 
          * the given snippet give
          *
          * <code>
          *     True
          *     False
          * </code>
          * </example>
          *
          * <param name="d1">the first direction</param>
          * <param name="d2">the second direction</param>
          *
          * <returns>
          * True if <paramref name="d1"/> et <paramref name="d2"/> or the same axes. Otherwise False.
          * </returns>
          */
        public static bool SameAxis(Direction d1, Direction d2)
        {
          switch (d1,d2)
          {
            case(Direction.Up, Direction.Down):
            case(Direction.Down, Direction.Up):
            case(Direction.Right, Direction.Left):
            case(Direction.Left, Direction.Right):
              return true;
          default:
            return false;
          }
        }

        /**
          * <summary>
          * Rotate the given direction clockwise.
          * </summary>
          *
          * <example>
          * <code>
          *     Console.WriteLine(DirUtils.Rotate(Direction.Up));
          *     Console.WriteLine(DirUtils.Rotation(Direction.Left));
          * </code>
          * 
          * the given snippet give
          *
          * <code>
          *     Direction.Right
          *     Direction.Up
          * </code>
          * </example>
          *
          * <param name="direction">the direction to rotate</param>
          *
          * <returns>
          * The clockwise rotated direction <paramref name="direction"/>.
          * </returns>
          */
        public static Direction Rotate(Direction direction)
        {
          return (Direction) (((int) direction + 1) % 4);
        }

        /**
          * <summary>
          * Give the opposite direction of the given one.
          * </summary>
          *
          * <example>
          * <code>
          *     Console.WriteLine(DirUtils.Invert(Direction.Up));
          *     Console.WriteLine(DirUtils.Invert(Direction.Left));
          * </code>
          * 
          * the given snippet give
          *
          * <code>
          *     Direction.Down
          *     Direction.Right
          * </code>
          * </example>
          *
          * <param name="direction">the direction to invert</param>
          *
          * <returns>
          * The opposite direction of <paramref name="direction"/>.
          * </returns>
          */
        public static Direction Invert(Direction direction)
        {
            return (Direction) (((int) direction + 2) % 4);
        }

        /**
          * <summary>
          * Give the X component of the direction.
          * </summary>
          *
          * <example>
          * <code>
          *     Console.WriteLine(DirUtils.DeltaX(Direction.Up));
          *     Console.WriteLine(DirUtils.DeltaX(Direction.Left));
          * </code>
          * 
          * the given snippet give
          *
          * <code>
          *     0
          *     -1
          * </code>
          * </example>
          *
          * <param name="direction">the direction whose component we must find</param>
          *
          * <returns>
          * The component of the direction.
          * </returns>
          */
        public static int DeltaX(Direction direction)
        {
            if(direction == Direction.Up || direction == Direction.Down) return 0;
            if(direction == Direction.Left) return (-1);
            return 1; //direction is Right
        }

        /**
          * <summary>
          * Give the Y component of the direction.
          * </summary>
          *
          * <example>
          * <code>
          *     Console.WriteLine(DirUtils.DeltaY(Direction.Up));
          *     Console.WriteLine(DirUtils.DeltaY(Direction.Left));
          * </code>
          * 
          * the given snippet give
          *
          * <code>
          *     1
          *     0
          * </code>
          * </example>
          *
          * <param name="direction">the direction whose component we must find</param>
          *
          * <returns>
          * The component of the direction.
          * </returns>
          */
        public static int DeltaY(Direction direction)
        {
            if(direction == Direction.Left || direction == Direction.Right) return 0;
            if(direction == Direction.Down) return (-1);
            return 1; //direction is Up
        }
    }
}