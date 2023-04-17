using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using TechnicalTest.Core.AppExceptions;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Entities;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Responses;
using TechnicalTest.Infrastructure.Repositories;
using TechnicalTest.Infrastructure.Validations;

namespace TechinicalTest.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personServices;
        private readonly IMapper _mapper;

        public PersonController(IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult> GetPersons()
        {
            var person = await _personServices.GetAllPerson();
            return Ok(person);
        }

        [HttpGet("{personId}")]

        public async Task<ActionResult> GetPersonById(int personId)
        {

            var result = await _personServices.GetPersonById(personId);
            if (result == null)
            {
                throw new AppException($"Doesn't exist any person with Id: {personId}");
            }
            var person = new ApiResponse<Person>()
            {
                Data = result
            };
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult> PostPerson(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _personServices.InsertPerson(person);


            var validator = new PersonValidation();
            var validationResult = validator.Validate(personDto);
            if (!validationResult.IsValid)
            {
                throw new AppException(
                    "An error has occurred",
                    validationResult.Errors.Select(validation => new ErrorMessage
                    {
                        Message = validation.ErrorMessage,
                        Code = validation.ErrorCode
                    }));
            }

            var response = new ApiResponse<Person>()
            {
                Data = person,
                Message = "Added successfully"
            };
            return Ok(response);
        }

        [HttpDelete("{personId}")]

        public async Task<ActionResult> Delete(int personId)
        {
            _ = await _personServices.DeletePerson(personId);

            var response = new ApiResponse<Person>()
            {
                Message = "Delete succesfully"
            };
            return Ok(response);
        }

        [HttpPut("{personId}")]

        public async Task<ActionResult> PutPerson(int personId, PersonDto personDto)
        {
            var personUpdate = _mapper.Map<Person>(personDto);
            personUpdate.Id = personId;

            var validator = new PersonValidation();
            var validationResult = validator.Validate(personDto);
            if (!validationResult.IsValid)
            {
                throw new AppException(
                    "An error has occurred",
                    validationResult.Errors.Select(validation => new ErrorMessage
                    {
                        Message = validation.ErrorMessage,
                        Code = validation.ErrorCode
                    }));
            }

            _ = await _personServices.UpdatePerson(personUpdate);
            var response = new ApiResponse<Person>()
            {
                Data = personUpdate,
                Message = "Succesfully Modified"
            };
            return Ok(personUpdate);
        }

    }
}
