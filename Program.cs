using System;
using System.CodeDom.Compiler;

namespace CodeGenerationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var unit = CodeGenerator.BuildCode();
            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            const string baseFileName = "SampleGeneratedFile";
            var sourceFileName = $"{baseFileName}.{codeDomProvider.FileExtension}";

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
    }
}
