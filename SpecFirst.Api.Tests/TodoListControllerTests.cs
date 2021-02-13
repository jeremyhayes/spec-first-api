using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace SpecFirst.Api.Tests
{
    public class TodoListControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TodoListControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory
                .CreateClient();
        }

        [Fact]
        public async void ShouldGetAllTodoLists()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("todo-list");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

            var json = JArray.Parse(await response.Content.ReadAsStringAsync());
            Assert.Single(json);
            Assert.Equal(1, json.SelectToken("$[0].id"));
            Assert.Equal("Prove spec-first API design.", json.SelectToken("$[0].title"));
        }
    }
}
