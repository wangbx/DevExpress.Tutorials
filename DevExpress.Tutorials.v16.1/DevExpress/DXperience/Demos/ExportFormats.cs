namespace DevExpress.DXperience.Demos
{
    using System;

    [Flags]
    public enum ExportFormats
    {
        All = 0xffff,
        DOC = 0x20,
        DOCX = 0x40,
        EPUB = 2,
        HTML = 8,
        Image = 0x800,
        ImageEx = 0x2000,
        MHT = 0x10,
        None = 0,
        ODT = 0x400,
        PDF = 1,
        RTF = 0x200,
        Text = 0x1000,
        XLS = 0x80,
        XLSX = 0x100,
        XML = 4
    }
}

