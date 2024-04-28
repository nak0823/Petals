namespace Petals.Protections.Renaming.Helper.Constants
{
    /// <summary>
    /// Contains constant values related to <see cref="ComponentAnalyzer"/>.
    /// </summary>
    public class TypeDefConstants
    {
        /// <summary>
        /// Costura's namespace used for embedding resources.
        /// </summary>
        public static readonly string CosturaNamespace = "Costura";

        /// <summary>
        /// MSVC's automatically generated property file/folder.
        /// </summary>
        public static readonly string PropertyIdentifier = ".Properties";

        /// <summary>
        /// Resource.
        /// </summary>
        public static readonly string SpecialPrefix = "<";

        /// <summary>
        /// Module identifier.
        /// </summary>
        public static readonly string ModuleIdentifier = "<Module>";
    }
}