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

        public HuffmanPlugin(){  
        }
        
        bool Huffman.IPlugin.Compress(ref Huffman.HuffmanData hf){
            try
            {
                TableFq tf = new TableFq(hf.uncompressedData);
                BinaryTree bnTree = new BinaryTree();
                bnTree.feedTree(tf);
                bnTree.computeTree();
                HuffmanCode hfCode = new HuffmanCode();
                hfCode = bnTree.createHuffman();

                hf.sizeOfUncompressedData = hf.uncompressedData.Length;
                hf.frequency = tf.getTable().ToList();
                hf.compressedData = hfCode.concat(hf.uncompressedData);
            }
            catch (SystemException e)
            {
                return false;
            }
            return true;
        }

        bool Huffman.IPlugin.Decompress(ref Huffman.HuffmanData hf){
            try
            {
                TableFq tf = new TableFq(hf.frequency);
                BinaryTree bnTree = new BinaryTree();
                bnTree.feedTree(tf);
                bnTree.computeTree();
                HuffmanCode hfCode = new HuffmanCode();
                hfCode = bnTree.createHuffman();

                hf.uncompressedData = hfCode.decompress(hf.compressedData, hf.sizeOfUncompressedData);
            }
            catch (SystemException e)
            {
                return false;
            }
            return true;
        }

        string Huffman.IPlugin.PluginName{
            get{
                return "GenouxHuTual_plugin";
            }
        }

    }
}
