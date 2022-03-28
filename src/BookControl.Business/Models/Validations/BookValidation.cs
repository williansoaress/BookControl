using FluentValidation;

namespace BookControl.Business.Models.Validations
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(b => b.StudentId)
                .Null().WithMessage("The book is with another student");
        }
    }
}
