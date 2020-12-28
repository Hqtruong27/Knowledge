using Moq;
using System;
using System.Collections.Generic;
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

namespace Knowledge.Web.API.UnitTest.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        public RolesControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
        }
        [Fact]
        public void CreateInstance_Notnull()
        {
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

            var result = await rolesController.GetAllAsync();
            var okResult = (OkObjectResult)result;
            Assert.True((okResult.Value as IEnumerable<RoleViewModel>).Any());
        }

        [Fact]
        public async Task GetAll_Hasdata_Failed()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
            _mockRoleManager.Setup(x => x.Roles).Throws(new Exception());
            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetAllAsync());
        }

        [Fact]
        public async Task CreateAsync_ValidInput_Succeeded()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
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
            var rolesController = new RolesController(_mockRoleManager.Object, _mockUnitOfWork.Object);
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
