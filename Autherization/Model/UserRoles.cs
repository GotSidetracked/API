using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Autherization.Model
{
    public class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);

        public static readonly IReadOnlyCollection<string> all = new[] { Admin, User };

    }
}
