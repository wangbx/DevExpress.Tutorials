namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    internal class GroupNode
    {
        public GroupNode(string name, IList children)
        {
            this.Name = name;
            this.Children = children;
        }

        public IList Children { get; set; }

        public string Name { get; set; }
    }
}

