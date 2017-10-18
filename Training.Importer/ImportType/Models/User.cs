using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.ImportType.Models
{
    [Serializable]
    public class User:ImportEntity,ILockable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Hobby { get; set; }

        public string UniqueIdentifier => (Id + "!!" + Name).ToUpper();
        public override string MainFieldsMessage
        {
            get { return string.Format("Hobby='{0}' and Name='{1}'", Hobby, Name); }
        }
    }

    [Serializable]
    public class Users : IImportEntityCollection<User>
    {
        private User [] userField;

        public User[] User
        {
            get => this.userField;
            set => this.userField = value;
        }
        public IEnumerable<User> Entities =>User;
    }

}
