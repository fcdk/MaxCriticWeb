using System;

namespace CriticWeb.DataLayer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IdColumnNameAttribute : Attribute
    {
        public string Name;
        public IdColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
}
