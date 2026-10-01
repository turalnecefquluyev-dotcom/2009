using System;
using System.Diagnostics;
using System.Threading;

namespace SystemProgramming
{
    class Program
    {
        static Thread thread1;
        static Thread thread2;
        static Thread thread3;

        static Process file1Process;
        static Process chromeProcess;

        static bool exitProgram = false;

        static void StartFile1()
        {
            if (file1Process == null || file1Process.HasExited)
            {
                thread1 = new Thread(StartFile1Process);
                thread1.Start();
            }
            else
            {
                Console.WriteLine("File 1 artiq isleyir.");
            }
        }

        static void StartFile1Process()
        {
            file1Process = Process.Start("notepad.exe");
            Console.WriteLine("File 1 basladildi.");
        }

        static void StopFile1()
        {
            if (file1Process != null && !file1Process.HasExited)
            {
                file1Process.Kill();
                file1Process = null;

                Console.WriteLine("File 1 dayandirildi.");
            }
            else
            {
                Console.WriteLine("File 1 islemir.");
            }
        }

        static void StartChrome()
        {
            if (chromeProcess == null || chromeProcess.HasExited)
            {
                thread2 = new Thread(StartChromeProcess);
                thread2.Start();
            }
            else
            {
                Console.WriteLine("Chrome artiq isleyir.");
            }
        }

        static void StartChromeProcess()
        {
            chromeProcess = Process.Start("chrome.exe");
            Console.WriteLine("Chrome basladildi.");
        }

        static void StopChrome()
        {
            if (chromeProcess != null && !chromeProcess.HasExited)
            {
                chromeProcess.Kill();
                chromeProcess = null;

                Console.WriteLine("Chrome dayandirildi.");
            }
            else
            {
                Console.WriteLine("Chrome islemir.");
            }
        }

        static void CloseAll()
        {
            StopFile1();
            StopChrome();

            Console.WriteLine("Butun processler baglandi.");
        }

        static void Menu()
        {
            while (!exitProgram)
            {
                Console.Clear();

                Console.WriteLine("========== MENU ==========");
                Console.WriteLine("1  - File 1 Start");
                Console.WriteLine("2  - Chrome Start");
                Console.WriteLine("s-1 - File 1 Stop");
                Console.WriteLine("s-2 - Chrome Stop");
                Console.WriteLine("close all - Butun processleri bagla");
                Console.WriteLine("exit - Proqramdan cix");
                Console.WriteLine("==========================");

                Console.Write("Secim: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    StartFile1();
                }
                else if (choice == "2")
                {
                    StartChrome();
                }
                else if (choice == "s-1")
                {
                    StopFile1();
                }
                else if (choice == "s-2")
                {
                    StopChrome();
                }
                else if (choice == "close all")
                {
                    CloseAll();
                }
                else if (choice == "exit")
                {
                    CloseAll();

                    exitProgram = true;

                    Console.WriteLine("Proqramdan cixilir...");
                }
                else
                {
                    Console.WriteLine("Yanlis secim.");
                }

                if (!exitProgram)
                {
                    Console.WriteLine();
                    Console.WriteLine("Davam etmek ucun Enter basin...");
                    Console.ReadLine();
                }
            }
        }

        static void StartProgram()
        {
            thread3 = new Thread(Menu);
            thread3.Start();

            thread3.Join();
        }

        static void Main(string[] args)
        {
            StartProgram();
        }
    }
}