using System;

namespace Abacus
{
    public interface Interface
    {
        int TotalParameters
        {
            get;
        }

        double[] Parameters
        {
            get;
            set;
        }

        double Execute();
    }
    
    
    
    //Function class
    //make classes for (max,min,sqrt,abs,facto,isprime and gcd)
    public class max : Interface
    {

        public int TotalParameters
        {
            get { return 2; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            return Math.Max(Parameters[0], Parameters[1]);
        }
    }
    
    public class min : Interface
    {

        public int TotalParameters
        {
            get { return 2; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            return Math.Min(Parameters[0], Parameters[1]);
        }
    }
    
    public class sqrt : Interface
    {

        public int TotalParameters
        {
            get { return 1; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            return Math.Sqrt(Parameters[0]);
        }
    }
    
    public class abs : Interface
    {

        public int TotalParameters
        {
            get { return 1; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            return Math.Abs(Parameters[0]);
        }
    }
    
    public class facto : Interface
    {

        public int TotalParameters
        {
            get { return 1; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            double result = 1;
            for (int i = 1; i <= Parameters[0]; i++)
            {
                result *= i;
            }
            return result;
        }
    }
    
    public class isprime : Interface
    {

        public int TotalParameters
        {
            get { return 1; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            if (Parameters[0] == 1)
            {
                return 0;
            }
            else
            {
                for (int i = 2; i < Parameters[0]; i++)
                {
                    if (Parameters[0] % i == 0)
                    {
                        return 0;
                    }
                }
                return 1;
            }
        }
    }
    
    public class gcd : Interface
    {

        public int TotalParameters
        {
            get { return 2; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            int a = (int)Parameters[0];
            int b = (int)Parameters[1];
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
    
    //class for fibo
    public class fibo : Interface
    {

        public int TotalParameters
        {
            get { return 1; }
        }

        public double[] Parameters
        {
            get;
            set;
        }

        public double Execute()
        {
            //make internal method
            int n = (int)Parameters[0];
            int a = 0;
            int b = 1;
            int c = 0;
            for (int i = 0; i < n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }
            return c;
        }
    }
    
}