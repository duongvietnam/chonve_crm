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
    public partial class KhNhomKhachHangValidator : BaseNopValidator<KhNhomKhachHangModel>
    {
        public KhNhomKhachHangValidator(ILocalizationService localizationService,
            IKhNhomKhachHangModelFactory khNhomKhachHangModelFactory)
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.TEN).Must((model,code) => {
                return !khNhomKhachHangModelFactory.CheckTenExist(code);
            }).WithMessage("Tên không được trùng");
            RuleFor(x => x.NhomHeThongIds).Must((model, NhomHeThongIds) => {
                if (NhomHeThongIds.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Vui lòng chọn nhóm khách hàng");
        }
    }
}

