using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Services.BaoCao;
using Nop.Services.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Nop.Services.ExportImport.Help
{
    /// <summary>
    /// Class for working with PropertyByName object list
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    public class PropertyManager<T>
    {
        /// <summary>
        /// All properties
        /// </summary>
        private readonly Dictionary<string, PropertyByName<T>> _properties;

        /// <summary>
        /// Catalog settings
        /// </summary>
        private readonly CatalogSettings _catalogSettings;
        private readonly CRMSettings _hrmSettings;
        private readonly ILocalizationService _localizationService;
        public BaoCaoCauHinh baoCaoCauHinh { get; set; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="properties">All access properties</param>
        /// <param name="catalogSettings">Catalog settings</param>
        public PropertyManager(IEnumerable<PropertyByName<T>> properties, CatalogSettings catalogSettings)
        {
            _properties = new Dictionary<string, PropertyByName<T>>();
            _catalogSettings = catalogSettings;
            var poz = 1;
            foreach (var propertyByName in properties.Where(p => !p.Ignore))
            {
                propertyByName.PropertyOrderPosition = poz;
                poz++;
                _properties.Add(propertyByName.PropertyName, propertyByName);
            }
        }
        public PropertyManager(IEnumerable<PropertyByName<T>> properties, CatalogSettings catalogSettings, CRMSettings hrmSettings, ILocalizationService localizationService)
        {
            _properties = new Dictionary<string, PropertyByName<T>>();
            _catalogSettings = catalogSettings;
            _hrmSettings = hrmSettings;
            _localizationService = localizationService;
            var poz = 1;
            foreach (var propertyByName in properties.Where(p => !p.Ignore))
            {
                propertyByName.PropertyOrderPosition = poz;
                poz++;
                _properties.Add(propertyByName.PropertyName, propertyByName);
            }
        }

        /// <summary>
        /// Add new property
        /// </summary>
        /// <param name="property">Property to add</param>
        public void AddProperty(PropertyByName<T> property)
        {
            if (_properties.ContainsKey(property.PropertyName))
                return;
            
            _properties.Add(property.PropertyName, property);
        }

        /// <summary>
        /// Export objects to XLSX
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="itemsToExport">The objects to export</param>
        /// <returns></returns>
        public virtual byte[] ExportToXlsx(IEnumerable<T> itemsToExport)
        {
            using var stream = new MemoryStream();
            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handles to the worksheets
                var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name);
                var fWorksheet = xlPackage.Workbook.Worksheets.Add("DuLieu");
                fWorksheet.Hidden = eWorkSheetHidden.VeryHidden;
                //create Headers and format them 
                int startRow = 1;
                if (baoCaoCauHinh != null)
                {
                    //tao thong tin header bao cao
                    foreach(var item in baoCaoCauHinh.headerInfos)
                    {
                        int curRow = startRow+item.RowOffset;
                        if (item.RowHeight > 0)
                            worksheet.Row(curRow).Height = item.RowHeight;
                        WriteCellInfoToXlsx(worksheet, item);
                    }    
                }    

                
                if (baoCaoCauHinh != null)
                    startRow = baoCaoCauHinh.startDataRow;
                WriteCaption(worksheet, startRow);
                var row = startRow+1;
                foreach (var items in itemsToExport)
                {
                    CurrentObject = items;
                    worksheet.Row(row).Height = 22;                   
                    bool isBold = items.GetPropValue<Boolean>("IsRowSumary");
                    WriteToXlsx(worksheet, row++, fWorksheet: fWorksheet,isFontBold: isBold);
                }
                FormatAllBoder(worksheet, itemsToExport.Count() + 1, _properties.Count, startRow);
                if (baoCaoCauHinh != null)
                {
                    row = startRow + itemsToExport.Count() + 1;
                    //tao thong tin header bao cao
                    foreach (var item in baoCaoCauHinh.footerInfos)
                    {
                        int curRow = row + item.RowOffset;
                        item.CellRange = string.Format(item.CellRange, curRow);
                        if (item.RowHeight > 0)
                            worksheet.Row(curRow).Height = item.RowHeight;
                        WriteCellInfoToXlsx(worksheet, item);
                    }
                }
                xlPackage.Save();
            }

            CurrentObject = default;
            return stream.ToArray();
        }
        void WriteCellInfoToXlsx(ExcelWorksheet worksheet, CellInfo cellInfo)
        {
            var cell = worksheet.Cells[cellInfo.CellRange];
            cell.Merge = true;
            cell.Style.WrapText = cellInfo.isWrapText;
            cell.Style.Font.Bold = cellInfo.isFontBold;
            cell.Style.Font.Size = cellInfo.FontSize;
            cell.Style.Font.Italic = cellInfo.isFontItalic;
            cell.Style.HorizontalAlignment=cellInfo.horizontalAlignment;
            cell.Style.VerticalAlignment = cellInfo.verticalAlignment;
            cell.Value = cellInfo.Val;
            
        }
        /// <summary>
        /// Current object to access
        /// </summary>
        public T CurrentObject { get; set; }

        /// <summary>
        /// Return property index
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        public int GetIndex(string propertyName)
        {
            if (!_properties.ContainsKey(propertyName))
                return -1;

            return _properties[propertyName].PropertyOrderPosition;
        }

        /// <summary>
        /// Access object by property name
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property value</returns>
        public object this[string propertyName] => _properties.ContainsKey(propertyName) && CurrentObject != null
            ? _properties[propertyName].GetProperty(CurrentObject)
            : null;

        /// <summary>
        /// Remove object by property name
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void Remove(string propertyName)
        {
            _properties.Remove(propertyName);
        }

        /// <summary>
        /// Write object data to XLSX worksheet
        /// </summary>
        /// <param name="worksheet">Data worksheet</param>
        /// <param name="row">Row index</param>
        /// <param name="cellOffset">Cell offset</param>
        /// <param name="fWorksheet">Filters worksheet</param>
        public virtual void WriteToXlsx(ExcelWorksheet worksheet, int row, int cellOffset = 0, ExcelWorksheet fWorksheet = null,bool isFontBold=false)
        {
            if (CurrentObject == null)
                return;
            
            foreach (var prop in _properties.Values)
            {
                var cell = worksheet.Cells[row, prop.PropertyOrderPosition + cellOffset];
                if (prop.IsDropDownCell && _catalogSettings.ExportImportRelatedEntitiesByName)
                {
                    var dropDownElements = prop.GetDropDownElements();
                    if (!dropDownElements.Any())
                    {
                        cell.Value = string.Empty;
                        continue;
                    }

                    cell.Value = prop.GetItemText(prop.GetProperty(CurrentObject));

                    if (!UseDropdownLists)
                        continue;

                    var validator = cell.DataValidation.AddListDataValidation();
                    
                    validator.AllowBlank = prop.AllowBlank;

                    if (fWorksheet == null)
                        continue;

                    var fRow = 1;
                    foreach (var dropDownElement in dropDownElements)
                    {
                        var fCell = fWorksheet.Cells[fRow++, prop.PropertyOrderPosition];

                        if (fCell.Value != null && fCell.Value.ToString() == dropDownElement)
                            break;
                        
                        fCell.Value = dropDownElement;
                    }

                    validator.Formula.ExcelFormula = $"{fWorksheet.Name}!{fWorksheet.Cells[1, prop.PropertyOrderPosition].Address}:{fWorksheet.Cells[dropDownElements.Length, prop.PropertyOrderPosition].Address}";
                }
                else
                {
                    cell.Value = prop.GetProperty(CurrentObject);
                    if (!string.IsNullOrEmpty(prop.FormatValue))
                        cell.Style.Numberformat.Format = prop.FormatValue;// 
                    if (isFontBold)
                        cell.Style.Font.Bold = true;
                }
            }
        }
        
        /// <summary>
        /// Read object data from XLSX worksheet
        /// </summary>
        /// <param name="worksheet">worksheet</param>
        /// <param name="row">Row index</param>
        /// /// <param name="cellOffset">Cell offset</param>
        public virtual void ReadFromXlsx(ExcelWorksheet worksheet, int row, int cellOffset = 0)
        {
            if (worksheet?.Cells == null)
                return;

            foreach (var prop in _properties.Values)
            {
                prop.PropertyValue = worksheet.Cells[row, prop.PropertyOrderPosition + cellOffset].Value;
            }
        }

        /// <summary>
        /// Write caption (first row) to XLSX worksheet
        /// </summary>
        /// <param name="worksheet">worksheet</param>
        /// <param name="row">Row number</param>
        /// <param name="cellOffset">Cell offset</param>
        public virtual void WriteCaption(ExcelWorksheet worksheet, int row = 1, int cellOffset = 0)
        {
            string nameOfT = typeof(T).FullName;
            foreach (var caption in _properties.Values)
            {
                var cell = worksheet.Cells[row, caption.PropertyOrderPosition + cellOffset];
                ConfigCellExcel configCell = null;
                if(_localizationService!=null)
                {
                    configCell = _localizationService.GetResource(string.Format("{0}.{1}", nameOfT, caption)).toEntity<ConfigCellExcel>();
                    cell.Value = configCell.DisplayName;
                }
                else
                    cell.Value = caption;
                SetCaptionStyle(cell);
                worksheet.Column(caption.PropertyOrderPosition + cellOffset).BestFit = true;
                if(configCell!=null)
                {
                    worksheet.Column(caption.PropertyOrderPosition + cellOffset).Width = configCell.Width;
                    worksheet.Column(caption.PropertyOrderPosition + cellOffset).Style.HorizontalAlignment = configCell.horizontalAlignment;
                }                
                worksheet.Column(caption.PropertyOrderPosition + cellOffset).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.Hidden = false;
            }
            worksheet.Row(row).Height = 35;
        }
        public void FormatAllBoder(ExcelWorksheet worksheet, int _crow, int _ccol, int startRow=1, int startCol=1)
        {
            for (int col = startCol; col < _ccol+ startCol; col++)
            {
                for (int row = startRow; row < _crow+ startRow; row++)
                {
                    var border = worksheet.Cells[row, col].Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                }
            }
        }
        /// <summary>
        /// Set caption style to excel cell
        /// </summary>
        /// <param name="cell">Excel cell</param>
        public void SetCaptionStyle(ExcelRange cell)
        {
            cell.Style.WrapText = true;            
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            cell.Style.Font.Bold = true;
        }

        /// <summary>
        /// Count of properties
        /// </summary>
        public int Count => _properties.Count;

        /// <summary>
        /// Get property by name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public PropertyByName<T> GetProperty(string propertyName)
        {
            return _properties.ContainsKey(propertyName) ? _properties[propertyName] : null;
        }

        /// <summary>
        /// Get property array
        /// </summary>
        public PropertyByName<T>[] GetProperties => _properties.Values.ToArray();

        /// <summary>
        /// Set SelectList
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="list">SelectList</param>
        public void SetSelectList(string propertyName, SelectList list)
        {
            var tempProperty = GetProperty(propertyName);
            if (tempProperty != null)
                tempProperty.DropDownElements = list;
        }

        /// <summary>
        /// Is caption
        /// </summary>
        public bool IsCaption => _properties.Values.All(p => p.IsCaption);

        /// <summary>
        /// Gets a value indicating whether need create dropdown list for export
        /// </summary>
        public bool UseDropdownLists => _catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities && _catalogSettings.ExportImportRelatedEntitiesByName;
    }
}
