using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Pool.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rsb.EncodingIT.Data
{
    public class HeaderParser
    {
        public HeaderParser()
        {

        }
        
        // header: 32 bits header size + 8 bits pipeline + 24 bits extensions + n bits huffman metadata
        // header: 4 bytes + 1 byte + 3 bytes + N bytes -> mais fácil para trabalhar
        public FileHeader ReadHeader(MemoryStream entireFile, out byte[] encodedData)
        {
            var reader = new BinaryReader(entireFile, );
            
            // 4 bytes -> Max header size 2GB
            var headerSizeInBytes = reader.ReadInt32();
            // 1 byte
            var pipelineByte = new BitArray(reader.ReadByte());
            // 3 bytes
            var sourceExtensionBytes = reader.ReadBytes(3);

            string sourceExtesion = string.Empty;

            foreach (var b in sourceExtensionBytes)
                sourceExtesion += Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(Convert.ToString(b, 10)));

            var algorithmPipeline = (AlgorithmPipeline) Convert.ToInt32(pipelineByte.ToString());
            
            if (entireFile.Position != 7) throw new InvalidDataException("Error reading header");

            // 4 + 1 already readed
            var huffmanMetadata = reader.ReadBytes(((headerSizeInBytes - 4) - 1) - 3);
            
            if (entireFile.Position != headerSizeInBytes) throw new InvalidDataException("Error reading header");

            // read to end of stream
            encodedData = reader.ReadBytes( (int) (entireFile.Length - reader.BaseStream.Position) );

            reader.Close();

            return new FileHeader(algorithmPipeline, huffmanMetadata, sourceExtesion);
        }

        public byte[] WriteHeader(FileHeader header, MemoryStream encodedData)
        {
            var totalHeaderSize = header.HuffmanMetadata.Length + 4 + 1;
            var pipeline = (short) header.Pipeline;

            var pipelineBytes = Convert.ToByte(pipeline);
            var totalHeaderSizeBytes = BitConverter.GetBytes(totalHeaderSize);

            // tamanho 3?
            var sourceExtensionBytes = Encoding.ASCII.GetBytes(header.SourceExtension);
            
            var streamOutput = new MemoryStream();
            var writer = new BinaryWriter(streamOutput);

            writer.Write(totalHeaderSizeBytes);
            writer.Write(pipelineBytes);
            writer.Write(sourceExtensionBytes); // tamanho 3?
            writer.Write(header.HuffmanMetadata);
            writer.Write(encodedData.ToArray());
            writer.Flush();
            writer.Close();
            streamOutput.Close();

            return streamOutput.ToArray();
        }
    }
}
