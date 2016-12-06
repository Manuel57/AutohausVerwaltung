using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using BenutzerverwaltungBL.Model.DataObjects;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace BenutzerverwaltungBL.Controller
{
    public static class PdfGenerator
    {
        #region private fields
        private static int PAGEWIDTH = (int)PageSize.A4.Width;
        private static int PAGEWIDTHHALF = PAGEWIDTH / 2;
        private static int PAGEWIDTH_ONE_THIRD = PAGEWIDTH / 3;
        private static int PAGEWIDTH_TWO_THIRD = PAGEWIDTH / 3 *2;
        private static int PAGECORRECTURE = PAGEWIDTH / 9;
        #endregion
        public static byte[] GeneratePDF( Rechnung rechnung )
        {
            byte[] bytes = null;
            using ( MemoryStream ms = new MemoryStream() )
            {
                Document document = new Document(PageSize.A4);
                document.Open();

                document.Add(new Paragraph(rechnung.Kunde.WerkstattKonzern , new Font(Font.FontFamily.HELVETICA , 25 , Font.BOLD)));

                document.Add(CreateInfoTable());

                addAnredeText(document);

                PdfPTable posten = new PdfPTable(2);
                posten.WidthPercentage = 100;

                posten.SetWidths(new int[] {PAGEWIDTH_TWO_THIRD , PAGEWIDTH_ONE_THIRD });
                AddCellToHeader(posten , "Bezeichnung");
                AddCellToHeader(posten , "Preis Netto");
                foreach ( var reparatur in rechnung.Reparaturen )
                {
                    addCell(posten , reparatur.RepArt.Bezeichnung);
                    addCellRight(posten ,reparatur.RepArt.Preis.ToString());
                }

                document.Add(posten);
                PdfPTable preisInfo = new PdfPTable(2);
                preisInfo.WidthPercentage = 100;

                preisInfo.SetWidths(new int[] { ( int ) ( PageSize.A4.Width ) / 3 * 2 , ( int ) ( PageSize.A4.Width ) / 3 });

                addCellRightNoBorder(preisInfo , "Gesamt Netto:");
                addCellRightNoBorder(preisInfo , "0.00");
                addCellRightNoBorder(preisInfo , "MwSt 20%");
                addCellRightNoBorder(preisInfo , "0.00");
                addCellRightNoBorder(preisInfo , "Gesamt");
                addCellRightNoBorder(preisInfo , "0.00");


                document.Add(preisInfo);
                addZahlungsInformation(document);

                // Close the document
                document.Close();
                bytes = ms.ToArray();
            }
            return bytes;
        }

        private static void addZahlungsInformation( Document d )
        {
            d.Add(new Paragraph("Zahlbar immerhalb von 10 Tagen abzüglich 2%, 60 Tage ohne Abzug" , new Font(Font.FontFamily.HELVETICA , 8)) { Leading = 100 });

        }
        private static void addCellRightNoBorder( PdfPTable table , string text )
        {
            addCell(table , text , 0 , Element.ALIGN_RIGHT);

        }
        private static void addCellNoBorder( PdfPTable table , string text )
        {
            addCell(table , text , 0 , Element.ALIGN_LEFT);
        }
        private static void addCell( PdfPTable table , string text )
        {
            table.AddCell(new PdfPCell(new Phrase(text)));
        }
        private static void addCellRight( PdfPTable table , string text )
        {
            table.AddCell(new PdfPCell(new Phrase(text))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT
            });
        }
        private static void addCell( PdfPTable tab , string txt , int border , int hAlign )
        {
            tab.AddCell(new PdfPCell(new Phrase(txt))
            {
                Border = border ,
                HorizontalAlignment = hAlign
            });
        }
        private static void AddCellToHeader( PdfPTable tableLayout , string cellText )
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText , new Font(Font.FontFamily.HELVETICA , 12 , 1))) { HorizontalAlignment = Element.ALIGN_CENTER , Padding = 5 , Border = 0 });
        }

        private static PdfPTable CreateInfoTable( )
        {
            PdfPTable table = new PdfPTable(3);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.WidthPercentage = 100;
            table.SetWidths(new int[] { ( int ) ( PageSize.A4.Width / 2 + ( int ) ( PageSize.A4.Width / 9 ) ) , ( int ) ( PageSize.A4.Width / 3 - ( int ) ( PageSize.A4.Width / 9 ) ) , ( int ) ( PageSize.A4.Width / 6 ) });


            table.AddCell(new PdfPCell() { Border = 0 });

            table.AddCell(new PdfPCell(new Phrase("Rechnung" , new Font(Font.FontFamily.HELVETICA , 14 , 1))) { Border = 0 });
            table.AddCell(new PdfPCell() { Border = 0 });

            addCellNoBorder(table , "Max Mustermann");
            addCellNoBorder(table , "Rechnungsnummer:");
            addCellRightNoBorder(table , "001");

            addCellNoBorder(table , "Musterstrasse 12");
            addCellNoBorder(table , "Datum:");
            addCellRightNoBorder(table , "06.12.2016");

            addCellNoBorder(table , "9500 Villach");
            addCellNoBorder(table , "Kundennummer:");

            addCellRightNoBorder(table , "1234");
            return table;
        }

        private static void addAnredeText( Document d )
        {
            d.Add(new Paragraph("Sehr geerte/r Frau/Herr Mustermann,") { Leading = 100 });
            d.Add(new Paragraph("wir erlauben uns, Ihren Auftrag wiefolgt in Rechnung zu stellen:") { SpacingAfter = 50 });

        }


    }
}
