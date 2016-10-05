using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanPlugin
{
    class HuffmanCode
    {
        private Dictionary<byte, Tuple<byte, int>> _huffman;

        public HuffmanCode()
        {
            _huffman = new Dictionary<byte, Tuple<byte, int>>();
        }

        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<byte, Tuple<byte, int>> elm in _huffman)
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

        public Dictionary<byte, Tuple<byte, int>> getCode()
        {
            return _huffman;
        }

        public void setCode(Dictionary<byte, Tuple<byte, int>> huffman)
        {
            _huffman = huffman;
        }

        public byte[] concat(byte[] str)
        {
            byte[] concatenation = new byte[str.Length]; //la taille de la concaténation par code Huffman sera nécessairement inférieure à la taille de la string
            //Tuple<byte,int>[] res = new Tuple<byte, int>[str.Length]; 
            int count = 0;
            int curBits = 0;
            byte curByte = (byte) 0;
            byte tmp;
            foreach(byte c in str){                
                for(int i = _huffman[c].Item2-1; i >= 0; i--){
                    if(curBits>=8){
                        concatenation[count] = curByte;
                        count += 1;
                        curByte = (byte) 0;
                        curBits = 0;
                    }
                   
                    curByte = (byte) (curByte << 1);
                    curBits++;
                    tmp = (byte) ((_huffman[c].Item1 >> i) & 1);
                    curByte = (byte) (curByte | tmp);
                }
                
            }

            if (curBits > 0)
            {
                curByte = (byte)(curByte << (8 - curBits));           
                concatenation[count] = curByte;
                count+=1;
            }

            byte[] result = new byte[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = concatenation[i];
            }

            return result;

            /*
            int sizeBoolArray = (count-1)* 8 + curBits;
            bool[] boolTest = new bool[sizeBoolArray];
            byte loaded = (byte)0;
            bool b;
            for (int i = 0; i < count-1; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    loaded = (byte)(concatenation[i] >> (7 - j));
                    loaded = (byte)(loaded & 1);
                    if (loaded.Equals(0))
                        b = false;
                    else
                        b = true;
         
                    boolTest[i * 8 + j] = b;
                }
            }

            for (int j = 0; j < curBits; j++)
            {
                loaded = (byte)(concatenation[count-1] >> (7 - j));
                loaded = (byte)(loaded & 1);
                if (loaded.Equals(0))
                    b = false;
                else
                    b = true;

                boolTest[(count-1)* 8 + j] = b;
            }

            return boolTest;
            */
        }

        public byte[] decompress(byte[] concat, int dataLength)
        {
            byte[] s = new byte[dataLength];
            bool b;
            Dictionary<int, Dictionary<byte, byte>> decompressDico = new Dictionary<int, Dictionary<byte, byte>>();
            foreach (byte c in _huffman.Keys)
            {
                if (!decompressDico.ContainsKey(_huffman[c].Item2))
                {
                    decompressDico.Add(_huffman[c].Item2, new Dictionary<byte, byte>());
                }
                decompressDico[_huffman[c].Item2].Add(_huffman[c].Item1, c);           
            }

            bool[] boolTest = new bool[concat.Length * 8];
            byte loaded = (byte)0;
            for (int i = 0; i < concat.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    loaded = (byte) (concat[i] >> (7 - j));
                    loaded = (byte)(loaded & 1);
                    if(loaded.Equals(0)){
                        b = false;
                    }else{
                        b = true;
                    }
                    boolTest[i * 8 + j] = b;
                }   
            }
            int countBits = 0;
            byte testByte = (byte)0;
            int decryptedValues = 0;

            try
            {
                 for (int i = 0; i < concat.Length*8; i++)
                 {
                    /*s[decryptedValues] = (byte)'a';
                    decryptedValues += 1;
                    if (decryptedValues >= dataLength)
                    {
                        break;
                    }*/

                    
                    testByte = (byte)(testByte << 1);
                    if (boolTest[i])
                        testByte = (byte)(testByte | 1);
                    else
                        testByte = (byte)(testByte & ~1);
                    
                    countBits++;

                    if (decompressDico.ContainsKey(countBits))
                    {
                        if (decompressDico[countBits].ContainsKey(testByte))
                        {
                            s[decryptedValues] = decompressDico[countBits][testByte];
                            decryptedValues += 1;
                            countBits = 0;
                            testByte = (byte)0;
                            if(decryptedValues>=dataLength){
                                break;
                            }
                        }
                    }

                    /*if (countBits >= 8)
                    {
                        countBits = 0;
                        testByte = (byte)0;
                        throw new SystemException("Decompression failed");
                    }*/

                }
            }
            catch(SystemException e)
            {
                Console.Write(e.ToString());
            }

                 
            return s;
            
        }
    }
}
