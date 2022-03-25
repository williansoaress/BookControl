using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookControl.Business.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task Add(Student student)
        {
            await _studentRepository.Add(student);
        }

        public async Task Update(Student student)
        {
            await _studentRepository.Update(student);
        }


        public async Task Remove(Guid id)
        {
            await _studentRepository.Remove(id);
        }

        public void Dispose()
        {
            _studentRepository?.Dispose();
        }
    }
}
