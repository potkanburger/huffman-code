using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            TableFq t = new TableFq();
            KeyValuePair<char, int>[] tableau = { new KeyValuePair<char, int>('A', 10) , new KeyValuePair<char, int>('B', 10), new KeyValuePair<char, int>('C', 25), new KeyValuePair<char, int>('D', 16), new KeyValuePair<char, int>('E', 36), new KeyValuePair<char, int>('F', 6) };
            t.fillTable(tableau);

            BinaryTree bnTree = new BinaryTree();
            bnTree.feedTree(t);
            bnTree.computeTree();

            HuffmanCode hf = new HuffmanCode();
            hf = bnTree.createHuffman();
            Console.Write(bnTree.ToString());
            Console.Write("\n\n\n");
            Console.Write(hf.ToString());

            string testString = "";
            Console.Write("\nEnter a string to test: ");
            ConsoleKeyInfo c = Console.ReadKey();
            while (c.Key != ConsoleKey.Enter)
            {
                testString += c.KeyChar;
                c = Console.ReadKey();
            }
            Console.Write("\n String Length: " + testString.Length+"\n"+testString+"\n");
            TableFq testTable = new TableFq(testString);
            bnTree.feedTree(testTable);
            bnTree.computeTree();
            HuffmanCode testHf = new HuffmanCode();
            testHf = bnTree.createHuffman();
            Console.Write(bnTree.ToString());
            Console.Write("\n\n\n");
            Console.Write(testHf.ToString());

            bool[] b = testHf.concat(testString);
            string res = testHf.decompress(b);
            Console.Write("\n"+res);
            Console.Read();

        }
    }
}
