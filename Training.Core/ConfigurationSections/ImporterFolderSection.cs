using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.ConfigurationSections
{
    public class ImporterFoldersSection : ConfigurationSection
    {
        [ConfigurationProperty("importRootFolder")]
        public FolderElement ImportRootFolder
        {
            get { return (FolderElement)this["importRootFolder"]; }
            set { this["importRootFolder"] = value; }
        }

        [ConfigurationProperty("zippedFolder")]
        public FolderElement ZippedFolder
        {
            get { return (FolderElement)this["zippedFolder"]; }
            set { this["zippedFolder"] = value; }
        }

        [ConfigurationProperty("initialFolder")]
        public FolderElement InitialFolder
        {
            get { return (FolderElement)this["initialFolder"]; }
            set { this["initialFolder"] = value; }
        }

        [ConfigurationProperty("processingFolder")]
        public FolderElement ProcessingFolder
        {
            get { return (FolderElement)this["processingFolder"]; }
            set { this["processingFolder"] = value; }
        }

        [ConfigurationProperty("succeededFolder")]
        public FolderElement SucceededFolder
        {
            get { return (FolderElement)this["succeededFolder"]; }
            set { this["succeededFolder"] = value; }
        }

        [ConfigurationProperty("failedFolder")]
        public FolderElement FailedFolder
        {
            get { return (FolderElement)this["failedFolder"]; }
            set { this["failedFolder"] = value; }
        }
    }

    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }
}
