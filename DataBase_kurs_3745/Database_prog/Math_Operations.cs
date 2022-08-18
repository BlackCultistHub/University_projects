using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Database_prog
{
    static partial class Math_Operations
    {
        public static List<int> bubbleSort(List<int> target)
        {
            int temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i] > target[i + 1])
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}
