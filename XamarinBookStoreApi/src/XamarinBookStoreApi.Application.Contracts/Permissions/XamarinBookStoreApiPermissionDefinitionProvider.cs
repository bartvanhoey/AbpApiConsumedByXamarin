using XamarinBookStoreApi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace XamarinBookStoreApi.Permissions
{
  public class XamarinBookStoreApiPermissionDefinitionProvider : PermissionDefinitionProvider
  {
    public override void Define(IPermissionDefinitionContext context)
    {
      var bookStoreGroup = context.AddGroup(XamarinBookStoreApiPermissions.GroupName, L("Permission:BookStore"));

      var booksPermission = bookStoreGroup.AddPermission(XamarinBookStoreApiPermissions.Books.Default, L("Permission:Books"));
      booksPermission.AddChild(XamarinBookStoreApiPermissions.Books.Create, L("Permission:Books.Create"));
      booksPermission.AddChild(XamarinBookStoreApiPermissions.Books.Edit, L("Permission:Books.Edit"));
      booksPermission.AddChild(XamarinBookStoreApiPermissions.Books.Delete, L("Permission:Books.Delete"));
    }

    private static LocalizableString L(string name)
    {
      return LocalizableString.Create<XamarinBookStoreApiResource>(name);
    }
  }
}
