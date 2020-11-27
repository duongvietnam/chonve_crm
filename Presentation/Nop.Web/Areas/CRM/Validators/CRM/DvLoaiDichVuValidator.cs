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
    public partial class DvLoaiDichVuValidator : BaseNopValidator<DvLoaiDichVuModel>
    {
        public DvLoaiDichVuValidator(ILocalizationService localizationService,
            IDvLoaiDichVuModelFactory dvLoaiDichVuModelFactory)
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.TEN).Must((model, ten) => {
                return !dvLoaiDichVuModelFactory.CheckTenExist(ten);
            }).WithMessage("Tên đã tồn tại");
        }
    }
}

