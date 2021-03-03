using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using XamarinBookStoreApi.Permissions;

namespace XamarinBookStoreApi
{
  public class XamarinClientCustomClaimsAdder : ICustomTokenRequestValidator
  {

    private readonly IdentityUserManager _userManager;
    private readonly IPermissionGrantRepository _permissionGrantRepository;

    public XamarinClientCustomClaimsAdder(IdentityUserManager userManager, IPermissionGrantRepository permissionGrantRepository)
    {
      _userManager = userManager;
      _permissionGrantRepository = permissionGrantRepository;
    }

    public async Task ValidateAsync(CustomTokenRequestValidationContext context)
    {
      if (context.Result.ValidatedRequest.ClientId == "XamarinBookStoreApi_Xamarin")
      {
        var Claims = ((ClaimsIdentity)context.Result.ValidatedRequest.Subject.Identity).Claims;
        var userId = Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var userName = Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        var permissionsCurrentUser = (await _permissionGrantRepository.GetListAsync()).Where(x => x.ProviderKey == userId || x.ProviderKey == userName);

        // Add a reference to the Application.Contracts project
        var hasDefaultBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Default)) != null;
        var hasCreateBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Create)) != null;
        var hasEditBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Edit)) != null;
        var hasDeleteBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Delete)) != null;

        var claims = new Claim[] {
          new Claim(XamarinBookStoreApiPermissions.Books.Default, hasDefaultBookPermission.ToString()) ,
          new Claim(XamarinBookStoreApiPermissions.Books.Create, hasCreateBookPermission.ToString()) ,
          new Claim(XamarinBookStoreApiPermissions.Books.Edit, hasEditBookPermission.ToString()) ,
          new Claim(XamarinBookStoreApiPermissions.Books.Delete, hasDeleteBookPermission.ToString()) ,
        };

        context.Result.ValidatedRequest.Client.AlwaysSendClientClaims = true;

        foreach (var claim in claims)
        {
          context.Result.ValidatedRequest.ClientClaims.Add(claim);
        }
      }
      await Task.CompletedTask;
    }
  }
}