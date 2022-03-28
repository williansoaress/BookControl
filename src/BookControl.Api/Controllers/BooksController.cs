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
                               IBookRepository bookRepository,
                               INotificator notificator) : base(notificator)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _bookService = bookService;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            var books = _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetAll());
            return CustomResponse(books);
        }

        [HttpGet]
        [Route("Available")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksAvailable()
        {
            var books = await AvailableBooks();

            if(books == null)
            {
                NotifyError("No books available to rent.");
                return CustomResponse();
            }

            return CustomResponse(books);
        }

        [HttpGet]
        [Route("Unavailable")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksUnavailable()
        {
            var books = await UnavailableBooks();

            if (books == null)
            {
                NotifyError("All books are free to be rent.");
                return CustomResponse();
            }

            return CustomResponse(books);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<BookDTO>> GetById(Guid id)
        {
            var bookDTO = await GetBookById(id);

            if (bookDTO == null) return NotFound();

            return CustomResponse(bookDTO);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Post(BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _bookService.Add(_mapper.Map<Book>(bookDTO));

            return CustomResponse(bookDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<BookDTO>> Put(Guid id, BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != bookDTO.Id) return BadRequest();

            var book = _mapper.Map<Book>(bookDTO);
            await _bookService.Update(book);

            if (!ValidOperation()) return CustomResponse(bookDTO);

            return CustomResponse(bookDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<BookDTO>> Delete(Guid id)
        {
            var bookDTO = await GetBookById(id);

            if (bookDTO == null) return NotFound();

            await _bookRepository.Remove(id);
            return CustomResponse(bookDTO);
        }

        private async Task<BookDTO> GetBookById(Guid id)
        {
            return _mapper.Map<BookDTO>(await _bookRepository.GetById(id));
        }

        private async Task<IEnumerable<BookDTO>> AvailableBooks()
        {
            return _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetAvailableBooks());
        }

        private async Task<IEnumerable<BookDTO>> UnavailableBooks()
        {
            return _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetUnavailableBooks());
        }
    }
}
