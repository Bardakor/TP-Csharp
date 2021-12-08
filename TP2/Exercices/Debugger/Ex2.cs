namespace Debugger
{
    public class Ex2
    {
        public static int Exo2() //FIXME
        {
            int[] array = {1, 2, 3, 4, 5, 6, 7, 8, 9};
            int res = 0;
            for (int i = (int) Misc.GetLength(array) - 1; i != -1; --i)
                res += array[i];
            return res;
        }
    }
}