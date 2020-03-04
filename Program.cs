using System;
using System.CodeDom.Compiler;
using System.IO;

namespace CodeGenerationSample
{
    public static class Program
    {
        public static void Main()
        {
            var unit = CodeGenerator.BuildCode();
            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            const string baseFileName = "SampleGeneratedFile";
            var sourceFileName = Path.Join(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}",
                $"{baseFileName}.{codeDomProvider.FileExtension}");

            CodeGenerator.GenerateCode(
                codeDomProvider,
                sourceFileName,
                unit
            );
        }
    }
}
