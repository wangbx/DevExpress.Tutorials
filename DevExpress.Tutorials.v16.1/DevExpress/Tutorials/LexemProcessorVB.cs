namespace DevExpress.Tutorials
{
    using System;

    public class LexemProcessorVB : LexemProcessorCode
    {
        protected override string[] GetKeywords()
        {
            return new string[] { 
                "AddHandler", "And", "Auto", "ByVal", "CBool", "CDec", "Class", "CShort", "Date", "Delegate", "Double", "End", "Event", "For", "GetType", "Implements",
                "Integer", "Lib", "Me", "MustOverride", "New", "NotInheritable", "Option", "Overloads", "Preserve", "Public", "REM", "Select", "Short", "Stop", "SyncLock", "True",
                "Until", "With", "AddressOf", "Ansi", "Boolean", "Call", "CByte", "CDbl", "CLng", "CSng", "Decimal", "Dim", "Each", "Enum", "Exit", "Friend",
                "GoTo", "Imports", "Interface", "Like", "Mod", "MyBase", "Next", "NotOverridable", "Optional", "Overridable", "Private", "RaiseEvent", "RemoveHandler", "Set", "Single", "String",
                "Then", "Try", "Variant", "WithEvents", "AndAlso", "As", "ByRef", "Case", "CChar", "Char", "CObj", "CStr", "Declare", "DirectCast", "Else", "Erase",
                "False", "Function", "Handles", "In", "Is", "Long", "Module", "MyClass", "Not", "Object", "Or", "Overrides", "Property", "ReadOnly", "Resume", "Shadows",
                "Static", "Structure", "Throw", "TypeOf", "When", "WriteOnly", "Alias", "Assembly", "Byte", "Catch", "CDate", "CInt", "Const", "CType", "Default", "Do",
                "ElseIf", "Error", "Finally", "Get", "If", "Inherits", "Let", "Loop", "MustInherit", "Namespace", "Nothing", "Or", "OrElse", "ParamArray", "Protected", "ReDim",
                "Return", "Shared", "Step", "Sub", "To", "Unicode", "While", "Xor"
            };
        }

        protected override bool IsStartCommentString(string s)
        {
            return (s == "'");
        }
    }
}

