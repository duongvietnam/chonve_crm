//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/9/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.CRM.Models.CRM;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public partial class KhNhomHeThongValidator : BaseNopValidator<KhNhomHeThongModel>
    {
        public KhNhomHeThongValidator(ILocalizationService localizationService)
        {
            //RuleFor(x => x.Code).NotEmpty().WithMessage("... không được để trống");
        }
    }
}

