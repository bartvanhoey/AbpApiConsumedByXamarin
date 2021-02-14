using XamarinBookStoreApi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace XamarinBookStoreApi.Permissions
{
    public class XamarinBookStoreApiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(XamarinBookStoreApiPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(XamarinBookStoreApiPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<XamarinBookStoreApiResource>(name);
        }
    }
}
