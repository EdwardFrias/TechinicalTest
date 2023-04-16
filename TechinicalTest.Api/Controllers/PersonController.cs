﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Entities;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Responses;
using TechnicalTest.Infrastructure.Repositories;

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

        public async Task<IActionResult> GetPersons()
        {
            var person = await _personServices.GetAllPerson();
            return Ok(person);
        }

        [HttpGet("{personId}")]

        public async Task<IActionResult> GetPersonById(int personId)
        {
            var person = await _personServices.GetPersonById(personId);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> PostPerson(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _personServices.InsertPerson(person);

            var response = new ApiResponse<Person>()
            {
                Data = person,
                Message = "Registro agregado"
            };
            return Ok(response);
        }

        [HttpDelete("{personId}")]

        public async Task<IActionResult> Delete(int personId)
        {
            var person = await _personServices.DeletePerson(personId);

            var response = new ApiResponse<PersonDto>()
            {
                Message = "Registro eliminado"
            };
            return Ok(response);
        }

        [HttpPut("{personId}")]

        public async Task<IActionResult> PutPerson(int personId,PersonDto personDto)
        {
            var personUpdate = _mapper.Map<Person>(personDto);
            personUpdate.Id = personId;

            await _personServices.UpdatePerson(personUpdate);
            return Ok(personUpdate);
        }

    }
}