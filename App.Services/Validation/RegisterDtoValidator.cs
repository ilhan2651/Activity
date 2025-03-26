using App.Dto.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Validation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserFullName)
                .NotEmpty().WithMessage("İsim-Soyisim boş olamaz.")
                .MaximumLength(40);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Mail alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz!");

            RuleFor(x => x.UserProfilePictureUrl).NotEmpty().WithMessage("Profil fotoğtafınız boş olamaz.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı boş olamaz.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.")
                .Matches(@"[a-z]").WithMessage("Şifre en az 1 küçük harf içermeli.")
                .Matches(@"[0-9]").WithMessage("Şifre en az 1 rakam içermeli.");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre onayı boş olamaz.")
                .Equal(x => x.Password).WithMessage("Şifreler Eşleşmiyor!");

        }
    }
}
