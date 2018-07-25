using System;
using System.Security.Cryptography;

namespace GenerateSecretKey
{
    class Program
    {
        static void Main(string[] args)
        {
            var secret = Convert.ToBase64String(new HMACSHA256().Key);
            Console.WriteLine(secret);
            Console.ReadKey();
        }
    }
}
