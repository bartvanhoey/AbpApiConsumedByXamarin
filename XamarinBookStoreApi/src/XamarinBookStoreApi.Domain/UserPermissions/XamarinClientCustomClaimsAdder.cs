using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using XamarinBookStoreApi.Domain.UserPermissions;
using XamarinBookStoreApi.Permissions;

namespace XamarinBookStoreApi.UserPermissions
{
  public class XamarinClientCustomClaimsAdder : ICustomTokenRequestValidator
  {

    private readonly IdentityUserManager _userManager;
    private readonly IPermissionGrantRepository _permissionGrantRepository;
    private readonly IUserPermissionsDapperRepository _userPermissionRepository;

    public XamarinClientCustomClaimsAdder(IdentityUserManager userManager, IPermissionGrantRepository permissionGrantRepository, IUserPermissionsDapperRepository userPermissionRepository)
    {
      _userManager = userManager;
      _permissionGrantRepository = permissionGrantRepository;
      _userPermissionRepository = userPermissionRepository;
    }

    public async Task ValidateAsync(CustomTokenRequestValidationContext context)
    {

      // if (context.Result.ValidatedRequest.ClientId == "XamarinBookStoreApi_Xamarin")
      // {
        var Claims = ((ClaimsIdentity)context.Result.ValidatedRequest.Subject.Identity).Claims;
        var currentUserId = Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var permissions = await _userPermissionRepository.GetUserPermissions(Guid.Parse(currentUserId));
        //var userName = Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        // var permissionsCurrentUser = (await _permissionGrantRepository.GetListAsync()).Where(x => x.ProviderKey == userId || x.ProviderKey == userName);

        // Add a reference to the Application.Contracts project
        var hasDefaultBookPermission = permissions.FirstOrDefault(x => x.Contains(XamarinBookStoreApiPermissions.Books.Default)) != null;
        var hasCreateBookPermission = permissions.FirstOrDefault(x => x.Contains(XamarinBookStoreApiPermissions.Books.Create)) != null;
        var hasEditBookPermission = permissions.FirstOrDefault(x => x.Contains(XamarinBookStoreApiPermissions.Books.Edit)) != null;
        var hasDeleteBookPermission = permissions.FirstOrDefault(x => x.Contains(XamarinBookStoreApiPermissions.Books.Delete)) != null;

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
      // }
      await Task.CompletedTask;
    }
  }
}