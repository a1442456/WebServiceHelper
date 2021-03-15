using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace WebTrashCheck.Users
{
    public partial class ViewEquipments : Page
    {
        private const string _ascending = " ASC";
        private const string _descending= " DESC";
        private string _fontCode39Path = @"D:\FTP\fonts\Code39.ttf";
        private string _pdfLabelFonts = @"D:\FTP\fonts\arial.ttf";
        private string _pathToFTP = @"D:\FTP\labels\";
        private string _listlabelsMask = @"D:\FTP\labels\List_Labels_";
        private string _linkToLabelSaving = @"ftp:it3m:211/Labels/";
        private string _linkToLabelsSavings = @"ftp:it3m:211/labels/List_Labels_";
        private int[] _pdfLabelUnPrintablePlaces = new int[] { 0, 0, 0, 2 }; //left, right, top, bottom;
        private float[] _pdfLabelPageSize = new float[] { 161.57f, 82.20f }; //width, height;{161.57f, 82.20f - 58mm X 30mm} {139.28f, 82.20f - 50mm X 30mm}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowEquipmentData();
                FillFilterList();
            }
            if (IsPostBack)
                Session["FilternName"] = ddlFilterName.SelectedValue;
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            ShowEquipMentData(ddlFilterName.SelectedValue, txtbxFilter.Text);
        }

        private DataSet GetDataSetByFilter(string storedProcFilter, string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
            DataSet ds = new DataSet("EquipData");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                SqlCommand sqlComm = new SqlCommand(storedProcFilter, conn);
                sqlComm.Parameters.AddWithValue("@Name", filter);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                return ds;
            }
        }

        private void FillFilterList()
        {
            ddlFilterName.Items.Add("Инвентарный_номер");
            ddlFilterName.Items.Add("Название_оборудования");
            ddlFilterName.Items.Add("Имя_владельца");
            ddlFilterName.Items.Add("Название_департамента");
        }

        private string GetProcNameFromSelectedFilter(string filtername)
        {
            switch (filtername)
            {
                case "Инвентарный_номер":
                    return "GetdataByInvNum";
                case "Название_оборудования":
                    return "GetdataByEquip";
                case "Имя_владельца":
                    return "GetdataByPersonName";
                case "Название_департамента":
                    return "GetdataByDepartName";
                default:
                    return "GetdataByEquip";
            }
        }

        private void ShowEquipmentData()
        {
            DataSet ds = new DataSet();

            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("select * from view_all_filled_eq_data", connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(ds);

            grdvw.DataSource = ds.Tables[0];
            Session["CurrentDataSource"] = ds.Tables[0];
            grdvw.DataBind();
        }

        private void ShowEquipMentData(string filtername, string filtervalue)
        {
            string procname = GetProcNameFromSelectedFilter(filtername);
            DataTable dt = GetDataSetByFilter(procname, filtervalue).Tables[0];
            Session["CurrentDataSource"] = dt;
            grdvw.DataSource = dt;
            grdvw.DataBind();
        }

        //Sortings Property
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        //Sortings Methods
        private void SortGridView(string sortExpression, string direction)
        {
            //  You can cache the DataTable for improving performance
            DataTable dt = (DataTable)Session["CurrentDataSource"];

            DataView dv = new DataView(dt);
            dv.Sort = string.Concat(sortExpression, direction);

            grdvw.DataSource = dv;
            grdvw.DataBind();
        }

        //Sortings Event
        protected void grdvw_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, _descending);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, _ascending);
            }   
        }

        protected void grdvw_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() != "Инвентарный_номер" && e.CommandArgument.ToString() != "Название_оборудования" && e.CommandArgument.ToString() != "Имя_владельца" && e.CommandArgument.ToString() != "Название_департамента")
            {
                GridViewRow row = grdvw.Rows[int.Parse(e.CommandArgument.ToString())];

                //row.Cells[1].Text - value.
                //[1]- index of Инвентарный_номер in grdvw
                GenerateQRlabel(string.Format("{0}", row.Cells[1].Text), ReplaceWrongSympolsOnPrint(row.Cells[2].Text)); //QR Code
                //GenerateLabel((string.Format("{0}", row.Cells[1].Text), row.Cells[2].Text); //BarCode39

                //Open url in a new tab
                string link = this._linkToLabelSaving + TranslateString(ExceptWrongSymbols(row.Cells[1].Text.ToUpper()), "RtE") + ".pdf";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + link + "');", true);
            }
        }

        private void GenerateLabel(string phrase, string equipName)
        {
            BaseFont baseFont = BaseFont.CreateFont(_fontCode39Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);
            var pdfPageSize = new iTextSharp.text.Rectangle(this._pdfLabelPageSize[0], this._pdfLabelPageSize[1]);
            Document doc = new Document(pdfPageSize, this._pdfLabelUnPrintablePlaces[0], this._pdfLabelUnPrintablePlaces[1], this._pdfLabelUnPrintablePlaces[2], this._pdfLabelUnPrintablePlaces[3]);
            
            //Creating pdf.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(this._pathToFTP + ExceptWrongSymbols(phrase) + ".pdf", FileMode.Create));
            
            doc.Open();
            //add barcode
            Paragraph preface = new Paragraph(new Chunk(phrase, font));
            preface.Alignment = Element.ALIGN_CENTER;
            doc.Add(preface);

            //add barcodeText
            baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
            preface = new Paragraph(new Chunk(ExceptWrongSymbols(phrase), font));
            preface.Alignment = Element.ALIGN_CENTER;
            doc.Add(preface);

            //Add name equipment
            baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            font = new iTextSharp.text.Font(baseFont, 6, iTextSharp.text.Font.NORMAL);
            preface = new Paragraph(new Chunk(equipName, font));
            preface.Alignment = Element.ALIGN_CENTER;
            doc.Add(preface);

            doc.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">item 1/2 - phrase/name</param>
        private void GenerateLabel(List<Tuple<string, string>> list, ref Guid guidLink)
        {
            BaseFont baseFont = BaseFont.CreateFont(_fontCode39Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);

            var pdfPageSize = new iTextSharp.text.Rectangle(this._pdfLabelPageSize[0], this._pdfLabelPageSize[1]);
            Document doc = new Document(pdfPageSize, this._pdfLabelUnPrintablePlaces[0], this._pdfLabelUnPrintablePlaces[1], this._pdfLabelUnPrintablePlaces[2], this._pdfLabelUnPrintablePlaces[3]);
            //Creating pdf.
            string fileName = this._listlabelsMask + guidLink.ToString() + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
            doc.Open();
            for (int i = 0; i < list.Count; i++)
            {
                //add barcode
                baseFont = BaseFont.CreateFont(this._fontCode39Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);
                Paragraph preface = new Paragraph(new Chunk('*' + list[i].Item1 + '*', font));
                preface.Alignment = Element.ALIGN_CENTER;
                doc.Add(preface);

                //add barcodeText
                baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
                preface = new Paragraph(new Chunk(list[i].Item1, font));
                preface.Alignment = Element.ALIGN_CENTER;
                doc.Add(preface);

                //Add name equipment
                baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                font = new iTextSharp.text.Font(baseFont, 6, iTextSharp.text.Font.NORMAL);
                preface = new Paragraph(new Chunk(list[i].Item2, font));
                preface.Alignment = Element.ALIGN_CENTER;
                doc.Add(preface);
                doc.NewPage();
            }
            doc.Close();
        }

        private void GenerateQRlabel(string phrase, string equipName)
        {
            BaseFont baseFont = BaseFont.CreateFont(_fontCode39Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);

            var pdfPageSize = new iTextSharp.text.Rectangle(this._pdfLabelPageSize[0], this._pdfLabelPageSize[1]);
            Document doc = new Document(pdfPageSize, this._pdfLabelUnPrintablePlaces[0], this._pdfLabelUnPrintablePlaces[1], this._pdfLabelUnPrintablePlaces[2], this._pdfLabelUnPrintablePlaces[3]);

            //Creating pdf.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(this._pathToFTP + TranslateString((ExceptWrongSymbols(phrase.ToUpper())), "RtE") + ".pdf", FileMode.Create));
            doc.Open();


            //add barcode
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(phrase, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Image qrCodeImage = qrCode.GetGraphic(1);

            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Png);

            if (pic.Height > pic.Width)
            {
                //Maximum height is 800 pixels.
                float percentage = 0.0f;
                percentage = 40 / pic.Height;
                pic.ScalePercent(percentage * 100);
            }
            else
            {
                //Maximum width is 600 pixels.
                float percentage = 0.0f;
                percentage = 40 / pic.Width;
                pic.ScalePercent(percentage * 100);
            }
            pic.Alignment = Element.ALIGN_CENTER;
            doc.Add(pic);



            //add barcodeText
            baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
            Paragraph preface = new Paragraph(new Chunk(ExceptWrongSymbols(phrase), font));
            preface.Alignment = Element.ALIGN_CENTER;
            preface.SpacingBefore = -8.0f;
            doc.Add(preface);

            //Add name equipment
            baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            font = new iTextSharp.text.Font(baseFont, 6, iTextSharp.text.Font.NORMAL);
            preface = new Paragraph(new Chunk(equipName, font));
            preface.Alignment = Element.ALIGN_CENTER;
            doc.Add(preface);

            doc.Close();
        }

        private void GenerateQRlabel(List<Tuple<string, string>> list, ref Guid guidLink)
        {
            guidLink = Guid.NewGuid();
            BaseFont baseFont = BaseFont.CreateFont(_fontCode39Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);

            var pdfPageSize = new iTextSharp.text.Rectangle(this._pdfLabelPageSize[0], this._pdfLabelPageSize[1]);
            Document doc = new Document(pdfPageSize, this._pdfLabelUnPrintablePlaces[0], this._pdfLabelUnPrintablePlaces[1], this._pdfLabelUnPrintablePlaces[2], this._pdfLabelUnPrintablePlaces[3]);

            //Creating pdf.
            string fileName = this._listlabelsMask + guidLink.ToString() + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

            doc.Open();
            for (int i = 0; i < list.Count; i++)
            {
                //add barcode
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(list[i].Item1, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                 System.Drawing.Image qrCodeImage = qrCode.GetGraphic(1);

                 iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Png);

                if (pic.Height > pic.Width)
                {
                    //Maximum height is 800 pixels.
                    float percentage = 0.0f;
                    percentage = 40 / pic.Height;
                    pic.ScalePercent(percentage * 100);
                }
                else
                {
                    //Maximum width is 600 pixels.
                    float percentage = 0.0f;
                    percentage = 40 / pic.Width;
                    pic.ScalePercent(percentage * 100);
                }
                pic.Alignment = Element.ALIGN_CENTER;
                doc.Add(pic);

                //add barcodeText
                baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
                Paragraph preface = new Paragraph(new Chunk(ExceptWrongSymbols(list[i].Item1), font));
                preface.Alignment = Element.ALIGN_CENTER;
                preface.SpacingBefore = -8.0f;
                doc.Add(preface);

                //Add name equipment
                baseFont = BaseFont.CreateFont(this._pdfLabelFonts, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                font = new iTextSharp.text.Font(baseFont, 6, iTextSharp.text.Font.NORMAL);
                preface = new Paragraph(new Chunk(ReplaceWrongSympolsOnPrint(list[i].Item2), font));
                preface.Alignment = Element.ALIGN_CENTER;
                doc.Add(preface);
                doc.NewPage();
            }

            doc.Close();
        }

        private string ExceptWrongSymbols(string str)
        {
            List<char> wrongPathSymbols = new List<char> { '/', '\\', ':', '*', '<', '>', '|' };

            string clearStr = string.Empty;
            foreach (char symbol in str)
            {
                if (!wrongPathSymbols.Contains(symbol))
                    clearStr += symbol;
            }

            return clearStr;
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            
            var list = new List<Tuple<string, string>>();
            foreach (TableRow row in grdvw.Rows)
            {
                list.Add(new Tuple<string, string>(row.Cells[1].Text, row.Cells[2].Text));
            }

            //GenerateLabel(list); //BarCode39
            Guid guidLink = new Guid();
            GenerateQRlabel(list, ref guidLink); //QR code
            string link = this._linkToLabelsSavings + guidLink.ToString() +".pdf";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + link + "');", true);
            
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="direction">RtE - Russian To English; EtR - English To Russian</param>
        /// <returns></returns>
        private string Translator(char ch, string direction)
        {
            if (Char.IsLetter(ch))
            {
                if (direction == "RtE")
                {
                    #region RtE
                    switch (ch)
                    {
                        case 'А':
                            return "A";
                        case 'Б':
                            return "B";
                        case 'В':
                            return "V";
                        case 'Г':
                            return "G";
                        case 'Д':
                            return "D";
                        case 'Е':
                            return "E";
                        case 'Ё':
                            return "YO";
                        case 'Ж':
                            return "GH";
                        case 'З':
                            return "Z";
                        case 'И':
                            return "I";
                        case 'Й':
                            return "Y";
                        case 'К':
                            return "K";
                        case 'Л':
                            return "L";
                        case 'М':
                            return "M";
                        case 'Н':
                            return "N";
                        case 'О':
                            return "O";
                        case 'П':
                            return "P";
                        case 'Р':
                            return "R";
                        case 'С':
                            return "S";
                        case 'Т':
                            return "T";
                        case 'У':
                            return "U";
                        case 'Ф':
                            return "A";
                        case 'Х':
                            return "H";
                        case 'Ц':
                            return "C";
                        case 'Ч':
                            return "CH";
                        case 'Ш':
                            return "SH";
                        case 'Щ':
                            return "SHH";
                        case 'Ъ':
                            return "TVERDY_ZNAK";
                        case 'Ы':
                            return "YY";
                        case 'Ь':
                            return "MYAGKIY_ZNAK";
                        case 'Э':
                            return "YE";
                        case 'Ю':
                            return "UY";
                        case 'Я':
                            return "YA";
                        default:
                            throw new Exception("No such symbol");
                    }
                    #endregion
                }

                else if (direction == "EtR")
                {
                    #region EtR
                    switch (ch)
                    {
                        case 'A':
                            return "А";
                        case 'B':
                            return "Б";
                        case 'C':
                            return "Ц";
                        case 'D':
                            return "Д";
                        case 'F':
                            return "Ф";
                        case 'G':
                            return "Г";
                        case 'H':
                            return "АШ";
                        case 'I':
                            return "АЙ";
                        case 'J':
                            return "ДЖЕЙ";
                        case 'K':
                            return "К";
                        case 'L':
                            return "Л";
                        case 'M':
                            return "М";
                        case 'N':
                            return "Н";
                        case 'O':
                            return "О";
                        case 'P':
                            return "П";
                        case 'Q':
                            return "КЬЮ";
                        case 'R':
                            return "Р";
                        case 'S':
                            return "С";
                        case 'T':
                            return "Т";
                        case 'U':
                            return "Ю";
                        case 'V':
                            return "В";
                        case 'W':
                            return "ДАБЛЪЮ";
                        case 'X':
                            return "ИКС";
                        case 'Y':
                            return "У";
                        case 'Z':
                            return "З";
                        default:
                            throw new Exception("No such symbol");
                    }
                    #endregion
                }
                else
                    throw new Exception("No such direction param");
            }
            return ch.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="direction">RtE - Russian To English; EtR - English To Russian</param>
        /// <returns></returns>
        private string TranslateString(string str, string direction)
        {
            string tmp = string.Empty;
            if (Regex.IsMatch(str, @"\p{IsCyrillic}"))
            {
                foreach (char ch in str)
                {
                    tmp += Translator(ch, direction);
                }
                return tmp;
            }
            else
                return str;
            
        }

        private string ReplaceWrongSympolsOnPrint(string str)
        {
            str = str.Replace("&quot;", @"""");
            return str;
        }
    }
}