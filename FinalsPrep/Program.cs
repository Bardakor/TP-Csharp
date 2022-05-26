using System;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Player
    {
        //attributes
        private string name;
        private int health;
        private int xp;
        
        //getters and setters
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        
        public int Xp
        {
            get { return xp; }
            set { xp = value; }
        }
        
        //constructor
        
        public Player(string name = "Player", int health = 100, int xp = 0)
        {
            this.name = name;
            this.health = health;
            this.xp = xp;
        }
        
    }

    public class Despise
    {
        //FIBONACCI
        public static long Fibonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int a = 0;
            int b = 0;

            while (n > 1)
            {
                int c = b;
                b += a;
                a = c;
                n -= 1;
            }

            return b;
        }

        //TRIBONACCI
        public static int Tribonacci(int n)
        {
            if (n == 0 || n == 1 || n == 2) return 0;
            if (n == 3) return 1;

            int a = 0;
            int b = 0;
            int c = 0;

            while (n > 3)
            {
                (int, int) d = (b, c);
                c += b + a;

                a = d.Item1;
                b = d.Item2;

                n--;
            }

            return c;
        }

        //FAIRE UNE ROTATION DE n AU CHAR c
        public static char RotChar(char c, int n)
        {
            if (c < 'A' || c > 'Z')
                throw new ArgumentException("Char is not in Ascii range");
            return (char)('A' + (((c - 65 +n) % 26) % 26) % 26);
        }

        //FAIRE UNE ROTATION DE n DE TOUTE LA PHRASE s 
        public static string RotSentence(string s, int n)
        {
            string result = "";
            foreach (var c in s)
            {
                result += RotChar(c, n);
            }

            return result;
        }

        //VICE MAX
        //RETOURNER LE DEUXIÈME PLUS GRAND ÉLÉMENT DE LA LIST
        // Si [1,2,2] --> 2
        // Si [1,2,3] --> 2
        public static int ViceMax(int[] list)
        {
            int len = list.Length;
            if (len == 0)
                throw new ArgumentException("Vicemax: Empty List");
            if (len == 1)
                return list[0];

            int max1 = list[0];
            int max2 = list[1];

            if (list[0] < list[1])
                (max1, max2) = (max2, max1);

            int i = 2;
            while (i < len)
            {
                if (list[i] > max1)
                {
                    max2 = max1;
                    max1 = list[i];
                }
                else if (list[i] > max2)
                    max2 = list[i];

                i++;
            }

            return max2;
        }

        public static int Nbonnaci(int n, int m)
        {
            //assuming m >= n:
            int[] a = new int[m];
            Array.Clear(a, 0, a.Length);
            a[n - 1] = 1;

            //computing every term as sum of previous terms
            for (int i = n; i < m; i++)
            {
                for (int j = i - n; j < i; j++)
                {
                    a[i] += a[j];
                }
            }

            return a[m - 1];
        }

        public static bool IsPrime(int n)
        {
            if (n == 0 || n == 1)
                return false;
            int i = 2;
            while (i * i < n)
            {
                if (n % i == 0)
                    return false;
                i++;
            }

            return true;
        }

        public static int KingOfTheHill(int[] arr)
        {
            var len = arr.Length;

            if (len == 0)
                return -1;

            var i = 0;

            while (i < len - 1 && arr[i] <= arr[i + 1])
                i++;

            int max = arr[i];

            while (i < len - 1 && arr[i] >= arr[i + 1])
                i++;
            
            return i == len - 1 ? max : -1;
        }
    }
}