namespace Petals.Models
{
    public class Settings
    {
        public RenamingSettings Renaming { get; set; }
        public L2FSettings LocalToField { get; set; }
        public class RenamingSettings
        {
            public bool Enabled { get; set; }
            public bool Types { get; set; }
            public bool Properties { get; set; }
            public bool Methods { get; set; }
            public bool Events { get; set; }
            public bool Fields { get; set; }
            public bool Parameters { get; set; }
        }

        public class L2FSettings
        {
            public bool Enabled { get; set; }
        }
    }
}