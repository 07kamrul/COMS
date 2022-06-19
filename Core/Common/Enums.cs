using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class Enums
    {
        public enum UserRoles
        {
            Maker = 1,
            Checker,
            Viewer,
            Admin
        }

        public enum Groups
        {
            AdminGroup = 1,
            UserGroup
        }

        public enum DataOperation
        {
            None,
            Save,
            Delete
        }
        public enum PermissionType
        {
            Maker = 1,
            Checker,
            Viewer,
            Admin
        }
    }
}
