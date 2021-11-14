using Volo.Abp.Settings;

namespace AbpApi.Settings
{
    public class AbpApiSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(AbpApiSettings.MySetting1));
        }
    }
}
