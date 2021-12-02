namespace AbpApi.Permissions
{
    public static class AbpApiPermissions
    {
        public const string BookStoreGroup = "BookStore";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";


        public static class Books
        {
            public const string Default = BookStoreGroup + ".Book";
            public const string Create = Default + ".Create";
            public const string Update = Default+ ".Update";
            public const string Delete = Default + ".Delete";
        }       

    }
}