using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace XtraPrintingDemos.BioLifePrinting {
    public class BioLifePrintingLink {
        const string pictureColumn = "Picture";
        const string notesColumn = "Notes";
        const string idColumn = "ID";
        const string lengthColumn = "Length(cm)";
        const string speciesNoColumn = "Species No";
        const string commonNameColumn = "Common Name";
        const string speciesNameColumn = "Species Name";
        const string categoryColumn = "Category";
        const string markColumn = "Mark";

        PrintingSystem printingSystem;
        ProgressBar progress;

        public BioLifePrintingLink(PrintingSystem ps, ProgressBar pb) {
            this.printingSystem = ps;
            this.progress = pb;
        }

        public static Image CreateFishImage(byte[] b) {
            if(b == null)
                return null;
            using (MemoryStream stream = new MemoryStream(b))
                return CreateImage(stream);
        }
        static Image CreateImage(Stream stream) {
            using (Image image = Image.FromStream(stream))
                return new Bitmap(image);
        }

        public bool PrintGroupingGrid(DataView dv) {
            using(DataView dvSorting = new DataView(dv.Table)) {
                dvSorting.Sort = dvSorting.Table.Columns[categoryColumn].ColumnName + " ASC";

                BrickGraphics gr = printingSystem.Graph;
                printingSystem.Begin();

                int gridWidth = (int)gr.ClientPageSize.Width - 1;
                gr.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);
                gr.BorderColor = Color.Black;
                gr.Font = new Font("Arial", 10f, FontStyle.Bold);

                int fontHeight = gr.Font.Height + 4;
                int xPicture = gridWidth - (fontHeight * 10) / 3;
                int y = fontHeight * 2;

                if(gridWidth < (fontHeight * 10) / 3) {
                    printingSystem.End();
                    return false;
                }

                //header
                int topRow = 1;
                gr.Modifier = BrickModifier.DetailHeader;
                gr.BackColor = Color.FromArgb(71, 143, 212);
                gr.Font = new Font("Tahoma", 9f, FontStyle.Bold);
                int headerFontHeight = gr.Font.Height + 5;

                gr.DrawString("ID", Color.White,
                    new RectangleF(0, topRow, xPicture / 3 - 1, headerFontHeight - 1), BorderSide.None);
                gr.DrawString("Species No", SystemColors.HighlightText,
                    new RectangleF(xPicture / 3, topRow, xPicture / 3 - 1, headerFontHeight - 1), BorderSide.None);
                gr.DrawString("Length", SystemColors.HighlightText,
                    new RectangleF((xPicture / 3) * 2, topRow, xPicture - (xPicture / 3) * 2 - 1, headerFontHeight - 1), BorderSide.None);
                gr.DrawString("Common Name", SystemColors.HighlightText,
                    new RectangleF(0, topRow + headerFontHeight, xPicture / 2 - 1, headerFontHeight), BorderSide.None);
                gr.DrawString("Species Name", SystemColors.HighlightText,
                    new RectangleF(xPicture / 2, topRow + headerFontHeight, xPicture - xPicture / 2 - 1, headerFontHeight), BorderSide.None);
                gr.DrawString("Image", SystemColors.HighlightText,
                    new RectangleF(xPicture, topRow, gridWidth - xPicture, headerFontHeight * 2), BorderSide.None);
                EmptyBrick brick = gr.DrawEmptyBrick(new RectangleF(0, headerFontHeight * 2 + 1, gridWidth, fontHeight / 2));
                brick.SeparableHorz = true;

                //strings
                gr.Modifier = BrickModifier.Detail;
                gr.BackColor = SystemColors.Highlight;
                string oldCategory = string.Empty;
                Image img;

                InitProgressBar(dvSorting.Count - 1);
                gr.BorderColor = Color.Gray;
                bool isFirstCategory = true;
                bool drawingGroup = false;

                for(int i = 0; i < dvSorting.Count; i++) {
                    ValueProgressBar(i);
                    DataRow row = dvSorting[i].Row;
                    img = CreateFishImage(row[pictureColumn] as byte[]);

                    if(oldCategory != (string)row[categoryColumn]) {
                        if(drawingGroup)
                            gr.EndUnionRect();
                        drawingGroup = true;
                        gr.BeginUnionRect();
                        gr.StringFormat = gr.StringFormat.ChangeAlignment(StringAlignment.Near);
                        gr.Font = new Font("Tahoma", 10f, FontStyle.Bold);
                        gr.BackColor = Color.FromArgb(255, 241, 219);
                        int topMargin = 0;
                        if(!isFirstCategory)
                            topMargin = fontHeight;
                        isFirstCategory = false;
                        TextBrick categoryBrick = gr.DrawString("Category : " + row[categoryColumn].ToString(), Color.FromArgb(27, 91, 182),
                            new RectangleF(0, topRow + topMargin, gridWidth, fontHeight), BorderSide.Top);
                        categoryBrick.SeparableHorz = true;
                        categoryBrick.Padding = new PaddingInfo(8, 8, 0, 0);

                        topRow += fontHeight + topMargin;
                    }
                    oldCategory = (string)row[categoryColumn];
                    gr.Font = new Font("Tahoma", 10f);
                    gr.BackColor = Color.White;

                    gr.StringFormat = gr.StringFormat.ChangeAlignment(StringAlignment.Center);
                    gr.DrawString(row[idColumn].ToString(), Color.Black,
                        new RectangleF(0, topRow, xPicture / 3, fontHeight), BorderSide.Top | BorderSide.Right | BorderSide.Bottom);
                    gr.DrawString(row[speciesNoColumn].ToString(), Color.Black,
                        new RectangleF(xPicture / 3, topRow, xPicture / 3, fontHeight), BorderSide.All);
                    gr.DrawString(row[lengthColumn].ToString(), Color.Black,
                        new RectangleF((xPicture / 3) * 2, topRow, xPicture - (xPicture / 3) * 2, fontHeight), BorderSide.All);

                    gr.DrawString(row[commonNameColumn].ToString(), Color.Black,
                        new RectangleF(0, topRow + fontHeight, xPicture / 2, fontHeight), BorderSide.Top | BorderSide.Right | BorderSide.Bottom);
                    gr.DrawString(row[speciesNameColumn].ToString(), Color.Black,
                        new RectangleF(xPicture / 2, topRow + fontHeight, xPicture - xPicture / 2, fontHeight), BorderSide.Top | BorderSide.Left | BorderSide.Bottom);
                    gr.DrawImage(img, new RectangleF(xPicture, topRow, gridWidth - xPicture, y), BorderSide.All, gr.BackColor);
                    topRow += fontHeight * 2;
                }
                if(drawingGroup)
                    gr.EndUnionRect();
                CreatePageFooter(gr);
                CreatePageHeader(gr);
                printingSystem.End();
                ValueProgressBar(0);
            }
            return true;
        }

        public bool PrintLabels(DataView dv, int rowN) {
            const int padding = 10;
            const int margin = 3;

            BrickGraphics graphics = printingSystem.Graph;
            printingSystem.Begin();

            graphics.StringFormat = graphics.StringFormat.ChangeFormatFlags(graphics.StringFormat.FormatFlags | StringFormatFlags.LineLimit);

            int x = (int)graphics.ClientPageSize.Width - 2;

            graphics.Modifier = BrickModifier.Detail;
            graphics.StringFormat = graphics.StringFormat.ChangeLineAlignment(StringAlignment.Near);
            graphics.BorderColor = Color.Black;
            graphics.Font = new Font("Tahoma", 8f);
            int textHeight = graphics.Font.Height + 2;

            int wBrick = (x - padding) / 2;
            int wPic = (wBrick / 5) * 3;
            int hPic;
            using(Image image = CreateFishImage(dv.Table.Rows[0][pictureColumn] as byte[])) {
                hPic = (image.Height * wPic) / image.Width;
            }
            int hBrick = (int)(16.66667 * textHeight);

            //strings
            int topRow = 0;
            int leftCell = 0;

            InitProgressBar(dv.Count - 1);

            int iFrom = 0, iTo = dv.Count;
            if(rowN != -1) {
                iFrom = rowN;
                iTo = rowN + 1;
            }
            int i = 0;
            foreach(DataRowView row in dv) {
                ValueProgressBar(i);
                if(i >= iFrom && i < iTo) {
                    graphics.Font = new Font("Tahoma", 8f);
                    graphics.StringFormat = graphics.StringFormat.ChangeAlignment(StringAlignment.Near);
                    graphics.BackColor = Color.White;
                    if(i % 2 == 0)
                        leftCell = 0;

                    graphics.DrawRect(new RectangleF(leftCell, topRow, wBrick, hBrick), BorderSide.All, Color.FromArgb(172, 222, 255), graphics.BorderColor);
                    leftCell += margin;
                    topRow += margin;

                    Image img = CreateFishImage(row[pictureColumn] as byte[]);
                    graphics.DrawImage(img, new RectangleF(leftCell, topRow, wPic, hPic), BorderSide.All, Color.White);

                    graphics.StringFormat = graphics.StringFormat.ChangeAlignment(StringAlignment.Near);
                    graphics.BorderColor = Color.FromArgb(16, 79, 121);
                    PaddingInfo titlePaddingInfo = new PaddingInfo(2, 0, 1, 0);
                    PaddingInfo valuePaddingInfo = new PaddingInfo(0, 2, 1, 0);
                    for(int j = 1; j <= 5; j++) {
                        float top = topRow + hPic + textHeight * (j - 1);
                        string title = dv.Table.Columns[j + 1].Caption + ':';
                        SizeF titleSize = graphics.MeasureString(title);
                        titleSize.Width += 3f;
                        TextBrick titleBrick = graphics.DrawString(title, Color.FromArgb(30, 100, 168), new RectangleF(leftCell, top, titleSize.Width, textHeight), BorderSide.Bottom | BorderSide.Top | BorderSide.Left);
                        titleBrick.Padding = titlePaddingInfo;

                        TextBrick textValueBrick = graphics.DrawString(row[j + 1].ToString(), Color.Black, new RectangleF(leftCell + titleSize.Width, top, wPic - titleSize.Width, textHeight), BorderSide.Bottom | BorderSide.Top | BorderSide.Right);
                        textValueBrick.Padding = valuePaddingInfo;
                    }

                    graphics.Font = new Font("Tahoma", 7f);
                    TextBrick descriptionBrick = graphics.DrawString((string)row[notesColumn], Color.FromArgb(16, 79, 121),
                        new RectangleF(leftCell + wPic + margin, topRow, wBrick - wPic - margin * 3, hBrick - margin * 2), BorderSide.All);
                    descriptionBrick.Padding = new PaddingInfo(6, 6, 10, 10);

                    graphics.Font = new Font("Tahoma", 10f, FontStyle.Bold);
                    graphics.StringFormat = graphics.StringFormat.ChangeAlignment(StringAlignment.Far);
                    graphics.BackColor = Color.Transparent;
                    graphics.DrawString("Record No: " + row[idColumn].ToString(), Color.FromArgb(16, 79, 121),
                        new RectangleF(leftCell, topRow + hBrick - textHeight * 2 + textHeight / 2, wPic, textHeight), BorderSide.None);
                    graphics.DrawCheckBox(new RectangleF(leftCell, topRow + hBrick - textHeight * 2 + textHeight / 2, textHeight, textHeight), BorderSide.None, graphics.BackColor, (bool)row[markColumn]);

                    leftCell = wBrick + padding;
                    if(i % 2 == 1)
                        topRow += hBrick + padding;
                    else
                        topRow -= margin;
                }
                i++;
            }
            CreatePageFooter(graphics);
            CreatePageHeader(graphics);
            printingSystem.End();
            ValueProgressBar(0);
            return true;
        }

        public bool PrintGrid(DataView data, int rowN) {
            const int offset = 3;
            BrickGraphics graphics = printingSystem.Graph;
            printingSystem.Begin();

            int x = (int)graphics.ClientPageSize.Width - 1;
            if(x < CreateFishImage(data.Table.Rows[0][pictureColumn] as byte[]).Width) {
                printingSystem.End();
                return false;
            }

            //header
            graphics.Modifier = BrickModifier.DetailHeader;
            graphics.Font = new Font("Tahoma", 12f, FontStyle.Bold);
            graphics.BackColor = Color.FromArgb(71, 143, 212);
            graphics.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);

            int fontHeight = graphics.Font.Height + offset * 2;
            graphics.BorderColor = Color.FromArgb(27, 91, 182);
            graphics.DrawString(data.Table.TableName, Color.White,
                new RectangleF(0, 0, x, fontHeight), BorderSide.All);
            graphics.DrawRect(new RectangleF(0, fontHeight + 1, x, offset), BorderSide.None, Color.Transparent, Color.Transparent);

            //strings
            int topRow = 0;
            graphics.Modifier = BrickModifier.Detail;
            graphics.Font = new Font("Tahoma", 8f, FontStyle.Bold | FontStyle.Italic);
            graphics.BorderColor = SystemColors.ControlDark;

            fontHeight = graphics.Font.Height + 2;
            InitProgressBar(data.Count - 1);

            int iFrom = 0, iTo = data.Count;
            if(rowN != -1) {
                iFrom = rowN;
                iTo = rowN + 1;
            }
            int i = 0;
            foreach(DataRowView row in data) {
                ValueProgressBar(i);

                if(i >= iFrom && i < iTo) {
                    graphics.BeginUnionRect();
                    graphics.BorderColor = Color.Gray;
                    Image img = CreateFishImage(row[pictureColumn] as byte[]);
                    graphics.DrawImage(img, new RectangleF(0, topRow, img.Width, img.Height), BorderSide.All, Color.White);

                    graphics.Font = new Font("Tahoma", 8f, FontStyle.Bold | FontStyle.Italic);
                    graphics.BackColor = Color.White;
                    graphics.StringFormat = graphics.StringFormat.ChangeAlignment(StringAlignment.Center);
                    graphics.DrawCheckBox(new RectangleF(offset, topRow - offset - fontHeight + img.Height, fontHeight, fontHeight), BorderSide.None, graphics.BackColor, (bool)row[markColumn]);
                    graphics.DrawString(row[idColumn].ToString(), Color.FromArgb(30, 100, 168), new RectangleF(offset, topRow + offset, fontHeight * 2, fontHeight), BorderSide.All);

                    const int RowsCount = 5;
                    PaddingInfo padding = new PaddingInfo(8, 8, 4, 2);

                    for(int j = 1; j <= RowsCount; j++) {
                        graphics.DrawRect(new RectangleF(img.Width, topRow + (img.Height / RowsCount) * (j - 1), x - img.Width, img.Height / RowsCount), BorderSide.All, Color.White, graphics.BorderColor);

                        graphics.Font = new Font("Tahoma", 10f, FontStyle.Bold);
                        graphics.BackColor = Color.FromArgb(172, 222, 255);
                        graphics.BorderColor = Color.FromArgb(16, 79, 121);
                        graphics.StringFormat = graphics.StringFormat.ChangeAlignment(StringAlignment.Near);
                        TextBrick titleBrick = graphics.DrawString(
                            data.Table.Columns[j + 1].Caption + ':',
                            Color.FromArgb(30, 100, 168),
                            new RectangleF(
                                img.Width + offset,
                                topRow + (img.Height / RowsCount) * (j - 1) + offset,
                                (x - img.Width) / 3 + 8,
                                (img.Height / RowsCount) - offset * 2),
                                BorderSide.All);
                        titleBrick.Padding = padding;

                        graphics.BackColor = Color.FromArgb(255, 241, 219);
                        graphics.BorderColor = Color.Gray;
                        TextBrick valueBrick = graphics.DrawString(
                            row[j + 1].ToString(),
                            (j == 3) ? Color.FromArgb(30, 100, 168) : Color.FromArgb(62, 62, 62),
                            new RectangleF(
                                img.Width + (x - img.Width) / 3 + offset * 2 + 8,
                                topRow + (img.Height / 5) * (j - 1) + offset,
                                ((x - img.Width) / 3) * 2 - offset * 3 - 8,
                                (img.Height / RowsCount) - offset * 2),
                            BorderSide.All);
                        valueBrick.Padding = padding;
                    }
                    graphics.Font = new Font("Tahoma", 8f);
                    graphics.BackColor = Color.White;
                    PaddingInfo notesPadding = new PaddingInfo(12, 12, 5, 4);
                    int hNote = (int)graphics.MeasureString((string)row[notesColumn], x - notesPadding.Left - notesPadding.Right).Height + 2 + notesPadding.Top + notesPadding.Bottom;
                    TextBrick notesBrick = graphics.DrawString((string)row[notesColumn], Color.Black, new RectangleF(0, topRow + img.Height, x, hNote), BorderSide.All);
                    notesBrick.Hint = (string)row[notesColumn];
                    notesBrick.Padding = notesPadding;
                    graphics.EndUnionRect();
                    const int bottomMargin = 10;
                    topRow += img.Height + hNote + offset + bottomMargin;
                }
                i++;
            }
            CreatePageFooter(graphics);
            CreatePageHeader(graphics);
            printingSystem.End();
            ValueProgressBar(0);
            return true;
        }

        private void InitProgressBar(int i) {
            if(progress != null)
                progress.Maximum = i;
        }

        private void ValueProgressBar(int i) {
            if(progress != null)
                progress.Value = i;
        }

        private void CreatePageFooter(BrickGraphics gr) {
            gr.StringFormat = gr.StringFormat.ChangeFormatFlags(gr.StringFormat.FormatFlags & ~StringFormatFlags.LineLimit);
            gr.Font = new Font("Arial", 8, FontStyle.Regular);
            gr.BackColor = Color.Transparent;
            gr.Modifier = BrickModifier.MarginalFooter;
            Image img;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("XtraPrintingDemos.logo.png"))
                img = CreateImage(stream);
            AddUrl(gr.DrawPageImage(img, new RectangleF(0, 0, img.Width, img.Height), BorderSide.None, Color.Transparent));
            RectangleF r = new RectangleF(0, 0, 0, gr.Font.Height);
            string format = "Page: {0} / {1}";
            PageInfoBrick brick = gr.DrawPageInfo(PageInfo.NumberOfTotal, format, Color.Black, r, BorderSide.None);
            brick.Alignment = BrickAlignment.Far;
            brick.Padding = new PaddingInfo(0, 10, 0, 0, GraphicsDpi.Pixel);
            brick.AutoWidth = true;
        }

        private void AddUrl(Brick brick) {
            ((IPageBrick)brick).LineAlignment = BrickAlignment.Center;
            ((IPageBrick)brick).Alignment = BrickAlignment.Center;
            brick.Url = "http://devexpress.com/Products/NET/XtraPrinting/";
        }

        private void CreatePageHeader(BrickGraphics gr) {
            gr.Font = gr.DefaultFont;
            gr.BackColor = Color.Transparent;
            gr.Modifier = BrickModifier.MarginalHeader;

            RectangleF r = new RectangleF(0, 0, 0, gr.Font.Height);

            PageInfoBrick brick = gr.DrawPageInfo(PageInfo.DateTime, string.Empty, Color.Black, r, BorderSide.None);
            brick.Alignment = BrickAlignment.Near;
            brick.Padding = new PaddingInfo(10, 0, 0, 0, GraphicsDpi.Pixel);
            brick.AutoWidth = true;
        }
    }
}
