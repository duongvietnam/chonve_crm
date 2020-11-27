using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.CRM.Models.CRM;
using LinqToDB.Tools;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public class DoanhNghiepValidator:BaseNopValidator<StoreModel>
    {
        public DoanhNghiepValidator(ILocalizationService localizationService)
        {

            //Kiểm duyệt dữ liệu trống 
            RuleFor(s  => s.Name).NotEmpty().WithMessage("Tên không được để trống!!");
            RuleFor(s => s.Url).NotEmpty().WithMessage("Url không được để trống!!");
            RuleFor(s => s.CompanyName).NotEmpty().WithMessage("Tên doanh nghiệp không được để trống!!");
            RuleFor(s => s.CompanyFullName).NotEmpty().WithMessage("Tên đầy đủ của doanh nghiệp không được để trống!!");
            RuleFor(s => s.CompanyAddress).NotEmpty().WithMessage("Địa chỉ doanh nghiệp không được để trống!!");
            RuleFor(s => s.CompanyPhoneNumber).NotEmpty().WithMessage("Số điện thoại doanh nghiệp không được để trống!!");
            RuleFor(s => s.DomainLogin).NotEmpty().WithMessage("Tên miền đăng nhập của doanh nghiệp không được để trống!!");

        }
    }
}
