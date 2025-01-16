using System;
using System.Dynamic;
using System.Formats.Asn1;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello");
        static int AddTwoInts(int a, int b)
        {
            int answer = a + b;
            return answer;
        }
        int answer = AddTwoInts(5, 10);
        Console.WriteLine(answer);





        static void SpeedTest()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            long count = 0;
            for (long i = 0; i < 1_000_000_000L; i++)
            {
                count += i;
            }
            watch.Stop();
            System.Console.WriteLine(count);
        }
    }
}