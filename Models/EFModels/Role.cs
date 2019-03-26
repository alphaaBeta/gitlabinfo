namespace GitlabInfo.Models.EFModels
{
    public enum Role
    {
        None = 0,
        Guest = 10,
        Reporter = 20,
        Developer = 30,
        Maintainer = 40,
        Owner = 50
    }

    public static class RoleHelpers
    {
        public static Role GetRoleByValue(int accessLevel)
        {
            switch (accessLevel)
            {
                case 10:
                    return Role.Guest;
                case 20:
                    return Role.Reporter;
                case 30:
                    return Role.Developer;
                case 40:
                    return Role.Maintainer;
                case 50:
                    return Role.Owner;
                default:
                    return Role.None;
            }
        }
    }
}