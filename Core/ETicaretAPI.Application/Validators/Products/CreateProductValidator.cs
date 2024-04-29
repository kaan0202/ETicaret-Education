using ETicaretAPI.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .NotNull()
             .WithMessage("Lütfen Ürün Adını Boş Geçmeyiniz")
             .MaximumLength(150)
             .MinimumLength(5)
              .WithMessage("Ürün adı 5 ile 150 karakter arasında olmalıdır.");


            RuleFor(s => s.Stock).NotEmpty()
                .NotNull()
                .WithMessage("Stok bilgisini giriniz")
                .Must(s => s >= 0)
                .WithMessage("Stok 0 'dan küçük olamaz.");

            RuleFor(p => p.Price).NotEmpty()
               .NotNull()
               .WithMessage("Fiyat bilgisini giriniz")
               .Must(s => s >= 0)
               .WithMessage("Fiyat 0 'dan küçük olamaz.");
        }
    }
}
