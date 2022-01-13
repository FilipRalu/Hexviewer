using System;
using Utilitati;

namespace Hexviewer
{
    internal static class Program
    {
        public static void Main()
        {
            var file = Cititor.FileAsByteArray(Cititor.ReadFilePath());

            while (file == null)
            {
                file = Cititor.FileAsByteArray(Cititor.ReadFilePath());
            }

            Console.WriteLine("Cati octeti vreti sa fie pe o linie?");
            var bytesPerLine = Nrpoz.NextPositiveInt();

            var viewer = new Hexviewer(file);
            viewer.SetBytesPerLine(bytesPerLine);

            Console.WriteLine(viewer.Build());
        }
    }
}

