using dnlib.DotNet;
using Petals.Protections.Renaming.Helper.Constants;

namespace Petals.Protections.Renaming.Helper
{
    public class ComponentAnalyzer
    {
        public static bool CanRename(TypeDef typeDef)
        {
            if (typeDef.Namespace == TypeDefConstants.CosturaNamespace ||
                typeDef.Name.StartsWith(TypeDefConstants.SpecialPrefix) ||
                typeDef.IsGlobalModuleType ||
                typeDef.IsInterface ||
                typeDef.IsForwarder ||
                typeDef.IsSerializable ||
                typeDef.IsEnum ||
                typeDef.IsRuntimeSpecialName ||
                typeDef.IsSpecialName ||
                typeDef.IsWindowsRuntime ||
                typeDef.IsNestedFamilyAndAssembly ||
                typeDef.IsNestedFamilyOrAssembly) return false;

            return true;
        }

        public static bool CanRename(EventDef eventDef)
        {
            if (eventDef.IsRuntimeSpecialName ||
                eventDef.IsSpecialName) return false;

            return true;
        }

        public static bool CanRename(TypeDef typeDef, FieldDef fieldDef)
        {
            return !(typeDef.Namespace.String.Contains(TypeDefConstants.PropertyIdentifier) ||
              (fieldDef.DeclaringType.IsSerializable && !fieldDef.IsNotSerialized) ||
              fieldDef.DeclaringType.BaseType.Name.Contains(FieldDefConstants.Delegate) ||
              fieldDef.Name.StartsWith(FieldDefConstants.SpecialPrefix) ||
              fieldDef.IsLiteral && fieldDef.DeclaringType.IsEnum ||
              fieldDef.IsFamilyOrAssembly ||
              fieldDef.IsSpecialName ||
              fieldDef.IsRuntimeSpecialName ||
              fieldDef.IsFamily ||
              fieldDef.DeclaringType.IsEnum);
        }

        public static bool CanRename(TypeDef typeDef, PropertyDef property)
        {
            if (typeDef.Namespace.String.Contains(TypeDefConstants.PropertyIdentifier) ||
                property.DeclaringType.Name.Contains(PropertyDefConstants.AnonymousType) ||
                property.IsRuntimeSpecialName ||
                property.IsEmpty ||
                property.IsSpecialName) return false;

            return true;
        }

        public static bool CanRename(TypeDef typeDef, Parameter parameter)
        {
            return !(typeDef.FullName == TypeDefConstants.ModuleIdentifier ||
                     parameter.IsHiddenThisParameter ||
                     parameter.Name == string.Empty);
        }

        public static bool CanRename(MethodDef method)
        {
            return !(method == null ||
                     !method.HasBody || !method.Body.HasInstructions ||
                     (method.DeclaringType.BaseType != null && method.DeclaringType.BaseType.Name.Contains(MethodDefConstants.DelegateTypeName)) ||
                     (method.DeclaringType.FullName == MethodDefConstants.SystemWindowsFormsBindingFullName && method.Name.String == MethodDefConstants.SystemWindowsFormsBindingCtorName) ||
                     method.Name == MethodDefConstants.InvokeMethodName ||
                     method.IsSetter || method.IsGetter ||
                     method.IsSpecialName ||
                     method.IsFamilyAndAssembly ||
                     method.IsFamily ||
                     method.IsRuntime ||
                     method.IsRuntimeSpecialName ||
                     method.IsConstructor ||
                     method.IsNative ||
                     method.IsPinvokeImpl || method.IsUnmanaged || method.IsUnmanagedExport ||
                     method.Name.StartsWith(MethodDefConstants.SpecialPrefix) ||
                     method.Overrides.Count > 0 ||
                     method.IsStaticConstructor ||
                     method.DeclaringType.IsGlobalModuleType ||
                     method.DeclaringType.IsForwarder ||
                     method.IsVirtual ||
                     method.HasImplMap);
        }
    }
}