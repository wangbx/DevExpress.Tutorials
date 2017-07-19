namespace DevExpress.DXperience.Demos
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly)]
    public class ProductIdAttribute : Attribute
    {
        private string productId;

        public ProductIdAttribute(string productId)
        {
            this.ProductId = productId;
        }

        public string ProductId
        {
            get
            {
                return this.productId;
            }
            set
            {
                this.productId = value;
            }
        }
    }
}

