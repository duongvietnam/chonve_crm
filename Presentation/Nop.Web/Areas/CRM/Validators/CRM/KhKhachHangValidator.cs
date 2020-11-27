//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.CRM.Models.CRM;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public partial class KhKhachHangValidator : BaseNopValidator<KhKhachHangModel>
    {
        public KhKhachHangValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
        }
    }
}

