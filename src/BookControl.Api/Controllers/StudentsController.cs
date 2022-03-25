using AutoMapper;
using BookControl.Api.DTOs;
using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookControl.Api.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : MainController
    {
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper, IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> Get()
        {
            var studentsDTO = await GetStudentsBooks();
            return Ok(studentsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<StudentDTO>> GetById(Guid id)
        {
            var studentDTO = await GetStudent(id);
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Post(StudentDTO studentDto)
        {
            await _studentService.Add(_mapper.Map<Student>(studentDto));
            return Ok(studentDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<StudentDTO>> Put(Guid id, StudentDTO studentDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != studentDto.Id) return BadRequest();

            await _studentService.Update(_mapper.Map<Student>(studentDto));
            return Ok(studentDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<StudentDTO>> Delete(Guid id)
        {
            var studentDTO = await GetStudent(id);

            if (studentDTO == null) return NotFound();

            await _studentService.Remove(id);
            return Ok(studentDTO);
        }

        private async Task<StudentDTO> GetStudent(Guid id)
        {
            var studentDTO = _mapper.Map<StudentDTO>(await _studentRepository.GetStudentBooks(id));
            return studentDTO;
        }

        private async Task<IEnumerable<StudentDTO>> GetStudentsBooks()
        {
            var studentsDTO = _mapper.Map<IEnumerable<StudentDTO>>(await _studentRepository.GetStudentsBooks());
            return studentsDTO;
        }
    }
}
