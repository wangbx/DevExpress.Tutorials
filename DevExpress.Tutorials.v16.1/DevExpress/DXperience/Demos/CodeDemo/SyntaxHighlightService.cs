namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.CodeParser;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.API.Native;
    using DevExpress.XtraRichEdit.Internal;
    using DevExpress.XtraRichEdit.Services;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public class SyntaxHighlightService : ISyntaxHighlightService
    {
        private readonly InnerRichEditControl editor;
        private readonly string fileExtensionToHighlight;
        private SyntaxHighlightInfo syntaxHighlightInfo;

        public SyntaxHighlightService(InnerRichEditControl editor, string extension)
        {
            this.editor = editor;
            ((RichEditControl) editor.Owner).LookAndFeel.StyleChanged += new EventHandler(this.LookAndFeel_StyleChanged);
            this.syntaxHighlightInfo = new SyntaxHighlightInfo(((RichEditControl) editor.Owner).LookAndFeel);
            this.fileExtensionToHighlight = extension;
        }

        private ITokenCategoryHelper CreateTokenizer()
        {
            string fileExtensionToHighlight;
            string currentFileName = this.editor.Options.DocumentSaveOptions.CurrentFileName;
            if (string.IsNullOrEmpty(currentFileName))
            {
                fileExtensionToHighlight = this.fileExtensionToHighlight;
            }
            else
            {
                fileExtensionToHighlight = Path.GetExtension(currentFileName);
            }
            ITokenCategoryHelper helper = TokenCategoryHelperFactory.CreateHelperForFileExtensions(fileExtensionToHighlight);
            if (helper != null)
            {
                return helper;
            }
            return null;
        }

        private void CustomHighlightByAttribute(TokenCollection tokens)
        {
            if ((this.TutorialControlBase != null) && (this.TutorialControlBase.SelectedExample != null))
            {
                IEnumerable<string> highlightTokens = this.TutorialControlBase.HighlightTokens;
                if (this.TutorialControlBase.SelectedExample.HighlightTokens != null)
                {
                    highlightTokens = highlightTokens.Union<string>(this.TutorialControlBase.SelectedExample.HighlightTokens);
                }
                if (this.TutorialControlBase.SelectedExample.Parent.HighlightTokens != null)
                {
                    highlightTokens = highlightTokens.Union<string>(this.TutorialControlBase.SelectedExample.Parent.HighlightTokens);
                }
                if ((highlightTokens != null) && (highlightTokens.Count<string>() != 0))
                {
                    foreach (Token token in tokens)
                    {
                        CategorizedToken token2 = token as CategorizedToken;
                        if (((token2 != null) && (token2.Category == TokenCategory.Identifier)) && highlightTokens.Contains<string>(token.Value))
                        {
                            int num = tokens.IndexOf(token) + 1;
                            if ((tokens.Count > num) && (tokens[num].Value != "="))
                            {
                                token2.Category = TokenCategory.Unknown;
                            }
                        }
                    }
                }
            }
        }

        private SyntaxHighlightProperties CustomUnderlineByAttribute(CategorizedToken token, SyntaxHighlightProperties foreColor)
        {
            if (token.Category != TokenCategory.Identifier)
            {
                return foreColor;
            }
            if (this.TutorialControlBase == null)
            {
                return foreColor;
            }
            if (this.TutorialControlBase.SelectedExample == null)
            {
                return foreColor;
            }
            IEnumerable<string> enumerable = this.TutorialControlBase.SelectedExample.UnderlineTokens;
            if (enumerable == null)
            {
                enumerable = this.TutorialControlBase.SelectedExample.Parent.UnderlineTokens;
            }
            else if (this.TutorialControlBase.SelectedExample.Parent.UnderlineTokens != null)
            {
                enumerable = enumerable.Union(this.TutorialControlBase.SelectedExample.Parent.UnderlineTokens);
            }
            if (enumerable == null || enumerable.Count<string>() == 0)
            {
                return foreColor;
            }
            if (enumerable.Contains(token.Value))
            {
                SyntaxHighlightProperties syntaxHighlightProperties = new SyntaxHighlightProperties
                {
                    ForeColor = foreColor.ForeColor
                };
                syntaxHighlightProperties.Underline = new UnderlineType?(UnderlineType.Single);
                syntaxHighlightProperties.UnderlineColor = new Color?(Color.Green);
                return syntaxHighlightProperties;
            }
            return foreColor;
        }

        void ISyntaxHighlightService.Execute()
        {
            this.ExecuteCore();
        }

        void ISyntaxHighlightService.ForceExecute()
        {
            this.ExecuteCore();
        }

        private void ExecuteCore()
        {
            TokenCollection tokens = this.Parse(this.editor.Text);
            this.HighlightSyntax(tokens);
        }

        private void HighlightCategorizedToken(CategorizedToken token, List<SyntaxHighlightToken> syntaxTokens)
        {
            SyntaxHighlightProperties foreColor = this.syntaxHighlightInfo.CalculateTokenCategoryHighlight(token.Category);
            SyntaxHighlightToken item = this.SetTokenColor(token, foreColor);
            if (item != null)
            {
                syntaxTokens.Add(item);
            }
        }

        private void HighlightSyntax(TokenCollection tokens)
        {
            if ((tokens != null) && (tokens.Count != 0))
            {
                Document document = this.editor.Document;
                CharacterProperties properties = document.BeginUpdateCharacters(0, 1);
                List<SyntaxHighlightToken> syntaxTokens = new List<SyntaxHighlightToken>(tokens.Count);
                foreach (Token token in tokens)
                {
                    this.HighlightCategorizedToken((CategorizedToken) token, syntaxTokens);
                }
                document.ApplySyntaxHighlight(syntaxTokens);
                document.EndUpdateCharacters(properties);
            }
        }

        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            this.syntaxHighlightInfo = new SyntaxHighlightInfo(((RichEditControl) this.editor.Owner).LookAndFeel);
            this.ExecuteCore();
        }

        private TokenCollection Parse(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            ITokenCategoryHelper helper = this.CreateTokenizer();
            if (helper == null)
            {
                return new TokenCollection();
            }
            TokenCollection tokens = helper.GetTokens(code);
            this.CustomHighlightByAttribute(tokens);
            return tokens;
        }

        private SyntaxHighlightToken SetTokenColor(CategorizedToken token, SyntaxHighlightProperties foreColor)
        {
            if (this.editor.Document.Paragraphs.Count < token.Range.Start.Line)
            {
                return null;
            }
            foreColor = this.CustomUnderlineByAttribute(token, foreColor);
            int paragraphStart = DocumentHelper.GetParagraphStart(this.editor.Document.Paragraphs[token.Range.Start.Line - 1]);
            int start = (paragraphStart + token.Range.Start.Offset) - 1;
            if (token.Range.End.Line != token.Range.Start.Line)
            {
                paragraphStart = DocumentHelper.GetParagraphStart(this.editor.Document.Paragraphs[token.Range.End.Line - 1]);
            }
            int num3 = (paragraphStart + token.Range.End.Offset) - 1;
            return new SyntaxHighlightToken(start, num3 - start, foreColor);
        }

        public CodeTutorialControlBase TutorialControlBase
        {
            get
            {
                if (this.editor == null)
                {
                    return null;
                }
                if (this.editor.Owner == null)
                {
                    return null;
                }
                Control owner = this.editor.Owner as Control;
                while ((owner != null) && !(owner is CodeTutorialControlBase))
                {
                    owner = owner.Parent;
                }
                return (owner as CodeTutorialControlBase);
            }
        }
    }
}

