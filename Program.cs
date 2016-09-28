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
            Console.Read();
        }
    }
}
