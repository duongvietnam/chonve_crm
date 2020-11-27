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
    public partial class DvNhatKyGiaValidator : BaseNopValidator<DvNhatKyGiaModel>
    {
        public DvNhatKyGiaValidator(ILocalizationService localizationService)
        {
            //RuleFor(x => x.Code).NotEmpty().WithMessage("... không được để trống");
        }
    }
}

