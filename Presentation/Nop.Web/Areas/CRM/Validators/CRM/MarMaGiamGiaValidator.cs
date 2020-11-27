//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public partial class MarMaGiamGiaValidator : BaseNopValidator<MarMaGiamGiaModel>
    {
        public MarMaGiamGiaValidator(ILocalizationService localizationService)
        {
            RuleFor(c => c.SaleTheoSoTien).Must((model, SaleTheoSoTien) => {
                if (SaleTheoSoTien > 0)
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
            }).WithMessage("Chưa nhập mức giảm !");

            RuleFor(c => c.SoPhieuTao).Must((model, SoPhieuTao) => {
                if (SoPhieuTao > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Chọn số lượng phiếu tạo !");
        }
    }
}

