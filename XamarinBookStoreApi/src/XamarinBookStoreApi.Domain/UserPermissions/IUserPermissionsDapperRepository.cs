using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace XamarinBookStoreApi.Domain.UserPermissions
{
  public interface IUserPermissionsDapperRepository : IRepository
  {
    Task<List<string>> GetUserPermissions(Guid userId);
  }
}