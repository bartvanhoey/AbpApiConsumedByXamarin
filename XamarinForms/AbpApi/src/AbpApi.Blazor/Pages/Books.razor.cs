using System;
using AbpApi.Permissions;

namespace AbpApi.Blazor.Pages
{
    public partial class Books
    {
        public Books()
        {
            CreatePolicyName = AbpApiPermissions.Books.Create;
            UpdatePolicyName = AbpApiPermissions.Books.Update;
            DeletePolicyName = AbpApiPermissions.Books.Delete;
        }
    }
}
