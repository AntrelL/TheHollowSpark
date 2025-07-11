using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;

namespace RainyPlace.Misc.Editor
{
    public static class SceneNamesCodeDomGenerator
    {
        private const string OutputPath = "Assets/_Source/Scripts/Game/Runtime/Generated/SceneName.cs";
        private const string NamespaceName = "TheHollowSpark";
        private const string ClassName = "SceneName";

        [MenuItem("Tools/Rainy Place/Generate Scene Names")]
        public static void Generate()
        {
            CodeCompileUnit compileUnit = CreateCompileUnit();
            WriteToFile(compileUnit);
            AssetDatabase.Refresh();
        }
        
        private static CodeCompileUnit CreateCompileUnit()
        {
            var compileUnit = new CodeCompileUnit();
            var codeNamespace = new CodeNamespace(NamespaceName);
            compileUnit.Namespaces.Add(codeNamespace);

            var typeDeclaration = new CodeTypeDeclaration(ClassName)
            {
                IsClass = true,
                IsPartial = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };
            codeNamespace.Types.Add(typeDeclaration);

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled == false)
                    continue;

                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                string safeName = MakeSafeIdentifier(sceneName);
                
                CodeMemberField field = CreateSceneField(safeName, sceneName);
                typeDeclaration.Members.Add(field);
            }

            return compileUnit;
        }
        
        private static CodeMemberField CreateSceneField(string fieldName, string value)
        {
            return new CodeMemberField(typeof(string), fieldName)
            {
                Attributes = MemberAttributes.Const | MemberAttributes.Public,
                InitExpression = new CodePrimitiveExpression(value)
            };
        }
        
        private static void WriteToFile(CodeCompileUnit compileUnit)
        {
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions
            {
                BracingStyle = "C",
                IndentString = "    ",
                BlankLinesBetweenMembers = false
            };

            Directory.CreateDirectory(Path.GetDirectoryName(OutputPath) ?? string.Empty);

            using var writer = new StreamWriter(OutputPath, false);
            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
        }

        private static string MakeSafeIdentifier(string name)
        {
            const char InvalidCharReplacement = '_';
            var builder = new StringBuilder();
            
            foreach (char character in name)
            {
                bool isValid = char.IsLetterOrDigit(character) || character == InvalidCharReplacement;
                builder.Append(isValid ? character : InvalidCharReplacement);
            }
            
            if (builder.Length > 0 && char.IsDigit(builder[0]))
            {
                builder.Insert(0, InvalidCharReplacement);
            }

            return builder.ToString();
        }
    }
}
