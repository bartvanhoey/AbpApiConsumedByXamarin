using AbpApi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AbpApi.Permissions
{
    public class AbpApiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {


        var bookStoreGroup = context.AddGroup(AbpApiPermissions.BookStoreGroup, L("Permission:BookStoreGroup"));

        var booksPermission = bookStoreGroup.AddPermission(AbpApiPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(AbpApiPermissions.Books.Create, L("Permission:Books:Create"));
        booksPermission.AddChild(AbpApiPermissions.Books.Update, L("Permission:Books:Update"));
        booksPermission.AddChild(AbpApiPermissions.Books.Delete, L("Permission:Books:Delete"));
        
       





        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpApiResource>(name);
        }
    }
}
