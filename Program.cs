using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CodeGenerationSample
{
    public static class Program
    {
        public static void Main()
        {
            var unit = CodeGenerator.BuildCode();
            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            const string baseFileName = "SampleGeneratedFile";
            var sourceFileName = $"{SourceDirectory}/{baseFileName}.{codeDomProvider.FileExtension}";

            CodeGenerator.GenerateCode(
                codeDomProvider,
                sourceFileName,
                unit
            );

            var compilerResults = CodeGenerator.CompileCode(
                codeDomProvider,
                sourceFileName,
                $"{baseFileName}.exe"
            );

            foreach (var error in compilerResults.Errors)
                Console.WriteLine(error);
        }

        private static string SourceDirectory =>
            new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)")
            .Match(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))
            .Value;
    }
}
