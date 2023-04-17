using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using NUnit.Framework;
using TechinicalTest.Api.Controllers;
using TechnicalTest.Core.AppExceptions;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Entities;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Responses;
using TechnicalTest.Infrastructure.Mappings;
using TechnicalTest.Infrastructure.Repositories;
using Xunit;

namespace TechnicalTest.UnitTest.TechnicalTestApiController_Tests
{
    public class PersonTests
    {
        private Mapper _mapper;
        private Mock<IPersonServices> _personServicesMock;
        private PersonController? _controller;

        public PersonTests()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            _personServicesMock = new Mock<IPersonServices>();

            _mapper = new Mapper(configMapper);
        }

        [Fact]
        public async Task should_return_people_list_when_httprequest_get()
        {
            var personList = new List<Person>
            {
                new Person()
                {
                    Id = 1,
                    FullName = "juan",
                    DateOfBirth = new DateTime(2000, 01, 07)
                },
                new Person()
                {
                    Id = 2,
                    FullName = "juan2",
                    DateOfBirth = new DateTime(2002, 02, 01)
                },
            };

            _personServicesMock.Setup(mock => mock.GetAllPerson()).ReturnsAsync(personList);
            _controller = new PersonController(_personServicesMock.Object, _mapper);


            ActionResult<Person> result = await _controller.GetPersons();
            var objectResult = result.Result as OkObjectResult;

            _personServicesMock.Verify(x => x.GetAllPerson(), Times.Once());
            objectResult.Should().BeEquivalentTo(new OkObjectResult(personList));
        }

        [Fact]
        public async Task Should_return_a_person_when_httpGet_recived_Id()
        {
            var person = new Person();

            _personServicesMock.Setup(mock => mock.GetPersonById(It.IsAny<int>())).ReturnsAsync(person);
            _controller = new PersonController(_personServicesMock.Object, _mapper);

            ActionResult<Person> result = await _controller.GetPersonById(9);
            var objectResult = result.Result as OkObjectResult;

            _personServicesMock.Verify(x => x.GetPersonById(9), Times.Once());
            objectResult.Should().BeEquivalentTo(new OkObjectResult(new ApiResponse<Person>()
            {
                Data = person
            }));

        }

        [Fact]
        public async Task Should_delete_a_person_when_httpDelete_recived_Id()
        {
            _controller = new PersonController(_personServicesMock.Object, _mapper);

            ActionResult<Person> result = await _controller.Delete(3);
            var objectResult = result.Result as OkObjectResult;

            objectResult.Should().BeEquivalentTo(new OkObjectResult(new ApiResponse<Person>()
            {
                Message = "Delete succesfully"
            }));
        }

        [Xunit.Theory, AutoData]

        public async Task Should_save_a_person_when_httpPost_have_data(PersonDto personDto)
        {
            personDto.DateOfBirth = new DateTime(2000, 01, 07);
            _controller = new PersonController(_personServicesMock.Object, _mapper);

            ActionResult<Person> result = await _controller.PostPerson(personDto);
            var objectResult = result.Result as OkObjectResult;

            objectResult.Should().BeEquivalentTo(new OkObjectResult(new ApiResponse<PersonDto>()
            {
                Data = personDto,
                Message = "Added successfully"
            }));
        }

        [Xunit.Theory, AutoData]
        public async Task Should_throw_appException_when_httpPost_has_invalid_Data(PersonDto personDto)
        {
            personDto.DateOfBirth = new DateTime(2015, 01, 02);
            _controller = new PersonController(_personServicesMock.Object, _mapper);

            Func<Task> result = async () => await _controller.PostPerson(personDto);
            await result.Should().ThrowAsync<AppException>().WithMessage("An error has occurred");

        }
    }
}
