using AbpApi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AbpApi.Permissions
{
    public class AbpApiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(AbpApiPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(AbpApiPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpApiResource>(name);
        }
    }
}
