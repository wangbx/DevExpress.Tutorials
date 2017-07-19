using System;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraPrinting;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace XtraPrintingDemos.Calendar {
    public class CalendarLink {
        static bool IsHoliday(DayOfWeek date) {
            return date == DayOfWeek.Saturday || date == DayOfWeek.Sunday;
        }

        const int WeeksInMonth = 6;

        ImageList imageList;
        string[] daysOfWeek;
        string[] shortDaysOfWeek;
        DayOfWeek[] week;
        DayOfWeek firstDayOfWeek;
        DateTime? selectedDateValue;

        public DateTime SelectedDate {
            get {
                return selectedDateValue.HasValue ? selectedDateValue.Value : DateTime.Now;
            }
            set {
                selectedDateValue = value;
            }
        }

        public CalendarLink(ImageList iml) {
            daysOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.DayNames;
            shortDaysOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
            firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            week = new DayOfWeek[daysOfWeek.Length];

            int index = 0;
            for(int i = (int)firstDayOfWeek; i < daysOfWeek.Length; i++)
                week[index++] = (DayOfWeek)i;
            for(int i = 0; i < (int)firstDayOfWeek; i++)
                week[index++] = (DayOfWeek)i;

            if(iml != null)
                imageList = iml;
            else {
                imageList = new ImageList();
                imageList.ImageSize = new Size(35, 35);
            }
        }

        public void CreateYearCalendar(PrintingSystem ps, int year, bool landscape) {
            BrickGraphics gr = ps.Graph;
            ps.Begin();
            ps.PageSettings.Landscape = landscape;
            int columnCount;
            int rowCount;
            float width = gr.ClientPageSize.Width - 1;
            float height = gr.ClientPageSize.Height - 1;
            float monthHorizontalPadding;
            float monthVerticalPadding = 24;
            if(width > height) {
                monthHorizontalPadding = 33;
                rowCount = 3;
                columnCount = 4;
                height = height / 4;
                width = (width - 3 * monthHorizontalPadding) / 4;
            } else {
                monthHorizontalPadding = 40;
                columnCount = 3;
                rowCount = 4;
                width = (width - 2 * monthHorizontalPadding) / 3;
                height = height / 5;
            }
            CreatePageFooter(gr);

            gr.Modifier = BrickModifier.ReportHeader;
            gr.Font = new Font("Tahoma", 26f, FontStyle.Bold);
            gr.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);

            gr.BackColor = Color.Transparent;
            Brick brick = gr.DrawString(year.ToString(), Color.FromArgb(41, 113, 182), new RectangleF(0f, 0f, gr.ClientPageSize.Width, height / 2f), BorderSide.None);
            brick.Separable = true;

            gr.Modifier = BrickModifier.Detail;
            int month = 1;
            PaddingInfo daysPadding = new PaddingInfo(10, 10, 10, 10);
            for(int row = 0; row < rowCount; row++) {
                for(int column = 0; column < columnCount; column++) {
                    MonthPrint(
                        gr,
                        column * (width + monthHorizontalPadding),
                        row * (height + monthVerticalPadding),
                        height / 10 + daysPadding.Top,
                        (width - daysPadding.Left - daysPadding.Right) / daysOfWeek.Length,
                        (height - daysPadding.Top) / (WeeksInMonth + 3),
                        month,
                        year,
                        18f,
                        9f,
                        "Tahoma",
                        daysPadding);
                    month++;
                }
            }
            ps.End();
        }

        public void CreateMonthCalendar(PrintingSystem ps, int format, int month, int year, bool landscape) {
            BrickGraphics gr = ps.Graph;
            ps.Begin();
            ps.PageSettings.Landscape = landscape;

            CreatePageFooter(gr);
            PaddingInfo daysPadding = new PaddingInfo(53, 53, 35, 35);
            float leftCell = 0;
            float topRow = 0;
            float width = gr.ClientPageSize.Width - 1;
            float height = gr.ClientPageSize.Height - 1;
            float dayWidth = (width - daysPadding.Left - daysPadding.Right) / week.Length;
            float dayHeight = 60;
            float daysFontSize = 22f;
            float captionFontSize = 36f;

            switch(format) {
                case 1:
                    dayWidth = (dayWidth / 3) * 2;
                    dayHeight = (dayHeight / 3) * 2;
                    daysFontSize = (daysFontSize / 3) * 2;
                    captionFontSize = (captionFontSize / 3) * 2;
                    leftCell = width / WeeksInMonth;
                    topRow = height / WeeksInMonth;
                    break;
                case 2:
                    dayWidth /= 2;
                    dayHeight /= 2;
                    daysFontSize /= 2;
                    captionFontSize /= 2;
                    leftCell = width / 4f;
                    topRow = height / 4f;
                    break;
            }

            gr.Modifier = BrickModifier.Detail;

            MonthPrint(
                gr,
                leftCell,
                topRow,
                dayHeight + 5,
                dayWidth,
                dayHeight,
                month,
                year,
                captionFontSize,
                daysFontSize,
                "Tahoma",
                daysPadding);
            ps.End();
        }

        private bool AllowWidth(BrickGraphics gr, string[] weekdays, float width) {
            bool result = true;
            foreach(string s in weekdays)
                if(gr.MeasureString(s).Width > width)
                    result = false;
            return result;
        }

        private void CreatePageFooter(BrickGraphics gr) {
            gr.BackColor = Color.Transparent;
            gr.Modifier = BrickModifier.MarginalFooter;

            Image img;
            using(Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("XtraPrintingDemos.logo.png"))
                img = new Bitmap(Image.FromStream(str));
            RectangleF r = new RectangleF(0, 0, img.Width, img.Height);

            AddUrl(gr.DrawPageImage(img, r, BorderSide.None, Color.Transparent));
        }
        private void AddUrl(Brick brick) {
            ((IPageBrick)brick).LineAlignment = BrickAlignment.Center;
            ((IPageBrick)brick).Alignment = BrickAlignment.Center;
            brick.Url = "http://devexpress.com/Products/NET/XtraPrinting/";
        }

        private void MonthPrint(BrickGraphics gr, float leftCell, float topRow, float daysHeight, float dayWidth, float dayHeight, int month, int year, float monthNameFontSize, float daysFontSize, string fontName, PaddingInfo dayPadding) {
            gr.BeginUnionRect();

            string captionMonth = Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames[month - 1];
            gr.Font = new Font(fontName, monthNameFontSize, FontStyle.Bold);
            gr.StringFormat = new BrickStringFormat(StringAlignment.Far, StringAlignment.Center);
            gr.BackColor = Color.FromArgb(71, 143, 212);
            gr.BorderColor = Color.FromArgb(42, 114, 183);

            gr.StringFormat = gr.StringFormat.ChangeAlignment(StringAlignment.Center);
            float captionWidth = dayWidth * daysOfWeek.Length + dayPadding.Left + dayPadding.Right;
            float captionHeight = Math.Max(dayHeight * 2 - 14, gr.Font.Height);
            gr.DrawString(
                captionMonth,
                Color.White,
                new RectangleF(leftCell, topRow, captionWidth, captionHeight),
                BorderSide.Top | BorderSide.Right | BorderSide.Left);

            topRow += captionHeight;

            gr.Font = new Font(fontName, daysFontSize, FontStyle.Regular);
            gr.StringFormat = gr.StringFormat.ChangeAlignment(StringAlignment.Center);
            gr.BackColor = Color.White;

            DayOfWeek firstDayInWeek = new DateTime(year, month, 1).DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            bool allowedWidth = AllowWidth(gr, daysOfWeek, dayWidth);
            gr.BorderColor = Color.FromArgb(159, 159, 159);
            gr.DrawRect(new RectangleF(leftCell, topRow, dayWidth * daysOfWeek.Length + dayPadding.Left + dayPadding.Right, daysHeight), BorderSide.Right | BorderSide.Left | BorderSide.Bottom, gr.BackColor, Color.FromArgb(159, 159, 159));

            string weekDay = null;
            int index = 0;
            foreach(DayOfWeek dayOfWeek in week) {
                int i = (int)dayOfWeek;
                weekDay = (allowedWidth) ? daysOfWeek[i] : shortDaysOfWeek[i].Substring(0, Math.Min(shortDaysOfWeek[i].Length, 2));
                gr.DrawString(weekDay, IsHoliday(dayOfWeek) ? Color.FromArgb(188, 0, 0) : Color.FromArgb(42, 114, 183), new RectangleF(leftCell + index * dayWidth + dayPadding.Left, topRow, dayWidth, daysHeight), BorderSide.None);
                index++;
            }

            topRow += daysHeight;
            gr.DrawRect(new RectangleF(leftCell, topRow, dayWidth * daysOfWeek.Length + dayPadding.Left + dayPadding.Right, dayHeight * WeeksInMonth + dayPadding.Top + dayPadding.Bottom), BorderSide.All, Color.FromArgb(255, 241, 219), Color.FromArgb(159, 159, 159));

            gr.Font = new Font(fontName, daysFontSize);
            int day = 1;
            bool drawHorizontalLine = true;

            for(int weekIndex = 0; weekIndex < WeeksInMonth; weekIndex++) {
                foreach(DayOfWeek dayOfWeek in week) {
                    string dayNumber = string.Empty;
                    string hint = null;
                    gr.BackColor = Color.Transparent;
                    gr.ForeColor = Color.Black;
                    if(drawHorizontalLine && day > daysInMonth && dayOfWeek == week[0])
                        drawHorizontalLine = false;
                    if(!(weekIndex == 0 && Array.IndexOf(week, dayOfWeek) < Array.IndexOf(week, firstDayInWeek)) && day <= daysInMonth) {
                        dayNumber = day.ToString();
                        DateTime currentDate = new DateTime(year, month, day);
                        if (currentDate.Equals(SelectedDate)) {
                            gr.BackColor = Color.FromArgb(0x47, 0x8f, 0xd4);
                            gr.ForeColor = Color.White;
                            drawHorizontalLine = true;
                        } else if(IsHoliday(dayOfWeek))
                            gr.ForeColor = Color.FromArgb(188, 0, 0);
                        hint = currentDate.ToString("dddd, MMMM dd yyyy");
                        day++;
                    }
                    BorderSide borders = weekIndex > 0 && drawHorizontalLine ? BorderSide.Top : BorderSide.None;
                    TextBrick dayBrick = gr.DrawString(
                        dayNumber,
                        gr.ForeColor,
                        new RectangleF(
                            leftCell + Array.IndexOf(week, dayOfWeek) * dayWidth + dayPadding.Left,
                            topRow + dayHeight * weekIndex + dayPadding.Top,
                            dayWidth,
                            dayHeight),
                        borders);
                    dayBrick.Hint = hint;
                    dayBrick.BorderStyle = BrickBorderStyle.Outset;
                }
            }
            gr.EndUnionRect();
        }
    }
}
