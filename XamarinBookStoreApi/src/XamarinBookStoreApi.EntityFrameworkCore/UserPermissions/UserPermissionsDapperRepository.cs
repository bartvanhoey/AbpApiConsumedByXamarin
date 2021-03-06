using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using XamarinBookStoreApi.Domain.UserPermissions;

namespace XamarinBookStoreApi.EntityFrameworkCore.UserPermissions
{
  public class UserPermissionsDapperRepository : DapperRepository<XamarinBookStoreApiDbContext>, IUserPermissionsDapperRepository, ITransientDependency
  {
    public UserPermissionsDapperRepository(IDbContextProvider<XamarinBookStoreApiDbContext> dbContextProvider) : base(dbContextProvider) { }

    public async Task<List<string>> GetUserPermissions(Guid userId)
    {
      var connection = await GetDbConnectionAsync();
      var transaction = await GetDbTransactionAsync();

      var userRolesIds = (await connection.QueryAsync<Guid>(@"SELECT [RoleId] FROM [XamarinBookStoreApi].[dbo].[AbpUserRoles] WHERE [UserId] = @UserId", new { UserId = userId }, transaction)).ToList();

      var userPermissions = new List<string>();
      foreach (var roleId in userRolesIds)
      {
        var abpRoleName = (await connection.QueryAsync<string>(@"SELECT [Name] FROM [XamarinBookStoreApi].[dbo].[AbpRoles] WHERE [Id] = @Id", new { Id = roleId }, transaction)).FirstOrDefault();

        var abpPermissionGrants = (await connection.QueryAsync<string>(@"SELECT DISTINCT [Name] FROM [XamarinBookStoreApi].[dbo].[AbpPermissionGrants] 
                    WHERE ([ProviderName] = 'R' AND  [ProviderKey] = @RoleName) OR ([ProviderName] = 'U' AND  [ProviderKey] = @UserId) ",
                    new { RoleName = abpRoleName, UserId = userId.ToString() }, transaction)).ToList();
        userPermissions.AddRange(abpPermissionGrants);
      }
      return userPermissions;
    }

  }

}