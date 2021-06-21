using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using Microsoft.Win32;
using System.Diagnostics;

namespace Diploma_Project.Desktop_app.Utils
{
    public static class Export
    {
        public static void exportPDF(Order order)
        {
            try
            {
                string fn = "";
                SaveFileDialog myFile = new SaveFileDialog();
                myFile.Filter = "(*.pdf)|*.pdf|All files (*.*)|*.*";
                myFile.FileName = $"Товарно-транспортная накладная №{order.Id} от {DateTime.Now.ToString("d")}";
                if (myFile.ShowDialog() == true)
                    fn = (myFile.FileName);
                iTextSharp.text.Document doc = new iTextSharp.text.Document();

                PdfWriter.GetInstance(doc, new FileStream(fn, FileMode.Create));

                doc.Open();

                BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
                PdfPTable table = new PdfPTable(7);

                PdfPCell cell = new PdfPCell(new Phrase($"Товарно-транспортная накладная №{order.Id} от {DateTime.Now.ToString("d")}", font)); ;

                cell.Colspan = 7;
                cell.HorizontalAlignment = 1;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase($"Грузополучатель: {order.Point.Name} {order.Point.AddressOfPoint}", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Поставщик: ООО \"Зарайский хлебокомбинат\" ИНН: 5022558510 КПП: 501401001", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                table.AddCell(cell);

                string customer = "";
                if (order.User.GetType() == typeof(Seller)) customer = "(Продавец)";
                else customer = "(Покупатель)";
                cell = new PdfPCell(new Phrase($"Заказчик: {customer} {order.User.FullName}", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                table.AddCell(cell);

                string payer = "";
                if (order.User.GetType() == typeof(Seller)) payer = $"{order.Point.Owner},ИНН: {order.Point.INN},КПП: {order.Point.KPP},Адрес: {order.Point.AddressOfOwner}";
                else payer = $"{order.User.FullName}";
                cell = new PdfPCell(new Phrase($"Плательщик: {payer}", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                cell.Padding = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Код", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Наименование", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Кол-во шт.", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Цена без НДС", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Сумма без НДС", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Цена розн.", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase($"Сумма розн.", font)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);

                foreach (var item in order.OrderProducts)
                {
                    cell = new PdfPCell(new Phrase(new Phrase($"{item.Product.Id}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(new Phrase($"{item.Product.Name}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(new Phrase($"{item.Amount}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(new Phrase($"{item.Product.Price * (decimal)0.7}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(new Phrase($"{item.cost * (decimal)0.7}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(new Phrase($"{item.Product.Price}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(new Phrase($"{item.cost}", font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                }
                cell = new PdfPCell(new Phrase("Итого к оплате: " + order.sumOrder + " рублей", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase($"Менджер: {order.Manager.FullName} _________________", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase($"Заказчик: {order.User.FullName} _________________", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                table.AddCell(cell);

                doc.Add(table);

                doc.Close();

            }
            catch (Exception)
            {

               
            }
               
        }
    }
}
