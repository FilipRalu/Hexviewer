using System;
using System.Collections.Generic;
using System.IO;

namespace Utilitati
{
    public static class Cititor
    {
        public static byte[] FileAsByteArray(string path)
        {
            byte[] file;
            try
            {
                file = File.ReadAllBytes(path);
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Calea introdusa este mult prea lunga!");
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Calea introdusa este invalida!");
                return null;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Calea introdusa este invalida!");
                return null;
            }
            catch (IOException)
            {
                Console.WriteLine("Nu s-a putut accesa fisierul ales!");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("A esuat incercarea accesarii fisierului!");
                return null;
            }

            return file;
        }

        public static string ReadFilePath()
        {
            Console.WriteLine("Introduceti calea fisierului pentru a fi citit:");
            string path;

            while (true)
            {
                path = Console.ReadLine();
                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }

                var invalidChars = Path.GetInvalidPathChars();
                var invalidContaining = new HashSet<char>();

                foreach (var invalidChar in invalidChars)
                {
                    if (path.Contains(invalidChar.ToString()))
                    {
                        invalidContaining.Add(invalidChar);
                    }
                }

                if (invalidContaining.Count != 0)
                {
                    Console.WriteLine($"Calea aleasa contine caractere invalide: {string.Join(", ", invalidContaining)}");
                    continue;
                }

                break;
            }

            return path;
        }
    }
}