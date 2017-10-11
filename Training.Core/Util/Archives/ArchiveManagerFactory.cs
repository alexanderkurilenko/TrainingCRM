using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Training.Core.Util.Archives
{
    public class ArchiveManagerFactory : IArchiveManagerFactory
    {
        private readonly Dictionary<string, IArchiveManager> archiveManagersMapping =
            new Dictionary<string, IArchiveManager>();

        [Inject]
        public ArchiveManagerFactory(IEnumerable<IArchiveManager> archiveManagers)
        {
            foreach (var archiveManager in archiveManagers)
            foreach (var extension in archiveManager.AcceptedExtensions)
            {
                archiveManagersMapping.Add(extension, archiveManager);
            }
        }

        public IArchiveManager GetArchiveManagerForFile(string file)
        {
            var extension = Path.GetExtension(file);

            if (!archiveManagersMapping.ContainsKey(extension))
            {
                // TODO (STS): replace with apropriate exception
                throw new Exception();
            }

            return archiveManagersMapping[extension];
        }
    }
}
