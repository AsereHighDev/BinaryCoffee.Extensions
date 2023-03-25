using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mvvm.Extensions.Generator;
using Mvvm.Extensions.Generator.Attributes;
using Xunit;

namespace Mvvm.Extensions.UnitTests
{
    public static class Test
    {
        [Fact]
        public static void ShouldGenerateCode()
        {
            var root = CSharpSyntaxTree.ParseText(@"namespace Test; 
public interface IUser
{
    string AgeStatus => $""You're {(Is18 ? """" : ""not "")}old enough"";
    string FirstName { get; set; }
    string? LastName { get; set; }
    [Ignore]
    ITest Test {get;}
    string Name => $""{FirstName} {LastName}"".Trim();
    int Age { get;set }
    bool Is18 { get => Age == 18; set => Age = value ? 18 : Age; }
    AsyncRelayCommand<string?> AddManagerCommand { get; }
}
public interface ITest {}").GetRoot();
            var e = CSharpCompilation.Create("Test", 
                new[] { root.SyntaxTree }, 
                new[] {
                    MetadataReference.CreateFromFile(typeof(AsyncRelayCommandOptions).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(CommandOptionsAttribute).Assembly.Location)
                });
            var sm = e.GetSemanticModel(root.SyntaxTree);
            ITypeSymbol iface = sm.GetDeclaredSymbol(root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().First())!;
            var (_, script) = ViewModelGenerator.CreateRelatedTypeFiles(iface, sm);
            Trace.WriteLine(script);
        }

        [Fact]
        public static void ShouldGenerateUser()
        {
            //User r = new() { };
            //int i = 0;
            //r.PropertyChanged += (o, e) =>
            //{
            //    switch (i++)
            //    {
            //        case 0:
            //            e.PropertyName.Should().Be(nameof(IUser.FirstName));
            //            break;
            //        case 1:
            //            e.PropertyName.Should().Be(nameof(IUser.Name));
            //            break;
            //    }
            //};
            //r.FirstName = "Pedro";
            //r.LastName = "Gil";
            //r.Name.Should().Be("Pedro Gil");
        }
    }
}
