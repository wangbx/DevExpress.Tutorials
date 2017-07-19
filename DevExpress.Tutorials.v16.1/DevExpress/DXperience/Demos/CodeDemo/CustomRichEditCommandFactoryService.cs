namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.Utils;
    using DevExpress.XtraRichEdit.Commands;
    using DevExpress.XtraRichEdit.Services;
    using System;

    public class CustomRichEditCommandFactoryService : IRichEditCommandFactoryService
    {
        private readonly IRichEditCommandFactoryService service;

        public CustomRichEditCommandFactoryService(IRichEditCommandFactoryService service)
        {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        RichEditCommand IRichEditCommandFactoryService.CreateCommand(RichEditCommandId id)
        {
            if ((!id.Equals(RichEditCommandId.InsertColumnBreak) && !id.Equals(RichEditCommandId.InsertLineBreak)) && !id.Equals(RichEditCommandId.InsertPageBreak))
            {
                return this.service.CreateCommand(id);
            }
            return this.service.CreateCommand(RichEditCommandId.InsertParagraph);
        }
    }
}

