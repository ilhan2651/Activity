using App.Dto.CommentDto;
using App.Dto.ContactDto;
using App.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Validation
{
    public class CreateContactValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("İsim Boş Olamaz.")
           .MinimumLength(3).WithMessage("İsim En Az 3 Karakter Olmalıdır.")
           .MaximumLength(30).WithMessage("İsim En Fazla 50 Karakter Olmalıdır.");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email Boş Olamaz.")
            .EmailAddress().WithMessage("Geçerli Bir Email Adresi Giriniz.");

            RuleFor(c => c.Subject).NotEmpty().WithMessage("Konu Boş Olamaz.")
         .MinimumLength(3).WithMessage("Konu En Az 3 Karakter Olmalıdır.");

            RuleFor(c => c.Message).NotEmpty().WithMessage("Mesaj Boş Olamaz.")
            .MinimumLength(10).WithMessage("Mesaj En Az 10 Karakter Olmalıdır.")
          .MaximumLength(500).WithMessage("Mesaj En Fazla 500 Karakter Olmalıdır.")
          .Must(ContainWord).WithMessage("Mesaj içerisinde en az bir kelime olmalıdır.");
                

        }
        private bool ContainWord(string message)
        {
            return !string.IsNullOrWhiteSpace(message) && message.Trim().Split(' ').Any(w => w.Length > 0);

        }
    }
}
