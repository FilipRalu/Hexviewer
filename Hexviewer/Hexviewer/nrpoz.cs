using System;

namespace Utilitati
{
    public static class Nrpoz
    {
        public static int NextPositiveInt()
        {
            var next = 0;
            var parsed = false;

            while (!parsed)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                parsed = int.TryParse(input, out next);

                if (parsed && next > 0)
                {
                    continue;
                }

                parsed = false;
                Console.WriteLine("Te rugam sa introduci o valoare de tip intreg pozitiva.");
            }

            return next;
        }
    }
}
