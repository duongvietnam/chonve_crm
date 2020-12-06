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
    public partial class MarMarketingValidator : BaseNopValidator<MarMarketingModel>
    {
        public MarMarketingValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");

            RuleFor(c => c.Sale).Must((model, Sale) => {
                if (Sale > 0)
                {
                    return true;
                }
                else if (model.SaleTheoSoTien > 0)
                {
                    return true;
                }
                else if (model.SaleTheoPhanTram > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Số tiền giảm không được để trống !");
        }
    }
}

