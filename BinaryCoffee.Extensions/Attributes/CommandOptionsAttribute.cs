using CommunityToolkit.Mvvm.Input;
using System;
namespace Mvvm.Extensions.Generator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CommandOptionsAttribute : Attribute
    {
        public CommandOptionsAttribute(bool canExecute, AsyncRelayCommandOptions asyncOption = AsyncRelayCommandOptions.None) { }
    }
}
