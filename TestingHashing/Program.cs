using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingHashing
{
    class Program
    {
        const String UsableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+-=:"";',./<>?";
        static String[] SmallArray = new String[5500];
        static String[] MediumArray = new String[60000];
        static String[] LargeArray = new String[500000];
        static String[] SuperArray = new String[20000000];
        static Random r = new Random();
        


        static void Main(string[] args)
        {
            Console.WriteLine("Small Array[" + SmallArray.Length + "]");
            Console.WriteLine("Medium Array[" + MediumArray.Length + "]");
            Console.WriteLine("Large Array[" + LargeArray.Length + "]");
            Console.WriteLine("Super Array[" + SuperArray.Length + "]");
            ///fill them with junk data
            //Parallel.Invoke(
            //    () => SmallBag.FillBag(5500),
            //    () => MediumBag.FillBag(60000), 
            //    () => LargeBag.FillBag(500000),
            //    () => GiantBag.FillBag(20000000));

            DateTime singlefillstart = DateTime.Now;
            SmallArray.FillArray();
            MediumArray.FillArray();
            LargeArray.FillArray();
            SuperArray.FillArray();
            DateTime singlefillend = DateTime.Now;

            ///Threaded fills seem to end up with a lot of bogus randoms (like "AAAAAA")
            
            //DateTime threadfillstart = DateTime.Now;
            //Parallel.Invoke(
            //    () => SmallArray.FillArray(),
            //    () => MediumArray.FillArray(),
            //    () => LargeArray.FillArray(),
            //    () => SuperArray.FillArray());
            //DateTime threadfillend = DateTime.Now;

            //DateTime singlethreadedfillstart = DateTime.Now;
            //SmallArray.ParallelFillArray(r, UsableChars);
            //MediumArray.ParallelFillArray(r, UsableChars);
            //LargeArray.ParallelFillArray(r, UsableChars);
            //SuperArray.ParallelFillArray(r, UsableChars);
            //DateTime singlethreadedfillend = DateTime.Now;

            //DateTime fullythreadedfillstart = DateTime.Now;
            //Parallel.Invoke(
            //    () => SmallArray.ParallelFillArray(),
            //    () => MediumArray.ParallelFillArray(),
            //    () => LargeArray.ParallelFillArray(),
            //    () => SuperArray.ParallelFillArray());
            //DateTime fullythreadedfillend = DateTime.Now;

            Console.WriteLine("Fill Timer: " + (singlefillend - singlefillstart).TotalSeconds);
            //Console.WriteLine("Fill Threaded Timer: " + (threadfillend - threadfillstart).TotalSeconds);
            //Console.WriteLine("Fill Single / Threaded Timer: " + (singlethreadedfillend - singlethreadedfillstart).TotalSeconds);
            //Console.WriteLine("Fill Fully Threaded Timer: " + (fullythreadedfillend - fullythreadedfillstart).TotalSeconds);

            RunHash(HashType.FNV);
            RunHash(HashType.RemoteMon);
            RunHash(HashType.RemoteMonModified);
            RunHash(HashType.GetHashCode);
            RunHash(HashType.MD5);            
            RunHash(HashType.SHA1);
            RunHash(HashType.SHA256);
            RunHash(HashType.SHA512);
            
            Console.ReadLine();
        }

        private static void RunHash(HashType hashtype)
        {
            Console.WriteLine("Hash type: " + hashtype.ToString());
            //Random r = new Random();
            DateTime starttime = DateTime.Now;
            //ConcurrentBag<String> SmallBag = new ConcurrentBag<String>();
            //ConcurrentBag<String> MediumBag = new ConcurrentBag<String>();
            //ConcurrentBag<String> LargeBag = new ConcurrentBag<String>();
            //ConcurrentBag<String> GiantBag = new ConcurrentBag<String>();
                      
            Hasher hasher = new Hasher();

            #region slow also
            //ConcurrentBag<Int32> smallarrdisty = new ConcurrentBag<Int32>();
            //ConcurrentBag<Int32> mediumarrdisty = new ConcurrentBag<Int32>();
            //ConcurrentBag<Int32> largearrdisty = new ConcurrentBag<Int32>();
            //ConcurrentBag<Int32> giantarrdisty = new ConcurrentBag<Int32>();

            //Int32 smallarraycollisions = 0;
            //Int32 mediumarraycollisions = 0;
            //Int32 largearraycollisions = 0;
            //Int32 giantarraycollisions = 0;
            //IEnumerable<Hash> smallhash = null;
            //IEnumerable<Hash> mediumhash = null;
            //IEnumerable<Hash> largehash = null;
            //IEnumerable<Hash> gianthash = null;
            //Parallel.Invoke(
            //    () => smallhash = hasher.Hash(SmallBag, hashtype),
            //    () => mediumhash = hasher.Hash(MediumBag, hashtype),
            //    () => largehash = hasher.Hash(LargeBag, hashtype),
            //    () => gianthash = hasher.Hash(GiantBag, hashtype)
            //    );


            //Parallel.Invoke(() =>
            //    Parallel.ForEach(smallhash, h =>
            //    {
            //        if (smallarrdisty.Contains(h.HashCode))
            //            smallarraycollisions++;
            //        else
            //            smallarrdisty.Add(h.HashCode);

            //    }), () =>
            //    Parallel.ForEach(mediumhash, h =>
            //    {
            //        if (mediumarrdisty.Contains(h.HashCode))
            //            mediumarraycollisions++;
            //        else
            //            mediumarrdisty.Add(h.HashCode);

            //    }), () =>
            //    Parallel.ForEach(largehash, h =>
            //    {
            //        if (largearrdisty.Contains(h.HashCode))
            //            largearraycollisions++;
            //        else
            //            largearrdisty.Add(h.HashCode);

            //    }), () => 
            //    Parallel.ForEach(gianthash, h =>
            //    {
            //        if (giantarrdisty.Contains(h.HashCode))
            //            giantarraycollisions++;
            //        else
            //            giantarrdisty.Add(h.HashCode);

            //    }));
            ////() =>
            ////{
            ////    hasher.Hash(MediumBag, hashtype).Map(h =>
            ////    {
            ////        if (mediumarrdisty.Contains(h.HashCode))
            ////            mediumarraycollisions++;
            ////        else
            ////            mediumarrdisty.Add(h.HashCode);
            ////    });
            ////}, () =>
            ////{
            ////    hasher.Hash(LargeBag, hashtype).Map(h =>
            ////    {
            ////        if (largearrdisty.Contains(h.HashCode))
            ////            largearraycollisions++;
            ////        else
            ////            largearrdisty.Add(h.HashCode);
            ////    });
            ////}, () =>
            ////{
            ////    hasher.Hash(GiantBag, hashtype).Map(h =>
            ////    {
            ////        if (giantarrdisty.Contains(h.HashCode))
            ////            giantarraycollisions++;
            ////        else
            ////            giantarrdisty.Add(h.HashCode);
            ////    });
            ////});
            #endregion
            #region slow way
            //List<Int32> smallarrdisty = new List<Int32>(SmallArray.Length);
            //Int32 smallarraycollisions = 0;
            //hasher.Hash(SmallArray, hashtype).Map(h =>
            //    {
            //        if (smallarrdisty.Contains(h.HashCode))
            //            smallarraycollisions++;
            //        else
            //            smallarrdisty.Add(h.HashCode);
            //    });

            //List<Int32> mediumarrdisty = new List<Int32>(MediumArray.Length);
            //Int32 mediumarraycollisions = 0;
            //hasher.Hash(MediumArray, hashtype).AsParallel().Map(h =>
            //    {
            //        if (mediumarrdisty.Contains(h.HashCode))
            //            mediumarraycollisions++;
            //        else
            //            mediumarrdisty.Add(h.HashCode);
            //    });

            //List<Int32> largearrdisty = new List<Int32>(LargeArray.Length);
            //Int32 largearraycollisions = 0;
            //hasher.Hash(LargeArray, hashtype).AsParallel().Map(h =>
            //    {
            //        if (largearrdisty.Contains(h.HashCode))
            //            largearraycollisions++;
            //        else
            //            largearrdisty.Add(h.HashCode);
            //    });

            //List<Int32> superarrdisty = new List<Int32>(SuperArray.Length);
            //Int32 superarraycollisions = 0;
            //hasher.Hash(SuperArray, hashtype).AsParallel().Map(h =>
            //    {
            //        if (superarrdisty.Contains(h.HashCode))
            //            superarraycollisions++;
            //        else
            //            superarrdisty.Add(h.HashCode);
            //    });
            #endregion
            #region best way

           
            //ConcurrentDictionary<Int32, Int32> smallarraydistribution = new ConcurrentDictionary<int, int>(20, 5500);
            //ConcurrentDictionary<Int32, Int32> mediumarraydistribution = new ConcurrentDictionary<int, int>(20, 60000);
            //ConcurrentDictionary<Int32, Int32> largearraydistribution = new ConcurrentDictionary<int, int>(20, 500000);
            //ConcurrentDictionary<Int32, Int32> superarraydistribution = new ConcurrentDictionary<int, int>(20, 20000000);
            Dictionary<Int32, Int32> smallarraydistribution = new Dictionary<Int32, Int32>(5500);
            Dictionary<Int32, Int32> mediumarraydistribution = new Dictionary<Int32, Int32>(60000);
            Dictionary<Int32, Int32> largearraydistribution = new Dictionary<Int32, Int32>(500000);
            Dictionary<Int32, Int32> superarraydistribution = new Dictionary<Int32, Int32>(20000000);
            IEnumerable<Hash> smallhash = null;
            IEnumerable<Hash> mediumhash = null;
            IEnumerable<Hash> largehash = null;
            IEnumerable<Hash> superhash = null;
            DateTime starthash = DateTime.Now;
            Parallel.Invoke(
                () => smallhash = hasher.Hash(SmallArray, hashtype),
                () => mediumhash = hasher.Hash(MediumArray, hashtype),
                () => largehash = hasher.Hash(LargeArray, hashtype),
                () => superhash = hasher.Hash(SuperArray, hashtype)
                );
            DateTime endhash = DateTime.Now;
            
            DateTime startdist = DateTime.Now;
            #region seems threads get too swamped 
            //Parallel.ForEach(smallhash, h =>
            //    {
            //        if (smallarraydistribution.ContainsKey(h.HashCode))
            //            smallarraydistribution[h.HashCode] += 1;
            //        else
            //            smallarraydistribution.TryAdd(h.HashCode, 1);
            //    });

            //Parallel.ForEach(mediumhash, h =>
            //    {
            //        if (mediumarraydistribution.ContainsKey(h.HashCode))
            //            mediumarraydistribution[h.HashCode] += 1;
            //        else
            //            mediumarraydistribution.TryAdd(h.HashCode, 1);
            //    });


            //Parallel.ForEach(largehash, h =>
            //    {
            //        if (largearraydistribution.ContainsKey(h.HashCode))
            //            largearraydistribution[h.HashCode] += 1;
            //        else
            //            largearraydistribution.TryAdd(h.HashCode, 1);
            //    });

            //Parallel.ForEach(superhash, h =>
            //    {
            //        if (superarraydistribution.ContainsKey(h.HashCode))
            //        {
            //            Int32 old = superarraydistribution[h.HashCode];
            //            if(!superarraydistribution.TryUpdate(h.HashCode, old + 1, old))
            //                Console.WriteLine("Super Distribution failed to update: " + h.HashCode);
            //        }
            //        else
            //            if (!superarraydistribution.TryAdd(h.HashCode, 1))
            //                Console.WriteLine("Super Distribution failed to add: " + h.HashCode);
            //    });
            #endregion

            smallarraydistribution = smallhash.Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            mediumarraydistribution = mediumhash.Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            largearraydistribution = largehash.Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });

            superarraydistribution = superhash.Select<Int32, Int32, Hash>((h, d) =>
                {
                    if (d.ContainsKey(h.HashCode))
                        d[h.HashCode] += 1;
                    else
                        d.Add(h.HashCode, 1);
                });
            DateTime enddist = DateTime.Now;

            Int32 smalltotalcollisions = 0, mediumtotalcollisions = 0, largetotalcollisions= 0, supertotalcollisions = 0;
            Int32 smallcollisions = smallarraydistribution.Count(d =>{
                if (d.Value > 1)
                {
                    smalltotalcollisions += d.Value;
                    return true;
                }
                else
                    return false;
            });
            Int32 mediumcollisions = mediumarraydistribution.Count(d =>
            {
                if (d.Value > 1)
                {
                    mediumtotalcollisions += d.Value;
                    return true;
                }
                else
                    return false;
            });
            Int32 largecollisions = largearraydistribution.Count(d =>
            {
                if (d.Value > 1)
                {
                    largetotalcollisions += d.Value;
                    return true;
                }
                else
                    return false;
            });
            Int32 supercollisions = superarraydistribution.Count(d =>
            {
                if (d.Value > 1)
                {
                    supertotalcollisions += d.Value;
                    return true;
                }
                else
                    return false;
            });
            #endregion
            Console.WriteLine("Small Array Collisions: " + smallcollisions);
            Console.WriteLine("Small Array Total Collisions: " + smalltotalcollisions);
            Console.WriteLine("Medium Array Collisions: " + mediumcollisions);
            Console.WriteLine("Medium Array Total Collisions: " + mediumtotalcollisions);
            Console.WriteLine("Large Array Collisions: " + largecollisions);
            Console.WriteLine("Large Array Total Collisions: " + largetotalcollisions);
            Console.WriteLine("Super Array Collisions: " + supercollisions);
            Console.WriteLine("Super Array Total Collisions: " + supertotalcollisions);
            Console.WriteLine("Total Timer: " + (DateTime.Now - starttime).TotalSeconds);
            Console.WriteLine("Hash Timer: " + (endhash - starthash).TotalSeconds);
            Console.WriteLine("Distribution Timer: " + (enddist - startdist).TotalSeconds);
           
            Console.WriteLine();
        }

    }
}
