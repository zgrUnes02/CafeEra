using CafeEraApi.Context;
using CafeEraApi.Dtos;
using CafeEraApi.Helpers;
using CafeEraApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CafeEraApi.Services
{
    public class RoleService
    {
        private readonly CafeEraContext _cafeEraContext;

        public RoleService(CafeEraContext cafeEraContext)
        {
            this._cafeEraContext = cafeEraContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<DataOutput> GetAllRolesRequest()
        {
            DataOutput dataOutput = new DataOutput();

            try
            {
                var roles = await _cafeEraContext.Roles
                .Where(role => role.deleted_at == null)
                .AsNoTracking()
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.created_at
                })
                .ToListAsync();

                if (roles is not null && roles.Count != 0)
                {
                    dataOutput.message = "Ok";
                    dataOutput.codeStatus = 200;
                    dataOutput.data = roles;
                }
                else
                {
                    dataOutput.message = "Ok";
                    dataOutput.codeStatus = 200;
                    dataOutput.data = [];
                }
            }
            catch (Exception error)
            {
                dataOutput.codeStatus = 500;
                dataOutput.message = error.Message;
            }

            return dataOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<DataOutput> AddNewroleRequest(DtoRole role)
        {
            DataOutput dataOutput = new DataOutput();

            try
            {
                var roleExists = await _cafeEraContext.Roles
                    .AnyAsync(r => r.Name == role.Name && r.deleted_at == null);

                if (!roleExists)
                {
                    Role newRole = new Role
                    {
                        Name = role.Name,
                        created_at = DateTime.UtcNow,
                        created_by = "1"
                    };

                    await _cafeEraContext.AddAsync(newRole);
                    await _cafeEraContext.SaveChangesAsync();

                    dataOutput.message = "Role created with success";
                    dataOutput.codeStatus = 200;
                    dataOutput.data = new List<object> { newRole };
                }
                else
                {
                    dataOutput.message = "Role already exists";
                    dataOutput.codeStatus = 201;
                }
            }
            catch ( Exception error )
            {
                dataOutput.codeStatus = 500;
                dataOutput.message = error.Message;
            }

            return dataOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_role"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<DataOutput> UpdateRoleRequest(int id_role, DtoRole role)
        {
            DataOutput dataOutput = new DataOutput();

            try
            {
                var roleToUpdate = await _cafeEraContext.Roles
                    .Where(role => role.Id == id_role && role.deleted_at == null)
                    .FirstOrDefaultAsync();

                if (roleToUpdate is not null)
                {
                    roleToUpdate.Name = role.Name;
                    roleToUpdate.updated_at = DateTime.UtcNow;
                    roleToUpdate.updated_by = "1";
                    await _cafeEraContext.SaveChangesAsync();

                    dataOutput.codeStatus = 200;
                    dataOutput.message = "Role updated with success";
                }
                else
                {
                    dataOutput.codeStatus = 404;
                    dataOutput.message = "Role not found";
                }
            }
            catch (Exception error)
            {
                dataOutput.codeStatus = 500;
                dataOutput.message = error.Message;
            }

            return dataOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_role"></param>
        /// <returns></returns>
        public async Task<DataOutput> DeleteRoleRequest(int id_role)
        {
            DataOutput dataOutput = new DataOutput();

            try
            {
                var roleToDelete = await _cafeEraContext.Roles
                    .Where(role => role.Id == id_role && role.deleted_at == null)
                    .FirstOrDefaultAsync();

                if ( roleToDelete is not null )
                {
                    roleToDelete.deleted_at = DateTime.UtcNow;
                    roleToDelete.deleted_by = "1";
                    await _cafeEraContext.SaveChangesAsync();

                    dataOutput.codeStatus = 200;
                    dataOutput.message = "Role deleted with success";
                }
                else
                {
                    dataOutput.codeStatus = 404;
                    dataOutput.message = "Role not found";
                }
            }
            catch (Exception error)
            {
                dataOutput.codeStatus = 500;
                dataOutput.message = error.Message;
            }

            return dataOutput;
        }
    }
}
