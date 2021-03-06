﻿using Rsb.EncodingIT.Analyzer.Bootstrap;
using Rsb.EncodingIT.Analyzer.Interfaces;
using Rsb.EncodingIT.Pool.LZW;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Rsb.EncodingIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid parameters. -e or -d <file>");
                return;
            }
            var operation = args[0];
            var file = args[1];

            //var operation = "-e";
            //var file = @"C:\Temp\novo\alice29.txt";

            //var operation = "-d";
            //var file = @"C:\Temp\novo\alice29.rsb";

            //var operation = "-e";
            //var file = @"C:\Temp\novo\eclipse.exe";

            //var operation = "-d";
            //var file = @"C:\Temp\novo\eclipse.rsb";

            //var operation = "-d";
            //var file = @"C:\Temp\novo\sum.rsb";   

            //var operation = "-e";
            //var file = @"C:\Temp\novo\sum";   

            //var operation = "-e";
            //var file = @"C:\Temp\novo\teste.txt";   
            
            var analyzer = default(IFileAnalyzer);
            
            switch (operation)
            {
                case "-e":
                    analyzer = new EncoderAnalyzer();
                    Console.WriteLine("Running encoding...");
                    break;

                case "-d":
                    analyzer = new DecoderAnalyzer();
                    Console.WriteLine("Running decoding...");
                    break;

                default:
                    Console.WriteLine("Operação inválida: '-e' encoding e '-d' decoding");
                    return;
            }

            try
            {
                analyzer.Analyze(file);
            }            
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File was not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured");
            }
        }       
    }
}
