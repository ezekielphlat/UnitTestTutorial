using CloudCustomer.API.Config;
using CloudCustomer.API.Models;
using CloudCustomer.API.Services;
using CloudCustomer.UnitTest.Fixtures;
using CloudCustomer.UnitTest.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomer.UnitTest.Systems.Services
{
    public class TestUsersService
    {
        private string _endpoint = "https://example.com";

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = "https://example.com/users"
            });
            var sut = new UsersService(httpClient,config);   // system under test

            //act
            await sut.GetAllUsers();
            //assert
            // verify http request is made
            handlerMock
                .Protected()
                .Verify("SendAsync", 
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
                
        }

        [Fact]
        public async Task GetAllUsers_WhenHit4040_ReturnsEmptyListOfUsers()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = "https://example.com/users"
            });
            var sut = new UsersService(httpClient, config);   // system under test

            //act
            var result = await sut.GetAllUsers();
            //assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = "https://example.com/users"
            });
            var sut = new UsersService(httpClient, config);   // system under test

            //act
            var result = await sut.GetAllUsers();
            //assert
            result.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, _endpoint);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = _endpoint
            });
            var sut = new UsersService(httpClient, config);   // system under test

            //act
            var result = await sut.GetAllUsers();
            var uri = new Uri(_endpoint);
            //assert
            handlerMock
               .Protected()
               .Verify("SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
               ItExpr.IsAny<CancellationToken>());
        }
    }
}
