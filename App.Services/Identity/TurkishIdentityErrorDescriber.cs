using Microsoft.AspNetCore.Identity;

public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError { Code = nameof(DuplicateUserName), Description = "Bu kullanıcı adı zaten kullanımda." };
    }
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = nameof(DuplicateUserName), Description = "Bu e-posta  adresi zaten kullanımda." };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Şifre en az {length} karakter olmalıdır." };
    }

    

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Şifre en az bir rakam içermelidir." };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Şifre en az bir küçük harf içermelidir." };
    }

   
}
