﻿using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookControl.Business.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Add(Book book)
        {
            await _bookRepository.Add(book);
        }

        public async Task Update(Book book)
        {
            await _bookRepository.Update(book);
        }

        public async Task Remove(Guid id)
        {
            await _bookRepository.Remove(id);
        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}
