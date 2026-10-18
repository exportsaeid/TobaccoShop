using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Models;

namespace TobaccoShop
{
    public static class PrintHelper
    {
        private static PersianCalendar pc = new PersianCalendar();

        private static string ConvertToPersianDate(DateTime date)
        {
            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);
            return $"{year:0000}/{month:00}/{day:00}";
        }

        public static void PrintInvoice(Invoice invoice)
        {
            PrintDocument printDoc = new PrintDocument();

            // تنظیم سایز کاغذ برای پرینتر حرارتی ۸۰ میلی‌متری
            PaperSize paperSize = new PaperSize("Receipt80mm", 315, 1200);
            printDoc.DefaultPageSettings.PaperSize = paperSize;
            printDoc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);

            // اتصال رویداد چاپ
            printDoc.PrintPage += (sender, e) => PrintDoc_PrintPage(e, invoice);

            // نمایش پیش‌نمایش چاپ
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDoc,
                WindowState = FormWindowState.Maximized,
                Text = "پیش‌نمایش چاپ فاکتور (۸۰ میلی‌متری)"
            };

            preview.ShowDialog();
        }

        private static void PrintDoc_PrintPage(PrintPageEventArgs e, Invoice invoice)
        {
            Graphics g = e.Graphics;

            // تنظیمات کیفیت بالا
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            // فونت‌ها
            Font titleFont = new Font("Tahoma", 13, FontStyle.Bold);
            Font tableFont = new Font("Tahoma", 7, FontStyle.Bold);
            Font totalFont = new Font("Tahoma", 8, FontStyle.Bold);
            Font footerFont = new Font("Tahoma", 8);

            float pageWidth = 320f;
            float y = 15f;
            float padding = 6f;

            // فرمت‌های راست‌چین به چپ (RTL)
            StringFormat centerRTL = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            StringFormat rightRTL = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            // ============================================================
            // هدر فاکتور (فروشگاه دخانیات علی)
            // ============================================================
            g.DrawString("🚬 فروشگاه دخانیات علی", titleFont, Brushes.Black, pageWidth / 2, y, centerRTL);
            y += 28;

            string headerLine1 = $"فاکتور شماره: {invoice.Id}     تاریخ: {ConvertToPersianDate(invoice.OrderDate)}";
            g.DrawString(headerLine1, tableFont, Brushes.Black, pageWidth / 2, y, centerRTL);
            y += 18;

            string headerLine2 = $"مشتری: {invoice.CustomerName}";
            g.DrawString(headerLine2, tableFont, Brushes.Black, pageWidth / 2, y, centerRTL);
            y += 24;

            // ============================================================
            // اندازه‌گیری طول متن برای هر ستون
            // ============================================================
            float maxMablagh = g.MeasureString("مبلغ", tableFont).Width;
            float maxTedad = g.MeasureString("تعداد", tableFont).Width;
            float maxGheymat = g.MeasureString("قیمت", tableFont).Width;
            float maxName = g.MeasureString("نام کالا", tableFont).Width;
            float maxRadif = g.MeasureString("ردیف", tableFont).Width;

            int rowIndex = 1;
            foreach (var item in invoice.Items)
            {
                maxMablagh = Math.Max(maxMablagh, g.MeasureString(item.TotalPrice.ToString("N0"), tableFont).Width);
                maxTedad = Math.Max(maxTedad, g.MeasureString(item.Quantity.ToString("N3"), tableFont).Width);
                maxGheymat = Math.Max(maxGheymat, g.MeasureString(item.UnitPrice.ToString("N0"), tableFont).Width);
                maxName = Math.Max(maxName, g.MeasureString(item.ProductName, tableFont).Width);
                maxRadif = Math.Max(maxRadif, g.MeasureString(rowIndex.ToString(), tableFont).Width);
                rowIndex++;
            }

            // ============================================================
            // تنظیم عرض ستون‌ها
            // ============================================================
            float colMablagh = maxMablagh + padding;
            float colTedad = maxTedad + padding;
            float colGheymat = maxGheymat + padding;
            float colName = maxName + padding;
            float colRadif = maxRadif + padding;

            float tableWidth = colMablagh + colTedad + colGheymat + colName + colRadif;

            if (tableWidth > pageWidth - 20)
            {
                float scale = (pageWidth - 20) / tableWidth;
                colMablagh *= scale;
                colTedad *= scale;
                colGheymat *= scale;
                colName *= scale;
                colRadif *= scale;
                tableWidth = colMablagh + colTedad + colGheymat + colName + colRadif;
            }

            float tableLeft = (pageWidth - tableWidth) / 2;

            float xMablagh = tableLeft;
            float xTedad = xMablagh + colMablagh;
            float xGheymat = xTedad + colTedad;
            float xName = xGheymat + colGheymat;
            float xRadif = xName + colName;

            float rowHeight = 20f;

            // ============================================================
            // هدر جدول
            // ============================================================
            g.FillRectangle(new SolidBrush(Color.LightGray), tableLeft, y, tableWidth, rowHeight);
            g.DrawRectangle(new Pen(Color.Black, 1.5f), tableLeft, y, tableWidth, rowHeight);

            g.DrawLine(new Pen(Color.Black, 1.5f), xMablagh, y, xMablagh, y + rowHeight);
            g.DrawLine(new Pen(Color.Black, 1.5f), xTedad, y, xTedad, y + rowHeight);
            g.DrawLine(new Pen(Color.Black, 1.5f), xGheymat, y, xGheymat, y + rowHeight);
            g.DrawLine(new Pen(Color.Black, 1.5f), xName, y, xName, y + rowHeight);
            g.DrawLine(new Pen(Color.Black, 1.5f), xRadif, y, xRadif, y + rowHeight);

            g.DrawString("مبلغ", tableFont, Brushes.Black, new RectangleF(xMablagh + 2, y, colMablagh - 4, rowHeight), centerRTL);
            g.DrawString("تعداد", tableFont, Brushes.Black, new RectangleF(xTedad + 2, y, colTedad - 4, rowHeight), centerRTL);
            g.DrawString("قیمت", tableFont, Brushes.Black, new RectangleF(xGheymat + 2, y, colGheymat - 4, rowHeight), centerRTL);
            g.DrawString("نام کالا", tableFont, Brushes.Black, new RectangleF(xName + 2, y, colName - 4, rowHeight), centerRTL);
            g.DrawString("ردیف", tableFont, Brushes.Black, new RectangleF(xRadif + 2, y, colRadif - 4, rowHeight), centerRTL);

            y += rowHeight;

            // ============================================================
            // آیتم‌های جدول
            // ============================================================
            int i = 1;
            bool alt = false;
            foreach (var item in invoice.Items)
            {
                if (alt) g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), tableLeft, y, tableWidth, rowHeight);

                g.DrawRectangle(new Pen(Color.Black, 0.8f), tableLeft, y, tableWidth, rowHeight);

                g.DrawLine(new Pen(Color.Black, 0.8f), xMablagh, y, xMablagh, y + rowHeight);
                g.DrawLine(new Pen(Color.Black, 0.8f), xTedad, y, xTedad, y + rowHeight);
                g.DrawLine(new Pen(Color.Black, 0.8f), xGheymat, y, xGheymat, y + rowHeight);
                g.DrawLine(new Pen(Color.Black, 0.8f), xName, y, xName, y + rowHeight);
                g.DrawLine(new Pen(Color.Black, 0.8f), xRadif, y, xRadif, y + rowHeight);

                // مبلغ
                string totalText = item.TotalPrice.ToString("N0");
                Font priceFont = tableFont;
                SizeF sz = g.MeasureString(totalText, tableFont);
                if (sz.Width > colMablagh - 8)
                {
                    float ns = tableFont.Size * ((colMablagh - 8) / sz.Width);
                    if (ns < 5) ns = 5;
                    priceFont = new Font(tableFont.FontFamily, ns, tableFont.Style);
                }
                g.DrawString(totalText, priceFont, Brushes.Black, new RectangleF(xMablagh + 2, y, colMablagh - 4, rowHeight), rightRTL);

                // تعداد
                g.DrawString(item.Quantity.ToString("0.###"), tableFont, Brushes.Black,
                    new RectangleF(xTedad + 2, y, colTedad - 4, rowHeight), centerRTL);

                // قیمت
                g.DrawString(item.UnitPrice.ToString("N0"), tableFont, Brushes.Black,
                    new RectangleF(xGheymat + 2, y, colGheymat - 4, rowHeight), rightRTL);

                // نام کالا
                g.DrawString(item.ProductName, tableFont, Brushes.Black,
                    new RectangleF(xName + 2, y, colName - 4, rowHeight), centerRTL);

                // ردیف
                g.DrawString(i.ToString(), tableFont, Brushes.Black,
                    new RectangleF(xRadif + 2, y, colRadif - 4, rowHeight), centerRTL);

                y += rowHeight;
                i++;
                alt = !alt;
            }

            // ============================================================
            // سطر آخر - جمع کل
            // ============================================================
            string totalAmount = invoice.TotalAmount.ToString("N0");
            string totalDisplay = $"جمع کل: {totalAmount} تومان";

            g.FillRectangle(new SolidBrush(Color.FromArgb(230, 240, 255)), tableLeft, y, tableWidth, rowHeight);
            g.DrawRectangle(new Pen(Color.Black, 1.5f), tableLeft, y, tableWidth, rowHeight);

            g.DrawString(totalDisplay, totalFont, Brushes.DarkBlue, new RectangleF(tableLeft + 2, y, tableWidth - 4, rowHeight), centerRTL);

            y += rowHeight;
            y += 20;

            // ============================================================
            // فوتر
            // ============================================================
            g.DrawString("با تشکر از خرید شما", new Font("Tahoma", 10, FontStyle.Bold), Brushes.Black, pageWidth / 2, y, centerRTL);
            y += 18;

            g.DrawString("🚬 فروشگاه دخانیات علی - بهترین انتخاب شما", footerFont, Brushes.Black, pageWidth / 2, y, centerRTL);

            e.HasMorePages = false;
        }
    }
}