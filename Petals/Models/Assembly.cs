using dnlib.DotNet;
using dnlib.DotNet.Writer;
using Petals.Enums;
using System.IO;

namespace Petals.Models
{
    public class Assembly
    {
        /// <summary>
        /// Path to the loaded assembly.
        /// </summary>
        public string AssemblyPath { get; set; }

        /// <summary>
        /// The output path of the assembly.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// The assembly extension.
        /// </summary>
        public Extension Extension { get; set; }

        /// <summary>
        /// Module representing the .NET assembly.
        /// </summary>
        public ModuleDefMD Module { get; set; }

        /// <summary>
        /// Module writer options.
        /// </summary>
        public ModuleWriterOptions ModuleWriterOptions { get; set; }

        /// <summary>
        /// Assembly constructor.
        /// </summary>
        /// <param name="assemblyPath">Path to the loaded assembly.</param>
        // TODO: Invalid assembly path handling/exception.
        public Assembly(string assemblyPath)
        {
            AssemblyPath = assemblyPath;
            Module = ModuleDefMD.Load(assemblyPath);
            ModuleWriterOptions = new ModuleWriterOptions(Module);
            Extension = GetExtension();
            OutputPath = GetOutputPath();

            OptimizeModule();
        }

        /// <summary>
        /// Obtain the extension of the assembly.
        /// </summary>
        /// <param name="assemblyPath">The path to the assembly.</param>
        /// <returns>Returns the extension of the given assembly.</returns>
        private Extension GetExtension()
        {
            var extension = AssemblyPath.Substring(AssemblyPath.LastIndexOf('.') + 1).ToUpper();
            return (Extension)System.Enum.Parse(typeof(Extension), extension);
        }

        /// <summary>
        /// Get the output path of the assembly path.
        /// </summary>
        /// <returns>Returns the output path of the protected assembly.</returns>
        private string GetOutputPath()
        {
            // Output folder which will be inside the same folder as the original assembly.
            string protectionFolder = Path.Combine(Path.GetDirectoryName(AssemblyPath), "Protected");

            // Creates a new directory if it doesn't exist.
            if (!Directory.Exists(protectionFolder))
                Directory.CreateDirectory(protectionFolder);

            switch (Extension)
            {
                case Extension.EXE:
                    return Path.Combine(protectionFolder, Path.GetFileNameWithoutExtension(AssemblyPath) + "_obfuscated.exe");

                case Extension.DLL:
                    return Path.Combine(protectionFolder, Path.GetFileNameWithoutExtension(AssemblyPath) + "_obfuscated.dll");

                case Extension.BAT:
                    return Path.Combine(protectionFolder, Path.GetFileNameWithoutExtension(AssemblyPath) + "_obfuscated.bat");

                default:
                    return AssemblyPath;
            }
        }

        /// <summary>
        /// Optimizes the module by simplifying branches and optimizing macros, by using the shorter form if possible.
        /// </summary>
        private void OptimizeModule()
        {
            foreach (TypeDef type in Module.GetTypes())
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;
                    method.Body.SimplifyBranches();
                    method.Body.OptimizeBranches();
                    method.Body.OptimizeMacros();
                }
            }
        }

        /// <summary>
        /// Method to save the module to the output path.
        /// </summary>
        // TODO: Multiple file extension support.
        public void SaveModule()
        {
            // Don't throw any exceptions when saving the module.
            ModuleWriterOptions.MetadataLogger = DummyLogger.NoThrowInstance;

            // Flags for the metadata options.
            ModuleWriterOptions.MetadataOptions.Flags =
                MetadataFlags.AlwaysCreateGuidHeap |
                MetadataFlags.AlwaysCreateStringsHeap |
                MetadataFlags.AlwaysCreateUSHeap |
                MetadataFlags.AlwaysCreateBlobHeap |
                MetadataFlags.PreserveAllMethodRids;

            Module.Write(OutputPath, ModuleWriterOptions);
            Module.Dispose();
        }
    }
}