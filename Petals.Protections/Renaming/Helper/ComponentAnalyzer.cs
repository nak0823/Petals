using dnlib.DotNet;
using Petals.Protections.Renaming.Helper.Constants;

namespace Petals.Protections.Renaming.Helper
{
    /// <summary>
    /// Provides methods to analyze components for renaming eligibility.
    /// </summary>
    public class ComponentAnalyzer
    {
        /// <summary>
        /// Determines whether a <see cref="TypeDef"/> can be renamed.
        /// </summary>
        /// <param name="typeDef">The <see cref="TypeDef"/> to analyze.</param>
        /// <returns><see langword="true"/> if the <see cref="TypeDef"/> can be renamed; otherwise, <see langword="false"/>.</returns>
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

        /// <summary>
        /// Determines whether an <see cref="EventDef"/> can be renamed.
        /// </summary>
        /// <param name="eventDef">The <see cref="EventDef"/> to analyze.</param>
        /// <returns><see langword="true"/> if the <see cref="EventDef"/> can be renamed; otherwise, <see langword="false"/>.</returns>
        public static bool CanRename(EventDef eventDef)
        {
            if (eventDef.IsRuntimeSpecialName ||
                eventDef.IsSpecialName) return false;

            return true;
        }

        /// <summary>
        /// Determines whether a <see cref="FieldDef"/> in the context of a <see cref="TypeDef"/> can be renamed.
        /// </summary>
        /// <param name="typeDef">The <see cref="TypeDef"/> containing the <see cref="FieldDef"/>.</param>
        /// <param name="fieldDef">The <see cref="FieldDef"/> to analyze.</param>
        /// <returns><see langword="true"/> if the <see cref="FieldDef"/> can be renamed; otherwise, <see langword="false"/>.</returns>
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

        /// <summary>
        /// Determines whether a <see cref="PropertyDef"/> in the context of a <see cref="TypeDef"/> can be renamed.
        /// </summary>
        /// <param name="typeDef">The <see cref="TypeDef"/> containing the <see cref="PropertyDef"/>.</param>
        /// <param name="property">The <see cref="PropertyDef"/> to analyze.</param>
        /// <returns><see langword="true"/> if the <see cref="PropertyDef"/> can be renamed; otherwise, <see langword="false"/>.</returns>
        public static bool CanRename(TypeDef typeDef, PropertyDef property)
        {
            if (typeDef.Namespace.String.Contains(TypeDefConstants.PropertyIdentifier) ||
                property.DeclaringType.Name.Contains(PropertyDefConstants.AnonymousType) ||
                property.IsRuntimeSpecialName ||
                property.IsEmpty ||
                property.IsSpecialName) return false;

            return true;
        }

        /// <summary>
        /// Determines whether a <see cref="Parameter"/> in the context of a <see cref="TypeDef"/> can be renamed.
        /// </summary>
        /// <param name="typeDef">The <see cref="TypeDef"/> containing the <see cref="Parameter"/>.</param>
        /// <param name="parameter">The <see cref="Parameter"/> to analyze.</param>
        /// <returns><see langword="true"/> if the <see cref="Parameter"/> can be renamed; otherwise, <see langword="false"/>.</returns>
        public static bool CanRename(TypeDef typeDef, Parameter parameter)
        {
            return !(typeDef.FullName == TypeDefConstants.ModuleIdentifier ||
                     parameter.IsHiddenThisParameter ||
                     parameter.Name == string.Empty);
        }

        /// <summary>
        /// Determines whether a <see cref="MethodDef"/> can be renamed.
        /// </summary>
        /// <param name="method">The <see cref="MethodDef"/> to analyze.</param>
        /// <returns><see langword="true"/> if the <see cref="MethodDef"/> can be renamed; otherwise, <see langword="false"/>.</returns>
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