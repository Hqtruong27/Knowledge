using Moq;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Knowledge.Web.API.Controllers;
using Knowledge.Data.UOW;
using Knowledge.Services.ViewModels;
using AutoMapper;
using MockQueryable.Moq;
=======
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Xunit;
using Knowledge.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Knowledge.Web.API.UnitTest.Extensions;
using Knowledge.Web.API.Controllers;
using Knowledge.Data.UOW;
>>>>>>> main

namespace Knowledge.Web.API.UnitTest.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
<<<<<<< HEAD
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<IdentityRole> _roles = new List<IdentityRole> {
                new IdentityRole{Id = "admin",Name = "admin" },
                new IdentityRole{Id = "Manager",Name = "Manager" },
                new IdentityRole{Id = "CEO",Name = "CEO" },
                new IdentityRole{Id = "Employee",Name = "Employee" }
            };
        #region 1: Ctor
        public RolesControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
=======
        public RolesControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
>>>>>>> main
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
        }
        [Fact]
        public void CreateInstance_Notnull()
        {
<<<<<<< HEAD
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
            Assert.NotNull(rolesController);
        }
        #endregion Ctor

        #region 2: GetAll
        [Fact]
        public async Task GetAll_HasData_Success()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
            _mockUnitOfWork.Setup(x => x.RoleRepository.GetAll()).ReturnsAsync(_roles.AsQueryable().BuildMock().Object);
=======
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
            Assert.NotNull(rolesController);
        }
        [Fact]
        public async Task GetAll_HasData_Success()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(x => x.RoleRepository.GetAll()).ReturnsAsync(new List<IdentityRole> {
                new IdentityRole{Id = "admin",Name = "admin" },
                new IdentityRole{Id = "user",Name = "user" }
            }.AsEnumerable());
>>>>>>> main

            var result = await rolesController.GetAllAsync();
            var okResult = (OkObjectResult)result;
            Assert.True((okResult.Value as IEnumerable<RoleViewModel>).Any());
        }

        [Fact]
        public async Task GetAll_Hasdata_Failed()
        {
<<<<<<< HEAD
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
            _mockRoleManager.Setup(x => x.Roles).Throws(new Exception());
            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetAllAsync());
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
            var paging = okResult.Value as Pagination<RoleViewModel>;

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
            var paging = okResult.Value as Pagination<RoleViewModel>;

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
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
=======
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
            _mockRoleManager.Setup(x => x.Roles).Throws(new Exception());
            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetAllAsync());
        }

        [Fact]
        public async Task CreateAsync_ValidInput_Succeeded()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
>>>>>>> main
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);
            var result = await rolesController.CreateAsync(new RoleViewModel
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
<<<<<<< HEAD
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object, _mockMapper.Object);
=======
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
>>>>>>> main
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed(new IdentityError()));
            var result = await rolesController.CreateAsync(new RoleViewModel
            {
                Id = "Admin",
                Name = "Admin",
            });
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
