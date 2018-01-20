using System;

namespace TriviaApi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SetOnlyJsonPropertyAttribute : Attribute
    {
    }
}