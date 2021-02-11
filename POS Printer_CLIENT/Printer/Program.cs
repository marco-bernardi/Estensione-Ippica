// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;

namespace Printer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string PrinterADR = "";
            using (StreamReader sr = new StreamReader("C:\\PosPrinter\\settings.txt"))
            {
                PrinterADR = sr.ReadLine();
            }
            if (args == null || args.Length == 0)
            Console.WriteLine("Order ID is not specified.");
            
            //Console.WriteLine(args[0].Replace("print://", string.Empty));
            new ReceiptPrint().Print(PrinterADR, args[0].Replace("print://", string.Empty));
            //Console.ReadKey();
            //print://A1005545570D	Ippica a quota fissa	25/08/2020	4,00	12,00	VAG VARESE GL/PM corsa 4	1 RED PAINTER
            //new ReceiptPrint().Print(PrinterADR, "TSING*A10055429010*S.G.TEATINO TR/PM TROTTO*Data: martedì 1 settembre 2020 ore 21:15*Corsa: 5*Scommessa: PIAZZATO*Pronostico: 4 ALEXANDER MARK*2");
            //Console.ReadKey();
        }
    }
}