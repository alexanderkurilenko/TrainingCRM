using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.ImportType.Models
{
    public enum Roles
    {
        User = 928130000,
        Admin
    }

    [Serializable]
    public class PortalTest:ImportEntity,ILockable
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Roles Role { get; set; }

        public string UniqueIdentifier => (UserId + "!!" + Login).ToUpper();

        public override string MainFieldsMessage
        {
            get { return string.Format("Login='{0}' and Name='{1}'",Login, Name); }
        }
    }

    [Serializable]
    public class PortalTests : IImportEntityCollection<PortalTest>
    {
        private PortalTest[] portalTestField;

        public PortalTest[] PortalTest
        {
            get => this.PortalTest;
            set => this.PortalTest = value;
        }
        public IEnumerable<PortalTest> Entities => PortalTest;
    }
}
