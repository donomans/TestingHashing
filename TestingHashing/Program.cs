using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingHashing
{
    class Program
    {
        const String UsableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+-=:"";',./<>?";


        static void Main(string[] args)
        {
            ///Create 3 arrays of varying size
            
            RunHash(HashType.FoundOnline1);
            RunHash(HashType.GetHashCode);
            RunHash(HashType.MD5);
            RunHash(HashType.RemoteMon);
            RunHash(HashType.SHA1);
            RunHash(HashType.SHA256);
            RunHash(HashType.SHA512);
            
            Console.ReadLine();
        }

        private static void RunHash(HashType hashtype)
        {
            Random r = new Random();
            DateTime starttime = DateTime.Now;

            String[] SmallArray = new String[5500];
            String[] MediumArray = new String[60000];
            String[] LargeArray = new String[500000];
            String[] SuperArray = new String[20000000];
            ///fill them with junk data
            SmallArray.FillArray();
            MediumArray.FillArray();
            LargeArray.FillArray();
            SuperArray.FillArray();

            Hasher hasher = new Hasher();

            
            Dictionary<Int32, Int32> smallarraydistribution =
                hasher.Hash(SmallArray, hashtype).Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            Dictionary<Int32, Int32> mediumarraydistribution =
                hasher.Hash(MediumArray, hashtype).Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            Dictionary<Int32, Int32> largearraydistribution =
                hasher.Hash(LargeArray, hashtype).Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            Dictionary<Int32, Int32> superarraydistribution =
                hasher.Hash(SuperArray, hashtype).Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            Int32 smallcollisions = smallarraydistribution.Count(d => d.Value > 1);
            Int32 mediumcollisions = mediumarraydistribution.Count(d => d.Value > 1);
            Int32 largecollisions = largearraydistribution.Count(d => d.Value > 1);
            Int32 supercollisions = superarraydistribution.Count(d => d.Value > 1);
            Console.WriteLine("Hash type: " + hashtype.ToString());
            Console.WriteLine("Small Array[" + SmallArray.Length + "] Collisions: " + smallcollisions);
            Console.WriteLine("Medium Array[" + MediumArray.Length + "] Collisions: " + mediumcollisions);
            Console.WriteLine("Large Array[" + LargeArray.Length + "] Collisions: " + largecollisions);
            Console.WriteLine("Super Array[" + SuperArray.Length + "] Collisions: " + supercollisions);
            Console.WriteLine("Timer: " + (DateTime.Now - starttime).TotalSeconds);
            Console.WriteLine();
            Console.ReadLine();
        }

    }
}
