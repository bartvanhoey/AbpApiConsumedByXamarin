using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using XamarinBookStoreApi.Domain.UserPermissions;
using XamarinBookStoreApi.Permissions;

namespace XamarinBookStoreApi.UserPermissions
{
  public class XamarinClientCustomClaimsAdder : ICustomTokenRequestValidator
  {
    // Claims you want to pass in the JWT token
    private List<string> _claimsToAdd = new List<string> {
        XamarinBookStoreApiPermissions.Books.Default,
        XamarinBookStoreApiPermissions.Books.Create,
        XamarinBookStoreApiPermissions.Books.Edit,
        XamarinBookStoreApiPermissions.Books.Delete
        };

    private readonly IUserPermissionsDapperRepository _userPermissionRepository;

    public XamarinClientCustomClaimsAdder(IUserPermissionsDapperRepository userPermissionRepository) => _userPermissionRepository = userPermissionRepository;

    public async Task ValidateAsync(CustomTokenRequestValidationContext context)
    {
      // Add claims only when client is Xamarin
      if (context.Result.ValidatedRequest.ClientId == "XamarinBookStoreApi_Xamarin")
      {
        // Find permissions of the current user
        var Claims = ((ClaimsIdentity)context.Result.ValidatedRequest.Subject.Identity).Claims;
        var currentUserId = Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var permissions = await _userPermissionRepository.GetUserPermissions(Guid.Parse(currentUserId));

        context.Result.ValidatedRequest.Client.AlwaysSendClientClaims = true;
        
        foreach (var claimToAdd in _claimsToAdd)
        {
          context.Result.ValidatedRequest.ClientClaims.Add(new Claim(claimToAdd, (permissions.FirstOrDefault(x => x.Contains(claimToAdd)) != null).ToString()));
        }
      }
      await Task.CompletedTask;
    }
  }
}