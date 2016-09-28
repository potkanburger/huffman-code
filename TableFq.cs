using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class TableFq
    {
        Dictionary<char, int> _elements;

        public TableFq()
        {
            _elements = new Dictionary<char, int>();
        }


        public void fillTable(params KeyValuePair<char, int>[] valuePairs)
        {
            foreach (KeyValuePair<char, int> valuePair in valuePairs)
            {
                if(!_elements.Keys.Contains(valuePair.Key))
                _elements.Add(valuePair.Key, valuePair.Value);
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach(KeyValuePair<char, int> elm in _elements)
            {
                s += "["+elm.Key.ToString() +": "+ elm.Value.ToString()+"] ";
            }
            return s;
        }

        public Dictionary<char, int> getTable()
        {
            return _elements;
        }
    }
}
