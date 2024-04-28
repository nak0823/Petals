using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Petals.Models;
using Petals.Protections.Renaming.Helper;

namespace Petals.Protections.Renaming
{
    public class Renamer
    {
        /// <summary>
        /// Boolean value indicating whether to rename types.
        /// </summary>
        public bool Types { get; set; }

        /// <summary>
        /// Boolean value indicating whether to rename properties.
        /// </summary>
        public bool Properties { get; set; }

        /// <summary>
        /// Boolean value indicating whether to rename methods.
        /// </summary>
        public bool Methods { get; set; }

        /// <summary>
        /// Boolean value indicating whether to rename events.
        /// </summary>
        public bool Events { get; set; }

        /// <summary>
        /// Boolean value indicating whether to rename fields.
        /// </summary>
        public bool Fields { get; set; }

        /// <summary>
        /// Boolean value indicating whether to rename parameters.
        /// </summary>
        public bool Parameters { get; set; }

        public Renamer(bool types, bool properties, bool methods, bool events, bool fields, bool parameters)
        {
            Types = types;
            Properties = properties;
            Methods = methods;
            Events = events;
            Fields = fields;
            Parameters = parameters;
        }

        /// <summary>
        /// Function to rename various components of an assembly.
        /// </summary>
        /// <param name="assembly">The to-be obfuscated assembly.</param>
        public void Rename(Assembly assembly)
        {
            foreach (TypeDef typeDef in assembly.Module.Types)
            {
                PropertyPhase(typeDef);
                FieldPhase(typeDef);
                MethodPhase(typeDef);
                EventPhase(typeDef);
                ParameterPhase(typeDef);
                TypePhase(assembly, typeDef);
            }
        }

        /// <summary>
        /// Method to rename the types of the assembly.
        /// </summary>
        /// <param name="assembly">The to-be obfuscated assembly.</param>
        /// <param name="typeDef">The type definition.</param>
        private void TypePhase(Assembly assembly, TypeDef typeDef)
        {
            if (Types)
            {
                if (ComponentAnalyzer.CanRename(typeDef))
                {
                    string formNamespace = StringGenerator.Generate(16);
                    string formName = StringGenerator.Generate(16);

                    foreach (MethodDef method in typeDef.Methods)
                    {
                        if (typeDef.BaseType != null && typeDef.BaseType.Name == "Form")
                        {
                            foreach (Resource resource in assembly.Module.Resources)
                            {
                                if (resource.Name.Contains(typeDef.Name + ".resources"))
                                {
                                    resource.Name = formNamespace + "." + formName + ".resources";
                                }
                            }
                        }

                        typeDef.Namespace = formNamespace;
                        typeDef.Name = formName;

                        if (method.Name.Equals("InitializeComponent") && method.HasBody)
                        {
                            foreach (Instruction instruction in method.Body.Instructions)
                            {
                                if (instruction.OpCode.Equals(OpCodes.Ldstr))
                                {
                                    string str = (string)instruction.Operand;
                                    if (str == typeDef.Name)
                                    {
                                        instruction.Operand = formName;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to rename the parameters of the assembly.
        /// </summary>
        /// <param name="typeDef">The type definition.</param>
        private void ParameterPhase(TypeDef typeDef)
        {
            if (Parameters)
            {
                foreach (MethodDef method in typeDef.Methods)
                {
                    foreach (Parameter parameter in method.Parameters)
                    {
                        if (ComponentAnalyzer.CanRename(typeDef, parameter))
                            parameter.Name = StringGenerator.Generate(16);
                    }
                }
            }
        }

        /// <summary>
        /// Method to rename the events of the assembly.
        /// </summary>
        /// <param name="typeDef">The type definition.</param>
        private void EventPhase(TypeDef typeDef)
        {
            if (Events)
            {
                foreach (EventDef eventDef in typeDef.Events)
                {
                    if (ComponentAnalyzer.CanRename(eventDef))
                        eventDef.Name = StringGenerator.Generate(16);
                }
            }
        }

        /// <summary>
        /// Method to rename the methods of the assembly.
        /// </summary>
        /// <param name="typeDef">The type definition.</param>
        private void MethodPhase(TypeDef typeDef)
        {
            if (Methods)
            {
                foreach (MethodDef method in typeDef.Methods)
                {
                    if (ComponentAnalyzer.CanRename(method))
                        method.Name = StringGenerator.Generate(16);
                }
            }
        }

        /// <summary>
        /// Method to rename the fields of the assembly.
        /// </summary>
        /// <param name="typeDef">The type definition.</param>
        private void FieldPhase(TypeDef typeDef)
        {
            if (Fields)
            {
                foreach (FieldDef field in typeDef.Fields)
                {
                    if (ComponentAnalyzer.CanRename(typeDef, field))
                        field.Name = StringGenerator.Generate(16);
                }
            }
        }

        /// <summary>
        /// Method to rename the properties of the assembly.
        /// </summary>
        /// <param name="typeDef">The type definition.</param>
        private void PropertyPhase(TypeDef typeDef)
        {
            if (Properties)
            {
                foreach (PropertyDef property in typeDef.Properties)
                {
                    if (ComponentAnalyzer.CanRename(typeDef, property))
                        property.Name = StringGenerator.Generate(16);
                }
            }
        }
    }
}