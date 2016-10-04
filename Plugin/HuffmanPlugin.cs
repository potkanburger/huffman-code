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

        HuffmanCode _hfCode;
        int _compressedSize;

        public HuffmanPlugin(){
            _hfCode = new HuffmanCode();
            _compressedSize = 0;
        }
        
        bool Huffman.IPlugin.Compress(ref Huffman.HuffmanData hf){
            try
            {
                TableFq tf = new TableFq(hf.uncompressedData);
                BinaryTree bnTree = new BinaryTree();
                bnTree.feedTree(tf);
                bnTree.computeTree();
                _hfCode = new HuffmanCode();
                _hfCode = bnTree.createHuffman();
                _compressedSize = hf.uncompressedData.Length;

                hf.sizeOfUncompressedData = hf.uncompressedData.Length;
                hf.frequency = tf.getTable().ToList();
                hf.compressedData = _hfCode.concat(hf.uncompressedData);
            }
            catch (SystemException e)
            {
                return false;
            }
            return true;
        }

        bool Huffman.IPlugin.Decompress(ref Huffman.HuffmanData hf){
            TableFq tf = new TableFq(hf.frequency);
            BinaryTree bnTree = new BinaryTree();
            bnTree.feedTree(tf);
            bnTree.computeTree();
            _hfCode = new HuffmanCode();
            _hfCode = bnTree.createHuffman();

            hf.uncompressedData = _hfCode.decompress(hf.compressedData, hf.sizeOfUncompressedData);
            //hf.sizeOfUncompressedData = hf.sizeOfUncompressedData;

            return true;
        }

        string Huffman.IPlugin.PluginName{
            get{
                return "GenouxHuTual_pluginJokariTresRose";
            }
        }

    }
}
