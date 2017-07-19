namespace DevExpress.Tutorials
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class FactoryBase
    {
        private Hashtable registeredEntries = new Hashtable();

        public FactoryBase()
        {
            this.RegisterEntries();
        }

        public ConstructorInfo GetConstructorByKind(string kind)
        {
            if (this.registeredEntries.ContainsKey(kind))
            {
                Type type = this.registeredEntries[kind] as Type;
                return type.GetConstructor(this.GetConstructorParamTypes());
            }
            return null;
        }

        protected virtual Type[] GetConstructorParamTypes()
        {
            return new Type[0];
        }

        protected virtual void RegisterEntries()
        {
        }

        protected void RegisterEntry(string kind, Type type)
        {
            this.registeredEntries.Add(kind, type);
        }
    }
}

