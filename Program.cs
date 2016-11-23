using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanComp
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"Dosya.bmp");

            HuffmanTree huffmanTree = new HuffmanTree();

            // Build the Huffman tree
            huffmanTree.Build(input);

            // Encode
            BitArray encoded = huffmanTree.Encode(input);


            var compressedBytes = BitArrayToByteArray(encoded);


            File.WriteAllBytes(@"Dosya.bmp.huff", compressedBytes);

        }

        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            List<byte> ret = new List<byte>();
            int count = 0;
            byte currentByte = 0;

            var lenght = bits.Length / 8 + (bits.Length % 8 > 0 ? 1 : 0);

            foreach (bool b in bits)
            {
                if (b) currentByte |= (byte)(1 << count);
                count++;
                if (count == 7)
                {
                    ret.Add(currentByte); currentByte = 0; count = 0;
                    Console.WriteLine("{0} / {1} processed", count, lenght);
                }

            }

            if (count < 7) ret.Add(currentByte);

            return ret.ToArray();
        }
    }
}
