using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Common
{
    /// <summary>
    /// Những cấu hình chung của phần quản lý nhân sự
    /// </summary>
    public class CRMSettings : ISettings
    {
        /// <summary>
        /// Thiet dat tu dong cham cong hay ko
        /// </summary>
        public bool TuDongTinhCong { get; set; }
        /// <summary>
        /// So ngay lui so voi ngay hien tai
        /// </summary>
        public int SoNgayLuiTinhCong { get; set; }
        /// <summary>
        /// So phut xac dinh khoang thoi gian vao or ra ke tu moc bat dau vao va bat dau nghi, doi voi cac giao dich cham cong thu 1
        /// vdi: 120: neu la nua trc 8h->10h thi hieu la vao, ngc lai la ra        
        /// </summary>
        public int SoPhutToiDaXacDinhVaoRa1 { get; set; }
        /// <summary>
        /// So phut xac dinh khoang thoi gian vao or ra ke tu moc bat dau vao va bat dau nghi, doi voi cac giao dich cham cong thu 2, 3, 4..
        /// vd: =40: bat dau vao lam viec la 8h00, thi trong khoang 8h00->8h40 cac giao dich se xac dinh la vao, nguoc lai 17h30 nghi thi trong khoang 17h kem 10 -> 17h30 la giao dich ra
        /// </summary>
        public int SoPhutToiDaXacDinhVaoRa2 { get; set; }
        /// <summary>
        /// Thiet bi cham cong, se co nhieu truong hop nhan giao dich lien tiep nhau cua 1 nguoi, 
        /// </summary>
        public int SoPhutXacDinh2GiaoDichLienTiep { get; set; }
        /// <summary>
        /// Cheo do dc huong so phut dc nghi sau sinh khi di lam tro lai
        /// =60
        /// </summary>
        public int SoPhutNghiCheDoSauSinh { get; set; }
        /// <summary>
        /// So phut toi da di muon ve som : 30p
        /// </summary>
        public int SoPhutToiDaDiMuonVeSom { get; set; }
        /// <summary>
        /// so lan toi da ma mot nhan vien co the them moi trong 1 thang
        /// </summary>
        public int SoLanToiDaDiMuonVeSom { get; set; }
        public decimal SoPhut1Cong { get; set; }
        /// <summary>
        /// Cho phep nhan vien sang di muon sau khi di lam them vao buoi toi ngay hom qua
        /// 30p
        /// </summary>
        public int SoPhutToiDaDiMuonSauLamThem { get; set; }
        /// <summary>
        /// Thoi gian phai sau h nay moi dc ap dung SoPhutToiDaDiMuonSauLamThem
        /// sau 9h toi (21h)
        /// </summary>
        public int GioApDungDiMuonSauLamThem { get; set; }

    }
    public class CheckInOutMachineSettings : ISettings
    {
        //Thoi gian binh thuong scan may cham cong de lay du lieu cham cong tu may cham cong
        public int SoPhutLayChamCong { get; set; }
        /// <summary>
        /// Thoi gian scan luc cao diem o ca lam viec, tan suat scan nhieu hon
        /// </summary>
        public int SoPhutLayChamCongCaoDiem { get; set; }

        /// <summary>
        /// Khoang thoi gian cao diem, vd: =30p, => 8h lam viec thi thoi gian tu 7h30 -> 8h30 se cu [SoPhutLayChamChongCaoDiem] phut quet 1 lan
        /// </summary>
        public int KhoangThoiGianCaoDiem { get; set; }

    }


}
