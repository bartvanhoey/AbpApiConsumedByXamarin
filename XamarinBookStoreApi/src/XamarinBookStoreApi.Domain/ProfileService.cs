// using System.Linq;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using IdentityServer4.Models;
// using IdentityServer4.Services;
// using Volo.Abp.Identity;
// using Volo.Abp.PermissionManagement;
// using XamarinBookStoreApi.Permissions;

// namespace XamarinBookStoreApi
// {
//   public class ProfileService : IProfileService
//   {
//     private readonly IdentityUserManager _userManager;
//     private readonly IPermissionGrantRepository _permissionGrantRepository;

//     public ProfileService(IdentityUserManager userManager, IPermissionGrantRepository permissionGrantRepository)
//     {
//       _userManager = userManager;
//       _permissionGrantRepository = permissionGrantRepository;
//     }

//     public async Task GetProfileDataAsync(ProfileDataRequestContext context)
//     {
//       var user = await _userManager.GetUserAsync(context.Subject);
//       var permissionsCurrentUser = (await _permissionGrantRepository.GetListAsync()).Where(x => x.ProviderKey == user.Id.ToString() || x.ProviderKey == user.Name);

//       // Add a reference to the Application.Contracts project
//       var hasDefaultBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Default)) != null;
//       var hasCreateBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Create)) != null;
//       var hasEditBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Edit)) != null;
//       var hasDeleteBookPermission = permissionsCurrentUser.FirstOrDefault(x => x.Name.Contains(XamarinBookStoreApiPermissions.Books.Delete)) != null;

//       var claims = new Claim[] {
//         new Claim(XamarinBookStoreApiPermissions.Books.Default, hasDefaultBookPermission.ToString()) ,
//         new Claim(XamarinBookStoreApiPermissions.Books.Create, hasCreateBookPermission.ToString()) ,
//         new Claim(XamarinBookStoreApiPermissions.Books.Edit, hasEditBookPermission.ToString()) ,
//         new Claim(XamarinBookStoreApiPermissions.Books.Delete, hasDeleteBookPermission.ToString()) ,
//     };

//       context.IssuedClaims.AddRange(claims);
//     }

//     public async Task IsActiveAsync(IsActiveContext context)
//     {
//       var user = await _userManager.GetUserAsync(context.Subject);
//       context.IsActive = (user != null);
//     }
//   }
// }