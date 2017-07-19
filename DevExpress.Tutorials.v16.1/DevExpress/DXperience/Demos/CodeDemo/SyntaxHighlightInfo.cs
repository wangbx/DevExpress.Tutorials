namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.CodeParser;
    using DevExpress.LookAndFeel;
    using DevExpress.Utils;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraRichEdit.API.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class SyntaxHighlightInfo
    {
        private readonly Dictionary<TokenCategory, SyntaxHighlightProperties> properties;
        private readonly UserLookAndFeel userLookAndFeel;

        public SyntaxHighlightInfo(UserLookAndFeel userLookAndFeel)
        {
            this.userLookAndFeel = userLookAndFeel;
            this.properties = new Dictionary<TokenCategory, SyntaxHighlightProperties>();
            this.Reset();
        }

        private void Add(TokenCategory category, Color foreColor)
        {
            SyntaxHighlightProperties properties = new SyntaxHighlightProperties {
                ForeColor = new Color?(foreColor)
            };
            this.properties.Add(category, properties);
        }

        public SyntaxHighlightProperties CalculateTokenCategoryHighlight(TokenCategory category)
        {
            SyntaxHighlightProperties properties = null;
            if (this.properties.TryGetValue(category, out properties))
            {
                return properties;
            }
            return this.properties[TokenCategory.Text];
        }

        private Color GetColor(Color color)
        {
            if (((FrameHelper.IsDarkSkin(this.userLookAndFeel) && (this.userLookAndFeel.ActiveSkinName != "Dark Side")) && ((this.userLookAndFeel.ActiveSkinName != "Office 2010 Black") && (this.userLookAndFeel.ActiveSkinName != "Sharp"))) && (this.userLookAndFeel.ActiveSkinName != "Office 2016 Dark"))
            {
                if (color == Color.Blue)
                {
                    return Color.DeepSkyBlue;
                }
                if (color == Color.Brown)
                {
                    return Color.LightCoral;
                }
                if (color == Color.Green)
                {
                    return Color.LightGreen;
                }
                if (color == Color.Red)
                {
                    return Color.LightPink;
                }
                if (color == Color.Gray)
                {
                    return Color.Silver;
                }
                if (color == Color.FromArgb(0x2b, 0x91, 0xaf))
                {
                    return Color.Aqua;
                }
            }
            return color;
        }

        public void Reset()
        {
            this.properties.Clear();
            this.Add(TokenCategory.Text, DXColor.Empty);
            this.Add(TokenCategory.Keyword, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.String, this.GetColor(DXColor.Brown));
            this.Add(TokenCategory.Comment, this.GetColor(DXColor.Green));
            this.Add(TokenCategory.Identifier, DXColor.Empty);
            this.Add(TokenCategory.PreprocessorKeyword, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.Number, this.GetColor(DXColor.Red));
            this.Add(TokenCategory.Operator, DXColor.Empty);
            this.Add(TokenCategory.Unknown, this.GetColor(Color.FromArgb(0x2b, 0x91, 0xaf)));
            this.Add(TokenCategory.XmlComment, this.GetColor(DXColor.Gray));
            this.Add(TokenCategory.CssComment, this.GetColor(DXColor.Green));
            this.Add(TokenCategory.CssKeyword, this.GetColor(DXColor.Brown));
            this.Add(TokenCategory.CssPropertyName, this.GetColor(DXColor.Red));
            this.Add(TokenCategory.CssPropertyValue, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.CssSelector, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.CssStringValue, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.HtmlAttributeName, this.GetColor(DXColor.Red));
            this.Add(TokenCategory.HtmlAttributeValue, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.HtmlComment, this.GetColor(DXColor.Green));
            this.Add(TokenCategory.HtmlElementName, this.GetColor(DXColor.Brown));
            this.Add(TokenCategory.HtmlEntity, this.GetColor(DXColor.Gray));
            this.Add(TokenCategory.HtmlOperator, DXColor.Empty);
            this.Add(TokenCategory.HtmlServerSideScript, DXColor.Empty);
            this.Add(TokenCategory.HtmlString, this.GetColor(DXColor.Blue));
            this.Add(TokenCategory.HtmlTagDelimiter, this.GetColor(DXColor.Blue));
        }
    }
}

