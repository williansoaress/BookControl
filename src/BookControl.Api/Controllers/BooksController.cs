using AutoMapper;
using BookControl.Api.DTOs;
using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookControl.Api.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : MainController
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IBookRepository _bookRepository;

        public BooksController(IHttpClientFactory httpClient, 
                               IMapper mapper, 
                               IBookService bookService, 
                               IBookRepository bookRepository)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _bookService = bookService;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<BookDTO>> GetById(Guid id)
        {
            var bookDTO = await GetBookById(id);

            if (bookDTO == null) return NotFound();

            return Ok(bookDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            var books = _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetAll());
            return Ok(books);
        }

        [HttpGet]
        [Route("{isbn:int}")]
        public async Task<ActionResult<string>> GetByIsbn(int isbn)
        {
            var httpClient = _httpClient.CreateClient();
            var response = await httpClient.GetAsync("https://api.mercadoeditorial.org/api/v1.2/book?isbn=9788576082675");
            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Post(BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _bookService.Add(_mapper.Map<Book>(bookDTO));

            return Ok(bookDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<BookDTO>> Put(Guid id, BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != bookDTO.Id) return BadRequest();

            var book = _mapper.Map<Book>(bookDTO);
            await _bookService.Update(book);

            return Ok(bookDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<BookDTO>> Delete(Guid id)
        {
            var bookDTO = await GetBookById(id);

            if (bookDTO == null) return NotFound();

            await _bookRepository.Remove(id);
            return Ok(bookDTO);
        }

        private async Task<BookDTO> GetBookById(Guid id)
        {
            return _mapper.Map<BookDTO>(await _bookRepository.GetById(id));
        }
    }
}
