using CafeEraApi.Dtos;
using CafeEraApi.Helpers;
using CafeEraApi.Models;
using CafeEraApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CafeEraApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        DataOutput dataOutput = new DataOutput();

        public RoleController(RoleService roleService) 
        {
            this._roleService = roleService;
        }

        [HttpPost("add/role")]
        public async Task<DataOutput> AddNewRole([FromBody] DtoRole role)
        {
            try
            {
                if (role == null || role.ToString().Length == 0)
                {
                    dataOutput.codeStatus = 202;
                    dataOutput.message = "Invalid params";
                }
                else
                {
                    var result = await _roleService.AddNewroleRequest(role);

                    dataOutput.codeStatus = result.codeStatus;
                    dataOutput.message = result.message;
                }
            }
            catch (Exception error)
            {
                dataOutput.codeStatus = 500;
                dataOutput.message = error.Message;
            }

            return dataOutput;
        }

        [HttpGet("get/roles")]
        public async Task<DataOutput> GetRoles()
        {
            try
            {
                var result = await _roleService.GetAllRolesRequest();

                dataOutput.codeStatus = result.codeStatus;
                dataOutput.message = result.message;
                dataOutput.data = result.data;
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
        [HttpPut("update/role/{id_role}")]
        public async Task<DataOutput> UpdateRole(int id_role, [FromBody] DtoRole role)
        {
            try
            {
                var result = await _roleService.UpdateRoleRequest(id_role, role);

                dataOutput.codeStatus = result.codeStatus;
                dataOutput.message = result.message;
                dataOutput.data = result.data;
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
        [HttpDelete("delete/role/{id_role}")]
        public async Task<DataOutput> DeleteRole(int id_role)
        {
            try
            {
                var result = await _roleService.DeleteRoleRequest(id_role);

                dataOutput.codeStatus = result.codeStatus;
                dataOutput.message = result.message;
                dataOutput.data = result.data;
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
