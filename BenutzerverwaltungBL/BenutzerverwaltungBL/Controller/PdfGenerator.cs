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
using iTextSharp;

namespace BenutzerverwaltungBL.Controller
{
    public static class PdfGenerator
    {
        #region private fields
        private static int PAGEWIDTH = (int)PageSize.A4.Width;
        private static int PAGEWIDTH_HALF = PAGEWIDTH / 2;
        private static int PAGEWIDTH_ONE_THIRD = PAGEWIDTH / 3;
        private static int PAGEWIDTH_TWO_THIRD = PAGEWIDTH / 3 *2;
        private static int PAGECORRECTURE = PAGEWIDTH / 9;
        private static Rechnung r = null;
        #endregion

        /// <summary>
        /// generates a bill as pdf out of the given bill
        /// and returns it as byte[] for storing it in a db
        /// </summary>
        /// <param name="rechnung">the object whhich should be "converted" to a pdf</param>
        /// <returns> a byte[]</returns>
        public static byte[] GeneratePDF( Rechnung rechnung )
        {
            byte[] bytes = null;
            r = rechnung;
            using ( MemoryStream ms = new MemoryStream() )
            {               
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document , ms);

                document.Open();

                //1.titel
                document.Add(new Paragraph(rechnung.Kunde.WerkstattKonzern , new Font(Font.FontFamily.HELVETICA , 25 , Font.BOLD)));
                //2.kunden und rechnungsinfo
                PdfPTable tab = CreateInfoTable();
                document.Add(tab);
                //3.anrede "sehr geehrte.."
                addAnredeText(document);

                //4.rechnungsposten und deren preis
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

                //5.gesamtpreis und mwst
                PdfPTable preisInfo = new PdfPTable(2);
                preisInfo.WidthPercentage = 100;
                preisInfo.SetWidths(new int[] { PAGEWIDTH_TWO_THIRD , PAGEWIDTH_ONE_THIRD });

                double gesamtPreis = r.Reparaturen.Sum(item => item.RepArt.Preis);
                addCellRightNoBorder(preisInfo , "Gesamt Netto:");
                addCellRightNoBorder(preisInfo , Math.Round(gesamtPreis,2).ToString());
                addCellRightNoBorder(preisInfo , "MwSt 20%:");
                addCellRightNoBorder(preisInfo , Math.Round((gesamtPreis*0.2),2).ToString());
                addCellRightNoBorder(preisInfo , "Gesamt:");
                addCellRightNoBorder(preisInfo , Math.Round(( gesamtPreis * 1.2),2).ToString());

                document.Add(preisInfo);

                //6.zahlungsbedingungen
                addZahlungsInformation(document);

                //7.Close the document
                document.Close();
                bytes = ms.ToArray();
            }
            return bytes;
        }

        #region methods for generating
        private static void addZahlungsInformation( Document d )
        {
            d.Add(new Paragraph("Zahlbar immerhalb von 10 Tagen abzüglich 2% Skonto, 60 Tage ohne Abzug" , new Font(Font.FontFamily.HELVETICA , 8)) { Leading = 100 });

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

        private static PdfPTable CreateInfoTable()
        {
            PdfPTable table = new PdfPTable(3);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.WidthPercentage = 100;
            table.SetWidths(new int[] { PAGEWIDTH_HALF+PAGECORRECTURE  , PAGEWIDTH_ONE_THIRD - PAGECORRECTURE  , PAGEWIDTH_ONE_THIRD/2 });


            table.AddCell(new PdfPCell() { Border = 0 });
            table.AddCell(new PdfPCell(new Phrase("Rechnung" , new Font(Font.FontFamily.HELVETICA , 14 , 1))) { Border = 0 });
            table.AddCell(new PdfPCell() { Border = 0 });

            addCellNoBorder(table , r.Kunde.FirstName + " " + r.Kunde.LastName);
            addCellNoBorder(table , "Rechnungsnummer:");
            addCellRightNoBorder(table , r.Rechnungsnummer.ToString());

            addCellNoBorder(table , r.Kunde.Adress);
            addCellNoBorder(table , "Datum:");
            addCellRightNoBorder(table , r.Rechnungsdatum.ToShortDateString());

            addCellNoBorder(table , "");
            addCellNoBorder(table , "Kundennummer:");
            addCellRightNoBorder(table , r.Kunde.CustomerId.ToString());

            return table;
        }

        /// <summary>
        /// generates the first line of the bill wih the last name of the customer
        /// </summary>
        /// <param name="d">the document to add </param>
        private static void addAnredeText( Document d )
        {
            d.Add(new Paragraph("Sehr geehrte/r Frau/Herr "+r.Kunde.LastName+",") { Leading = 100 });
            d.Add(new Paragraph("wir erlauben uns, folgende Leistungen in Rechnung zu stellen:") { SpacingAfter = 50 });

        }
        #endregion

    }
}
