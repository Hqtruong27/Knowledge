using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using MockQueryable.Moq;
using Knowledge.Web.IdentityProvider.Controllers;
using Knowledge.Web.IdentityProvider.Models;
using Knowledge.Web.IdentityProvider.Bussiness;

namespace Knowledge.Web.API.UnitTest.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<IRoleManager> _mockRoleManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<RoleResponse> _roles = new List<RoleResponse> {
                new RoleResponse{Id = "admin",Name = "admin" },
                new RoleResponse{Id = "Manager",Name = "Manager" },
                new RoleResponse{Id = "CEO",Name = "CEO" },
                new RoleResponse{Id = "Employee",Name = "Employee" }
            };
        #region 1: Ctor
        public RolesControllerTest()
        {
            _mockMapper = new Mock<IMapper>();
            var roleStore = new Mock<IRoleStore<IdentityRole>>();// roleStore.Object, null, null, null, null
            _mockRoleManager = new Mock<IRoleManager>();
        }
        [Fact]
        public void CreateInstance_Notnull()
        {
            var rolesController = new RolesController(_mockRoleManager.Object);
            Assert.NotNull(rolesController);
        }
        #endregion Ctor

        #region 2: GetAll
        [Fact]
        public async Task GetAll_HasData_Success()
        {
            var rolesController = new RolesController(_mockRoleManager.Object);
            _mockRoleManager.Setup(x => x.GetsAsync()).ReturnsAsync(_roles.AsQueryable().BuildMock().Object);
            var result = await rolesController.Gets();
            var okResult = (OkObjectResult)result;
            Assert.True((okResult.Value as IEnumerable<RoleResponse>).Any());
        }

        [Fact]
        public async Task GetAll_Hasdata_Failed()
        {
            var rolesController = new RolesController(_mockRoleManager.Object);
            _mockRoleManager.Setup(x => x.GetsAsync()).Throws(new Exception());
            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.Gets());
        }
        #endregion GetAll

        #region 3: Get Pagination
        [Fact]
        public async Task GetPaging_NoFilter_HasData()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
            _mockUnitOfWork.Setup(x => x.RoleRepository.GetAll()).ReturnsAsync(_roles.AsQueryable().BuildMock().Object);

            var result = await rolesController.GetPagination(null, 1, 2);
            var okResult = (OkObjectResult)result;
            var paging = okResult.Value as Pagination<RoleRequest>;

            Assert.Equal(4, paging.TotalRecords);
            Assert.Single(paging.Items);
        }

        [Fact]
        public async Task GetPaging_HasFilter_HasData()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
            _mockUnitOfWork.Setup(x => x.RoleRepository.GetAll()).ReturnsAsync(_roles.AsQueryable().BuildMock().Object);

            var result = await rolesController.GetPagination("admin", 1, 2);
            var okResult = (OkObjectResult)result;
            var paging = okResult.Value as Pagination<RoleRequest>;

            Assert.Equal(1, paging.TotalRecords);
            Assert.Single(paging.Items);
        }

        //[Fact]
        //public async Task GetPaging_Nofilter_Failed()
        //{
        //    var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
        //    _mockRoleManager.Setup(x => x.Roles).Throws(new Exception());
        //    await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetAllAsync());
        //}
        #endregion Get Pagination
        [Fact]
        public async Task CreateAsync_ValidInput_Succeeded()
        {
            var rolesController = new RolesController(_mockRoleManager.Object);
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<RoleRequest>())).ReturnsAsync(IdentityResult.Success);
            var result = await rolesController.CreateAsync(new RoleRequest
            {
                Id = "Admin",
                Name = "Admin",
            });
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
        }
        [Fact]
        public async Task CreateAsync_ValidInput_Failed()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed(new IdentityError()));
            var result = await rolesController.CreateAsync(new RoleRequest
            {
                Id = "Admin",
                Name = "Admin",
            });
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
