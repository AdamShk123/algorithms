namespace newton
{
    using System;
    
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const double num = 4.0;
            const double initial = 1.0;
            
            var result = NewtonMethod(num, initial);
            
            Console.ForegroundColor = ConsoleColor.Red;

            if (result.HasValue)
            {
                Console.WriteLine("The square root of {0} according to the NewtonMethod function is approximately {1:G3}.", num ,result.Value);   
                
                var builtin = Math.Sqrt(num);
                Console.WriteLine("The square root of {0} according to the Math.Sqrt function is approximately {1:G3}.", num, builtin);

                var diff = Math.Abs(result.Value - builtin);
                Console.WriteLine("The difference between results is {0}", diff);
            }
            else
            {
                Console.WriteLine("The square root of {0} can't be calculated!", num);
            }
        }
        
        private static double? NewtonMethod(double num, double initial, int maxIterations=1000, double within = 1e-10)
        {
            switch (num)
            {
                case < 0.0:
                    return null;
                case 0.0:
                    return 0.0;
            }

            int counter = 0;
            var current = initial;
            var converge = double.MaxValue;
            
            while (counter < maxIterations && converge > within)
            {
                current = current - Function(num, current) / Prime(current);
                converge = Math.Abs(num - Math.Pow(current, 2));
                counter += 1;
            }

            return current;
        }

        private static double Function(double num, double current)
        {
            return Math.Pow(current, 2) - num;
        }
        
        private static double Prime(double current)
        {
            return 2 * current;
        }
    }
}