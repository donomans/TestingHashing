using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestingHashing
{
    public class Hasher
    {
        public IEnumerable<Hash> Hash(IEnumerable<Object> tohash, HashType type)
        {
            switch (type)
            {
                case HashType.RemoteMon: ///used in RemoteMon
                    List<Hash> junkstring1hashes = new List<Hash>();
                    foreach (var a in tohash)
                        junkstring1hashes.Add(new Hash(UnsafeJunkString1Hash(a), a, type));
                    return junkstring1hashes;

                case HashType.FNV: ///found in hash table tester app
                    return tohash.JunkHasher(a =>
                    {
                        String s = a.ToString();
                        Int64 hash = 2166136261;
		                foreach (char c in s)
	                        hash = (16777619 * hash) ^ (Int32)c;

                        return new Hash((Int32)hash, a, type);
                    });
                case HashType.GetHashCode:
                    return tohash.JunkHasher(a =>
                    {
                        return new Hash(a.GetHashCode(), a, type);
                    });
                case HashType.MD5:
                    return tohash.JunkHasher(a =>
                    {
                        return new Hash(BitConverter.ToInt32(
                            HashAlgorithm.Create("MD5").ComputeHash(a.ToString().Select(s =>
                                {
                                    Byte[] b = new Byte[s.Length];
                                    Int32 i = 0;
                                    foreach (char c in s)
                                        b[i++] = (Byte)c;
                                    return b;
                                })),
                                0), a, type);
                    });
                case HashType.SHA1:
                    return tohash.JunkHasher(a =>
                    {
                        return new Hash(BitConverter.ToInt32(
                            HashAlgorithm.Create("SHA1").ComputeHash(a.ToString().Select(s =>
                            {
                                Byte[] b = new Byte[s.Length];
                                Int32 i = 0;
                                foreach (char c in s)
                                    b[i++] = (Byte)c;
                                return b;
                            })),
                                0), a, type);
                    });
                case HashType.SHA256:
                    return tohash.JunkHasher(a =>
                    {
                        return new Hash(BitConverter.ToInt32(
                             HashAlgorithm.Create("SHA256").ComputeHash(a.ToString().Select(s =>
                             {
                                 Byte[] b = new Byte[s.Length];
                                 Int32 i = 0;
                                 foreach (char c in s)
                                     b[i++] = (Byte)c;
                                 return b;
                             })),
                                 0), a, type); 
                    });
                case HashType.SHA512:
                    return tohash.JunkHasher(a =>
                    {
                        return new Hash(BitConverter.ToInt32(
                            HashAlgorithm.Create("SHA512").ComputeHash(a.ToString().Select(s =>
                            {
                                Byte[] b = new Byte[s.Length];
                                Int32 i = 0;
                                foreach (char c in s)
                                    b[i++] = (Byte)c;
                                return b;
                            })),
                                0), a, type);
                    });
                case HashType.RemoteMonModified:
                    List<Hash> junkstring2hashes = new List<Hash>();
                    foreach (var a in tohash)
                        junkstring2hashes.Add(new Hash(UnsafeJunkString2Hash(a), a, type));
                    return junkstring2hashes;
                
                default:
                    return null;
            }
            
        }

        public async Task FillBag(ConcurrentBag<String> source, Int32 length)
        {
            Random r = new Random();
            String UsableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+-=:"";',./<>?";

            for (Int32 count = 0; count < length; count++)
            {
                Char[] chars = new Char[r.Next(5, 50)];
                for (Int16 i = 0; i < chars.Length; i++)
                {
                    chars[i] = UsableChars[r.Next(0, UsableChars.Length)];
                }
                source.Add(new String(chars));
            }
        }


        private unsafe Int32 UnsafeJunkString1Hash(Object a)
        {
            String s = a.ToString();
            fixed (Char* str = s.ToCharArray())
            {
                Char* chPtr = str;
                Int32 x = 0x34058547;
                Int32 y = x;
                Int32* intPtr = (Int32*)chPtr;
                for (Int32 i = s.Length; i > 0; i -= 4)
                {
                    x = (((x << 5) + x) + (x >> 0x1B)) ^ intPtr[0];
                    if (i <= 2)
                    {
                        break;
                    }
                    y = (((y << 5) + y) + (y >> 0x1B)) ^ intPtr[1];
                    intPtr += 2;
                }
                return (x + (y * 0x214BD12C));
            }
        }

        private unsafe Int32 UnsafeJunkString2Hash(Object a)
        {
            String s = a.ToString();
            fixed (Char* str = s.ToCharArray())
            {
                Char* chPtr = str;
                Int32 x = 872777033;
                Int32 y = x;
                Int32* intPtr = (Int32*)chPtr;
                for (Int32 i = s.Length; i > 0; i -= 4)
                {
                    x = (((x << 3) + x) + (x >> 0x1B)) ^ intPtr[0];
                    if (i <= 2)
                    {
                        break;
                    }
                    y = (((y << 7) + y) + (y >> 0x17)) ^ intPtr[1];
                    intPtr += 2;
                }
                return (x + (y * 558616127));
            }
        }

      
    }

    public static class HasherExtension
    {
        public static IEnumerable<TResult> JunkHasher<T, TResult>(this IEnumerable<T> source, Func<T, TResult> hasher)
        {
            List<TResult> arr = new List<TResult>();
            foreach (var item in source)
                arr.Add(hasher(item));

            return arr;
        }

        public static TResult Select<TResult>(this String source, Func<String, TResult> transform)
        {
            return transform(source);
        }

        public static void FillArray<T>(this T[] source, Func<T> fillarray)
        {
            for (Int32 i = 0; i < source.Length; i++)
                source[i] = fillarray();
        }



        public static void FillBag(this ConcurrentBag<String> source, Int32 length)
        {
            Random r = new Random();
            String UsableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+-=:"";',./<>?";

            for (Int32 count = 0; count < length; count++)
            {                
                Char[] chars = new Char[r.Next(5, 50)];
                for (Int16 i = 0; i < chars.Length; i++)
                {
                    chars[i] = UsableChars[r.Next(0, UsableChars.Length)];
                }
                source.Add(new String(chars));
            }
        }

        public static void FillArray(this String[] source)
        {
            Random r = new Random();
            String UsableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+-=:"";',./<>?";

            for (Int32 count = 0; count < source.Length; count++)
            {
                Char[] chars = new Char[r.Next(20, 100)];
                for (Int16 i = 0; i < chars.Length; i++)
                {
                    chars[i] = UsableChars[r.Next(0, UsableChars.Length)];
                }
                source[count] =  new String(chars);
            }
        }

        public static void ParallelFillArray(this String[] source, Random r, String UsableChars)
        {            
            Parallel.For(0, source.Length, c =>
            {
                Char[] chars = new Char[r.Next(5, 50)];
                for (Int16 i = 0; i < chars.Length; i++)
                {
                    chars[i] = UsableChars[r.Next(0, UsableChars.Length)];
                }
                source[c] = new String(chars);
            });
        }

        public static Dictionary<T, R> Select<T, R, A>(this IEnumerable<A> source, Action<A, Dictionary<T,R>> transform)
        {
            Dictionary<T, R> dic = new Dictionary<T,R>();
            foreach (var h in source)
                transform(h, dic);

            return dic;
        }

        public static T[][] Select<T, A>(this IEnumerable<A> source, Int32 length, Action<A, T[][]> transform)
        {
            T[][] t = new T[length][];

            foreach (var h in source)
                transform(h, t);

            return t;
        }
        public static T[] Find<T>(this IEnumerable<T[]> source, T value, Func<T, Boolean> comparer)
        {
            foreach (var t in source)
                if (comparer(value))
                    return t;

            return null;
        }


        public static void Map<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }

    public struct Hash
    {
        public Hash(Int32 hash, Object root, HashType hashtype = HashType.None)
        {
            HashCode = hash;
            Original = root;
            Type = hashtype;
        }

        public Object Original;
        public HashType Type;
        public Int32 HashCode;
    }

    public enum HashType
    {
        None,
        RemoteMon,
        FNV,
        GetHashCode,
        MD5,
        SHA1,
        SHA256,
        SHA512,
        RemoteMonModified
    }
}
