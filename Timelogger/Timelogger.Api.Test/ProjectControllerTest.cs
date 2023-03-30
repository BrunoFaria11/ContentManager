using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Api.Controllers;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;
using Timelogger.Api.Test.Tools;
using System.Net.Http.Json;
using Timelogger.Application.Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Timelogger.Application.Test
{
    public class ProjectControllerTest : BaseControllerTests
    {
        private readonly Mock<IProjectService> _mockService;

        public ProjectControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
            _mockService = factory.ProjectServiceMock;
            _mockService.Reset();
        }

        [Fact]
        public async Task ShouldCreateProjectWithSuccessAsync()
        {
            //Arrange
            var project = new Project()
            {
                Name = "e-conomic Interview 2",
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 2,
            };

            _mockService.Setup(x => x.AddProject(It.IsAny<Project>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);

            var request = new ProjectsCommand()
            {
                Name = "e-conomic Interview 2",
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 2,
            };

            //Act
            var response = await GetNewClient().PostAsync("/api/Projects", JsonContent.Create(request));
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Response<Project>>(json);


            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(project.Name, obj.Data.Name);
        }

        [Fact]
        public async Task ShouldCreateProjectWithDuplicatedNameAsync()
        {
            //Arrange
            var project = new Project()
            {
                Name = "e-conomic Interview 2",
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 2,
            };

            _mockService.Setup(x => x.AddProject(It.IsAny<Project>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);
            _mockService.Setup(x => x.GetProjectByName(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);

            var request = new ProjectsCommand()
            {
                Name = "e-conomic Interview 2",
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 2,
            };

            //Act
            var response = await GetNewClient().PostAsync("/api/Projects", JsonContent.Create(request));

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task ShouldCreateProjectWithValidationErrosAsync()
        {
            //Arrange
            var request = new ProjectsCommand()
            {
                Name = null,
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 0,
            };

            //Act
            var response = await GetNewClient().PostAsync("/api/Projects", JsonContent.Create(request));

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.NotNull(response);
        }


        [Fact]
        public async Task ShouldGetAllProjectWithSuccessAsync()
        {
            //Arrange
            var projects = new List<Project>()
            {
                new Project(){
                    Name = "e-conomic Interview 2",
                    DeadLine = DateTime.Now.AddDays(1),
                    TimePerWeek = 2,
                },
                new Project(){
                    Name = "e-conomic Interview 3",
                    DeadLine = DateTime.Now.AddDays(1),
                    TimePerWeek = 2,
                },
            };

            _mockService.Setup(x => x.GetAllProjects(It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(projects);

            //Act
            var response = await GetNewClient().GetAsync("/api/Projects/GetAllProjects");
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Response<List<Project>>>(json);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.True(obj.Data.Count > 0);
            Assert.NotNull(response);
        }


        [Fact]
        public async Task ShouldGetProjectWithSuccessAsync()
        {
            //Arrange
            var project = new Project()
            {
                Name = "e-conomic Interview 2",
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 2,
            };

            _mockService.Setup(x => x.GetProject(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);

            //Act
            var response = await GetNewClient().GetAsync($"/api/Projects?Id={project.Id}");
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Response<Project>>(json);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(project.Name, obj.Data.Name);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task ShouldGetProjectWithValidationErrorAsync()
        {
            //Arrange
            var project = new Project()
            {
                Name = "e-conomic Interview 2",
                DeadLine = DateTime.Now.AddDays(1),
                TimePerWeek = 2,
            };

            _mockService.Setup(x => x.GetProject(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);

            //Act
            var response = await GetNewClient().GetAsync("/api/Projects");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.NotNull(response);
        }
    }
}

