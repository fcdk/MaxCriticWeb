using System;

namespace CriticWeb.DataLayer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class NameColumnNameAttribute : Attribute
    {
        public string Name;
        public NameColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
}
