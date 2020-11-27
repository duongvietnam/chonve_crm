//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.CRM.Models.CRM;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public partial class ChPhanHangKhachHangValidator : BaseNopValidator<ChPhanHangKhachHangModel>
    {
        public ChPhanHangKhachHangValidator(ILocalizationService localizationService)
        {
            //RuleFor(x => x.Code).NotEmpty().WithMessage("... không được để trống");
        }
    }
}

