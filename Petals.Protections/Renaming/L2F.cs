using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Petals.Models;
using Petals.Protections.Renaming.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Petals.Protections.Renaming
{
    public class L2F
    {
        private static Dictionary<Local, FieldDef> ProcessedLocals = new Dictionary<Local, FieldDef>();

        public static void Protect(Assembly assembly)
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

        private static void ProcessMethod(ModuleDef moduleDef, MethodDef methodDef)
        {
            methodDef.Body.SimplifyMacros(methodDef.Parameters);
            var instructions = methodDef.Body.Instructions;
            foreach (var t in instructions)
            {
                if (!(t.Operand is Local local)) continue;
                FieldDef def;
                if (!ProcessedLocals.ContainsKey(local))
                {
                    def = new FieldDefUser(StringGenerator.Generate(16), new FieldSig(local.Type), FieldAttributes.Public | FieldAttributes.Static);
                    moduleDef.GlobalType.Fields.Add(def);
                    ProcessedLocals.Add(local, def);
                }
                else
                {
                    def = ProcessedLocals[local];
                }

                var eq = t.OpCode?.Code;
                switch (eq)
                {
                    case Code.Ldloc:
                        t.OpCode = OpCodes.Ldsfld;
                        break;

                    case Code.Ldloca:
                        t.OpCode = OpCodes.Ldsflda;
                        break;

                    case Code.Stloc:
                        t.OpCode = OpCodes.Stsfld;
                        break;

                    default:
                        t.OpCode = null;
                        break;
                }
                t.Operand = def;
            }
            ProcessedLocals.ToList().ForEach(x => methodDef.Body.Variables.Remove(x.Key));
            ProcessedLocals = new Dictionary<Local, FieldDef>();
        }
    }
}