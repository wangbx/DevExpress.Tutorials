namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method)]
    public class CodeExampleGroupName : CategoryAttribute
    {
        public CodeExampleGroupName(string groupName) : base(groupName)
        {
            this.GroupName = groupName;
        }

        public string GroupName { get; set; }
    }
}

