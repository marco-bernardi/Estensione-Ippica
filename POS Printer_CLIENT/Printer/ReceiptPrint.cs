// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Drawing;
using System;
using System.IO;
using System.Collections.Generic;
using BarcodeLib;
using BarcodeStandard;

namespace Printer
{
    public class ReceiptPrint : PrintBase
    {
        private string orderId;
        private string quota;
        private string importo;
        private string vincita;
        private string type;
        private string evento;
        private string data;
        private string corsa;
        private string scommessa;
        private string pronostico;

        private List<string> eventi = new List<string>();

        //private Order order;

        // TODO: we don't need the orderId paramter, it is here just as an illustration
        public void Print(string printerName, string stringFromWeb)
        {
            string[] splitted = stringFromWeb.Split('*');
            this.type = splitted[0];
            switch (this.type)
            {
                case "QF":
                    this.orderId = splitted[1];
                    this.quota = splitted[2];
                    this.vincita = splitted[3].Split('%')[0];
                    this.importo = splitted[4];
                    for (int i = 5; i < splitted.Length; i++)
                    {
                        eventi.Add(splitted[i]);
                    }
                    break;
                case "TSING":
                    this.orderId = splitted[1];
                    this.evento = splitted[2];
                    this.data = splitted[3];
                    this.corsa = splitted[4];
                    this.scommessa = splitted[5];
                    this.pronostico = splitted[6];
                    this.importo = splitted[7];
                    break;
                case "TSIS":
                    this.orderId = splitted[1];
                    this.importo = splitted[2];
                    for (int i = 3; i < splitted.Length; i++)
                    {
                        eventi.Add(splitted[i]);
                    }
                    break;
                default:
                    break;
            }
            this.Print(printerName, this.PrintCustomerFragment);
        }

        private void PrintCustomerFragment(Graphics g)
        {
            float y = 0;

            using (Image logoImage = Image.FromFile(@"C:\\PosPrinter\\logo.png"))
                g.DrawImage(logoImage, 7, 0, 60, 9);

            y += 9; // Image logo height
            y += 5; // Empty line
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.Encode(BarcodeLib.TYPE.CODE128, this.orderId, Color.Black, Color.White, 290, 120);
            using (Image logoImage = b.Encode(BarcodeLib.TYPE.CODE128, this.orderId, Color.Black, Color.White, 290, 120))
                g.DrawImage(logoImage, 7, 14, 60, 7);

            y += 7; // Image logo height
            y += 5; // Empty line
            //using (Image barImage = Image.FromFile(@"C:\\PosPrinter\\barcode.png"))
            //    g.DrawImage(barImage, 0, 0, 60, 7);
            //y += 7;
            //y += 5;

            y += this.DrawTextColumns(
               g, y,
               new TextColumn($"CC:4106", 0.33f, fontSize: 8f),
               new TextColumn("PV:5529", 0.33f, StringAlignment.Center, 8f),
               new TextColumn("TM:2", 0.34f, StringAlignment.Far, 8f)
             );
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"NC:CORNER SAN MARTINO DI LUPARI BRENTA", 1.0f, fontSize: 10f)
            );
            y += 1;
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"___________________________________________", 1f, fontSize: 8f));
            y += 2;
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"ID Ticket:", 0.4f, fontSize: 14f),
              new TextColumn(this.orderId, 0.6f, StringAlignment.Far, 14f)
            );
            y += 5;
            switch (this.type)
            {
                case "QF":
                    for (int i = 0; i < eventi.Count - 2; i += 3)
                    {
                        y += this.DrawTextColumns(
                          g, y,
                          new TextColumn($"{eventi[i].Replace("%20", " ").Replace("/", "")}", 0.8f),
                          new TextColumn($"{eventi[i + 2]}", 0.2f, StringAlignment.Far)
                        );
                        y += this.DrawTextColumns(
                          g, y,
                          new TextColumn($"{eventi[i + 1].Replace("%20", " ").Replace("/", "")}", 1.0f)
                        );
                        y += 2;
                    }
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"QUOTA TOTALE:", 0.8f),
                      new TextColumn(this.quota, 0.2f, StringAlignment.Far)
                    );

                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"IMPORTO:", 0.8f),
                      new TextColumn(this.importo + "€", 0.2f, StringAlignment.Far)
                    );

                    y += 2; // Empty line
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"VINCITA POTENZIALE", 0.6f),
                      new TextColumn(this.vincita + "€", 0.4f, StringAlignment.Far)
                    );
                    y += 1;
                    break;
                case "TSING":
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"{evento.Replace("%20", " ").Replace("/", "")}", 1f)
                    );
                    y += 1;
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"{corsa.Replace("%20", " ").Replace("/", "")}", 1f)
                    );
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"{scommessa.Replace("%20", " ").Replace("/", "")}", 1f)
                    );
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"{pronostico.Replace("%20", " ").Replace("/", "")}", 1f)
                    );
                    y += 2;
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"IMPORTO:", 0.8f),
                      new TextColumn(this.importo + "€", 0.2f, StringAlignment.Far)
                    );
                    break;
                case "TSIS":
                    for (int i = 0; i < eventi.Count - 2; i += 3)
                    {
                        y += this.DrawTextColumns(
                          g, y,
                          new TextColumn($"{eventi[i].Replace("%20", " ").Replace("/", "")}", 1.0f)
                        );
                        y += this.DrawTextColumns(
                          g, y,
                          new TextColumn($"{eventi[i + 1].Replace("%20", " ")}", 0.8f),
                          new TextColumn($"{eventi[i + 2]}€", 0.2f, StringAlignment.Far)
                        );
                        y += 2;
                    }
                    y += this.DrawTextColumns(
                      g, y,
                      new TextColumn($"IMPORTO:", 0.8f),
                      new TextColumn(this.importo + "€", 0.2f, StringAlignment.Far)
                    );
                    break;
                default:
                    break;
            }
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"___________________________________________", 1f, fontSize: 8f));
            y += 2;
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"Il gioco può causare dipendenza", 1.00f, StringAlignment.Center, fontSize: 6f));
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"Consulta probabilità di vincita su www.aams.gov.it", 1.00f, StringAlignment.Center, fontSize: 6f));
            y += this.DrawTextColumns(
              g, y,
              new TextColumn($"E' vietato il gioco ai minori di anni 18", 1.00f, StringAlignment.Center, fontSize: 6f));

        }
    }
}