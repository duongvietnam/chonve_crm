//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.CRM;
using Nop.Services.CRM;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class DvDichVuModelFactory : IDvDichVuModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDvDichVuService _itemService;
        private readonly IDvNhomDichVuModelFactory _dvNhomDichVuModelFactory;
        private readonly IDvLoaiDichVuModelFactory _dvLoaiDichVuModelFactory;
        private readonly IDvDichVuComboService _dvDichVuComboService;
        private readonly IDvGiaDichVuService _dvGiaDichVuService;
        private readonly IDvDonViTinhModelFactory _dvDonViTinhModelFactory;
        #endregion

        #region Ctor

        public DvDichVuModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IDvDichVuService itemService,
            IDvNhomDichVuModelFactory dvNhomDichVuModelFactory,
            IDvLoaiDichVuModelFactory dvLoaiDichVuModelFactory,
            IDvDichVuComboService dvDichVuComboService,
            IDvGiaDichVuService dvGiaDichVuService,
            IDvDonViTinhModelFactory dvDonViTinhModelFactory
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._dvNhomDichVuModelFactory = dvNhomDichVuModelFactory;
            this._dvLoaiDichVuModelFactory = dvLoaiDichVuModelFactory;
            this._dvDichVuComboService = dvDichVuComboService;
            this._dvGiaDichVuService = dvGiaDichVuService;
            this._dvDonViTinhModelFactory = dvDonViTinhModelFactory;
        }
        #endregion

        #region DvDichVu
        public DvDichVuSearchModel PrepareDvDichVuSearchModel(DvDichVuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DvDichVuListModel PrepareDvDichVuListModel(DvDichVuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDvDichVus(isCombo: searchModel.IsCombo, StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DvDichVuListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<DvDichVuModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<DvDichVuModel>();
                    itemModel.EnumTrangThaiDichVuValue = CommonHelper.GetEnumDescription((EnumTrangThaiDichVu)item.TRANG_THAI);
                    if (searchModel.IsCombo)
                    {
                        itemModel.ListDichVuInCombo = string.Join("\n", _itemService.GetDvDichVuByIds(_dvDichVuComboService.GetDichVuTrongCombo(item.Id).ToArray()).Select(c => c.TEN));
                    }
                    ls.Add(itemModel);
                }
                return ls;
            });

            return model;
        }
        public DvDichVuModel PrepareDvDichVuModel(DvDichVuModel model, DvDichVu item, bool isComBo = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DvDichVuModel>();
                if (isComBo)
                {
                    var listCombo = _dvDichVuComboService.GetDichVuCombos(comBoId: item.Id);
                    foreach (var combo in listCombo)
                    {
                        model.comBoDichVuModels.Add(new ComBoDichVuModel
                        {
                            DichVuComboId = combo.DICH_VU_COMBO,
                            DichVuId = combo.DICH_VU_ID,
                            SoLuong = combo.SO_LUONG,
                            DDLDichVuCombo = PrepareDDLDichVuCombo(isCombo: false)
                        });
                    }
                }
                var giaDichVu = _dvGiaDichVuService.GetDvGiaDichVu(item.Id);
                model.GiaBanBuon = giaDichVu.GIA_BAN_BUON;
                model.GiaBanLe = giaDichVu.GIA_BAN_LE;
            }
            //more
            model.DDLLoaiDichVu = _dvLoaiDichVuModelFactory.PrepareSelectListLoaiDichVu(isAddFirst: true, valueFirstRow: "0");
            model.DDLNhomDichVu = _dvNhomDichVuModelFactory.PrepareSelectListNhomDichVu(isAddFirst: true, valueFirstRow: "0");
            model.DDLDonViTinh = _dvDonViTinhModelFactory.PrepareSelectListDonViTinh(isAddFirst: true);

            return model;
        }
        public void PrepareDvDichVu(DvDichVuModel model, DvDichVu item)
        {
            item.TEN = model.TEN;
            item.DIEM_DICH_VU = model.DIEM_DICH_VU;
            item.NHOM_ID = model.NHOM_ID;
            item.LOAI_ID = model.LOAI_ID;
            item.DON_VI_TINH = model.DON_VI_TINH;
        }
        public IList<SelectListItem> PrepareMultiSelectDichVuCombo(IList<int> valSelected = null, bool isAddFirst = false, string strFirstRow = "-- Chọn dịch vụ --", string valueFirstRow = "", bool isCombo = false)
        {
            var _listItems = _itemService.GetAllDvDichVus(isCombo: isCombo);
            var selectList = _listItems.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.Id.ToString(),
                Selected = valSelected != null && valSelected.Contains(c.Id)
            }).ToList();
            if (isAddFirst)
                selectList.Add(new SelectListItem
                {
                    Text = strFirstRow,
                    Value = valueFirstRow,
                });

            return selectList.OrderBy(c => c.Value).ToList();
        }
        public IList<SelectListItem> PrepareDDLDichVuCombo(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn dịch vụ --", string valueFirstRow = "", bool isCombo = false)
        {
            var _listItems = _itemService.GetAllDvDichVus(isCombo: isCombo);
            var selectList = _listItems.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.Id.ToString(),
                Selected = valSelected == c.Id
            }).ToList();
            if (isAddFirst)
                selectList.Add(new SelectListItem
                {
                    Text = strFirstRow,
                    Value = valueFirstRow,
                });

            return selectList.OrderBy(c => c.Value).ToList();
        }
        public bool CheckTenExist(string tenDichVu, int isCombo)
        {
            if (_itemService.CountDichVu(tenDichVu, isCombo) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

