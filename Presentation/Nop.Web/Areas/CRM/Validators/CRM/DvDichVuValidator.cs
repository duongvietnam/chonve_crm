//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Areas.CRM.Factories.CRM;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public partial class DvDichVuValidator : BaseNopValidator<DvDichVuModel>
    {
        public DvDichVuValidator(ILocalizationService localizationService,
            IDvDichVuModelFactory dvDichVuModelFactory)
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.TEN).Must((model, code) => {
                return !dvDichVuModelFactory.CheckTenExist(code, (int)model.IS_COMBO);
            }).WithMessage("Tên không được trùng");
            RuleFor(c => c.DON_VI_TINH).Must((model, DON_VI_TINH) => {
                if (DON_VI_TINH > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Chọn đơn vị tính");
        }
    }
}
