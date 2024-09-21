using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ResourcePlanner.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResourcePlanner.Api.Test.Integration
{
    public class ResourceControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _client;

        public ResourceControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetAllResources_ReturnsOk()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/resources");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetResouceById_ReturnsOk_WhenResourceExists()
        {
            // Arrange
            CreateResourceRequestDTO request = new()
            {
                Name = "Resource 1"
            };
            var createResponse = await _client.PostAsJsonAsync("/api/resources", request);
            var createResponseContent = JsonSerializer.Deserialize<CreateResourceReponseDTO>(await createResponse.Content.ReadAsStringAsync(), _jsonSerializerOptions);

            // Act
            var response = await _client.GetAsync($"/api/resources/{createResponseContent!.Id}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteResource_ReturnsNoContent_WhenResourceExists()
        {
            // Arrange
            CreateResourceRequestDTO request = new()
            {
                Name = "Resource 1"
            };
            var createResponse = await _client.PostAsJsonAsync("/api/resources", request);
            var createResponseContent = JsonSerializer.Deserialize<CreateResourceReponseDTO>(await createResponse.Content.ReadAsStringAsync(), _jsonSerializerOptions);

            // Act
            var response = await _client.DeleteAsync($"/api/resources/{createResponseContent!.Id}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteResource_ReturnsNotFound_WhenResourceDoesNotExist()
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync("/api/resources/999"); // Non-existent ID

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetResourceById_ReturnsNotFound_WhenResourceDoesNotExist()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/resources/999"); // Non-existent ID

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
