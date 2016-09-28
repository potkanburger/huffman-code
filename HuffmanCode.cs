using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class HuffmanCode
    {
        private Dictionary<char, Tuple<byte, int>> _huffman;

        public HuffmanCode()
        {
            _huffman = new Dictionary<char, Tuple<byte, int>>();
        }

        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<char, Tuple<byte, int>> elm in _huffman)
            {
                s += "[" + elm.Key.ToString() + ": ";
                byte tmp;
                for(int i= elm.Value.Item2-1; i>=0; i--)
                {
                    tmp = (byte) (elm.Value.Item1 >> i);
                    tmp = (byte) (tmp & 1); 
                    s += tmp.ToString();
                }
                s += "] ";
            }
            return s;
        }

        public Dictionary<char, Tuple<byte, int>> getCode()
        {
            return _huffman;
        }

        public void setCode(Dictionary<char, Tuple<byte, int>> huffman)
        {
            _huffman = huffman;
        }
    }
}
