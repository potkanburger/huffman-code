using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huffman;

namespace HuffmanPlugin
{
    public class HuffmanPlugin : MarshalByRefObject, Huffman.IPlugin
    {
        private string _pluginName;

        public HuffmanPlugin(){
            _pluginName = "workPlease";
        }
        
        bool Huffman.IPlugin.Compress(ref Huffman.HuffmanData hf){
            byte[] test = new byte[12];
            List<KeyValuePair<byte, int>> ll = new List<KeyValuePair<byte, int>>();
            for (int i = 0; i < 12; i++)
            {
                test[i] = (byte)i;
                ll.Add(new KeyValuePair<byte,int>((byte) i, i));
            }

            hf.compressedData = test;
            hf.uncompressedData = test;
            hf.sizeOfUncompressedData = 12;
            hf.frequency = ll;
            return true;
        }

        bool Huffman.IPlugin.Decompress(ref Huffman.HuffmanData hf){
            byte[] test = new byte[12];
            List<KeyValuePair<byte, int>> ll = new List<KeyValuePair<byte, int>>();
            for (int i = 0; i < 12; i++)
            {
                test[i] = (byte)i;
                ll.Add(new KeyValuePair<byte, int>((byte)i, i));
            }

            hf.compressedData = test;
            hf.uncompressedData = test;
            hf.sizeOfUncompressedData = 12;
            hf.frequency = ll;
            return true;
        }

        string Huffman.IPlugin.PluginName{
            get{
                return "workPlease";
            }
        }

    }
}
