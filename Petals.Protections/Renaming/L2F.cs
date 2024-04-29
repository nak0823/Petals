using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Petals.Models;
using Petals.Protections.Renaming.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Petals.Protections.Renaming
{
    public class L2F : IProtection
    {
        /// <summary>
        /// Dictionary to store simplified locals (has body & instructions).
        /// </summary>
        private static Dictionary<Local, FieldDef> ProcessedLocals = new Dictionary<Local, FieldDef>();

        public override void Protect(Assembly assembly)
        {
            foreach (TypeDef typeDef in assembly.Module.Types.Where(type => type != assembly.Module.GlobalType))
            {
                foreach (MethodDef methodDef in typeDef.Methods.Where(methodDef => methodDef.HasBody && methodDef.Body.HasInstructions && !methodDef.IsConstructor))
                {
                    ProcessedLocals = new Dictionary<Local, FieldDef>();
                    ProcessMethod(assembly.Module, methodDef);
                }
            }
        }

        /// <summary>
        /// Process the method and replace the locals with fields.
        /// </summary>
        /// <param name="moduleDef"></param>
        /// <param name="methodDef"></param>
        private static void ProcessMethod(ModuleDef moduleDef, MethodDef methodDef)
        {
            IList<Instruction> instructions = methodDef.Body.Instructions;

            foreach (Instruction instruction in instructions)
            {
                if (!(instruction.Operand is Local local)) continue;

                if (!ProcessedLocals.ContainsKey(local))
                {
                    string fieldName = StringGenerator.Generate(16);

                    FieldDef def = new FieldDefUser(fieldName, new FieldSig(local.Type), FieldAttributes.Public | FieldAttributes.Static);

                    moduleDef.GlobalType.Fields.Add(def);

                    ProcessedLocals.Add(local, def);
                }
                else
                {
                    FieldDef def = ProcessedLocals[local];
                }

                switch (instruction.OpCode?.Code)
                {
                    case Code.Ldloc:
                        instruction.OpCode = OpCodes.Ldsfld;
                        break;

                    case Code.Ldloca:
                        instruction.OpCode = OpCodes.Ldsflda;
                        break;

                    case Code.Stloc:
                        instruction.OpCode = OpCodes.Stsfld;
                        break;

                    default:
                        instruction.OpCode = null;
                        break;
                }

                instruction.Operand = ProcessedLocals[local];
            }

            ProcessedLocals.Keys.ToList().ForEach(x => methodDef.Body.Variables.Remove(x));

            ProcessedLocals = new Dictionary<Local, FieldDef>();
        }
    }
}