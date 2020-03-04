using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace CodeGenerationSample
{
    public static class CodeGenerator
    {
        public static CodeCompileUnit BuildCode()
        {
            var unit = new CodeCompileUnit();

            var samplesNamespace = new CodeNamespace("Samples");
            unit.Namespaces.Add(samplesNamespace);

            samplesNamespace.Imports.Add(new CodeNamespaceImport(nameof(System)));

            var sampleClass = new CodeTypeDeclaration("MyClass");
            samplesNamespace.Types.Add(sampleClass);

            var entryPointMethod = new CodeEntryPointMethod();
            entryPointMethod.Statements.Add(new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression($"{nameof(System)}.{nameof(System.Console)}"),
                nameof(System.Console.WriteLine),
                new CodePrimitiveExpression("Hello world!")
            ));

            entryPointMethod.Statements.Add(new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression($"{nameof(System)}.{nameof(System.Console)}"),
                nameof(System.Console.WriteLine),
                new CodePrimitiveExpression("Press any key to continue")
            ));

            entryPointMethod.Statements.Add(new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression($"{nameof(System)}.{nameof(System.Console)}"),
                nameof(System.Console.ReadLine)
            ));

            sampleClass.Members.Add(entryPointMethod);

            return unit;
        }

        public static void GenerateCode(
            CodeDomProvider provider,
            string sourceFileName,
            CodeCompileUnit unit)
        {
            using var textWriter = new IndentedTextWriter(
                new StreamWriter(sourceFileName, append: false),
                "   "
            );

            provider.GenerateCodeFromCompileUnit(
                unit,
                textWriter,
                new CodeGeneratorOptions()
            );

            textWriter.Close();
        }

        public static CompilerResults CompileCode(
            CodeDomProvider provider,
            string sourceFile,
            string exeFile)
        {
            var referenceAssemblies = new[] { "System.dll" };
            var compilerParameters = new CompilerParameters(
                referenceAssemblies,
                exeFile,
                includeDebugInformation: false)
            {
                GenerateExecutable = true
            };

            return provider.CompileAssemblyFromFile(
                compilerParameters,
                sourceFile
            );
        }
    }
}
