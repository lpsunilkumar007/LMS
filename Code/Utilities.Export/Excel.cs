using System.Text;
using System.Data;
using System;
using System.IO;
using System.Collections.Generic;
using HelperClasses;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace Utilities.Export
{
    public static class Excel
    {
        static Excel()
        {
            //creating the directory if not exists
            FileSystem.createDirectory(FileSystem.ApplicationDirectory + ExcelDocumentsRelativePath);
        }

        #region File Path
        public static string ExcelDocumentsRelativePath
        {
            get
            {
                return @"temp\excelFiles\";
            }
        }
        public static string ExcelDocumentsAbsolutePath
        {
            get { return FileSystem.ApplicationDirectory + ExcelDocumentsRelativePath; }
        }
        #endregion



        #region Extension Code for sending DataSet to Excel Xls
        const int rowLimit = 65000;
        static string getWorksheets(DataSet source, bool hasBorder, ArrayList columnsTobeHidden, Dictionary<string, string> HyperLinkColumns)
        {
            StringWriter sw = new StringWriter();
            try
            {
                if (source == null || source.Tables.Count == 0)
                {
                    sw.Write("<Worksheet ss:Name=\"Sheet1\">\r\n<Table>\r\n<Row><Cell><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                    return sw.ToString();
                }
                foreach (System.Data.DataTable dt in source.Tables)
                {
                    if (dt.Rows.Count == 0)
                        sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\">\r\n<Table>\r\n<Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                    else
                    {
                        //write each row data                
                        Int32 sheetCount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((i % rowLimit) == 0)
                            {
                                //add close tags for previous sheet of the same data table
                                if ((i / rowLimit) > sheetCount)
                                {
                                    sw.Write("\r\n</Table>\r\n</Worksheet>");
                                    sheetCount = (i / rowLimit);
                                }
                                sw.Write("\r\n<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) +
                                         (((i / rowLimit) == 0) ? "" : Convert.ToString(i / rowLimit)) + "\">\r\n<Table>");
                                //write column name row
                                sw.Write("\r\n<Row>");
                                foreach (DataColumn dc in dt.Columns)
                                    if (columnsTobeHidden == null)
                                    {

                                        sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data   ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.ColumnName)));

                                    }
                                    else if (!columnsTobeHidden.Contains(dc.ColumnName))
                                    {
                                        sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data   ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.ColumnName)));
                                    }

                                sw.Write("</Row>");
                            }
                            sw.Write("\r\n<Row>");

                            string hyperLinkColumnValue = "";

                            foreach (DataColumn dc in dt.Columns)
                            {
                                if (HyperLinkColumns != null && HyperLinkColumns.ContainsKey(dc.ColumnName))
                                {
                                    hyperLinkColumnValue = HyperLinkColumns[dc.ColumnName].ToString();
                                }


                                if (columnsTobeHidden == null)
                                {

                                    sw.Write(getCellXml(dc.DataType, dt.Rows[i][dc.ColumnName], true, hyperLinkColumnValue));
                                }

                                else if (!columnsTobeHidden.Contains(dc.ColumnName))
                                {
                                    sw.Write(getCellXml(dc.DataType, dt.Rows[i][dc.ColumnName], true, hyperLinkColumnValue));

                                }
                            }
                            sw.Write("</Row>");
                        }
                        sw.Write("\r\n</Table>\r\n</Worksheet>");
                    }
                }
                return sw.ToString();
            }
            finally { sw = null; if (source != null) { source.Dispose(); source = null; } }
        }
        static string getExcelXml(DataSet dsInput, bool hasBorder, ArrayList columnsTobeHidden, Dictionary<string, string> HyperLinkColumns)
        {
            try { return string.Format(getXMLWorkbookTemplate(), getWorksheets(dsInput, hasBorder, columnsTobeHidden, HyperLinkColumns)); }
            finally { if (dsInput != null) { dsInput.Dispose(); dsInput = null; } }
        }
        static string getXMLWorkbookTemplate()
        {
            StringBuilder sb = new StringBuilder(818);
            try
            {
                sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
                sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
                sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
                sb.AppendFormat(@" <ss:Styles>{0}", Environment.NewLine);
                sb.AppendFormat(@"  <ss:Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Interior/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:NumberFormat/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Protection/>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </ss:Style>{0}", Environment.NewLine);
                sb.AppendFormat(@"  <ss:Style ss:ID=""s62"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   </ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""{0}", Environment.NewLine);
                sb.AppendFormat(@"    ss:Bold=""1""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </ss:Style>{0}", Environment.NewLine);

                sb.AppendFormat(@"  <ss:Style ss:ID=""s64"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Underline=""Single"" ss:Size=""11"" ss:Color=""#0563C1""{0}", Environment.NewLine);
                sb.AppendFormat(@"    ss:Bold=""1""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </ss:Style>{0}", Environment.NewLine);


                sb.AppendFormat(@"  <ss:Style ss:ID=""s65"" ss:Parent=""s64"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   </ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </ss:Style>{0}", Environment.NewLine);



                sb.AppendFormat(@"  <ss:Style ss:ID=""s63"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:NumberFormat ss:Format=""Long Date""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   </ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </ss:Style>{0}", Environment.NewLine);
                sb.AppendFormat(@"  <ss:Style ss:ID=""s60"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   <ss:Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />{0}", Environment.NewLine);
                sb.AppendFormat(@"   </ss:Borders>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </ss:Style>{0}", Environment.NewLine);
                sb.AppendFormat(@" </ss:Styles>{0}", Environment.NewLine);
                sb.Append(@"{0}\r\n</Workbook>");
                return sb.ToString();
            }
            finally { sb = null; }
        }

        static string getCellXml(Type type, object cellData, bool hasBorder, string hyperLinkColumnValue = "")
        {
            object data = (cellData is DBNull) ? "" : cellData;

            string border = "";
            if (hasBorder) { border = @" ss:StyleID=""s60"""; }

            if (type.Name.Contains("Int") || type.Name.Contains("Double") || type.Name.Contains("Decimal") || type.Name.Contains("decimal"))
            {
                return string.Format("<Cell" + border + "><Data ss:Type=\"Number\">{0}</Data></Cell>", data);
            }


            if (type.Name.Contains("Date") && data.ToString() != string.Empty)
            {
                return string.Format("<Cell ss:StyleID=\"s63\"><Data ss:Type=\"DateTime\">{0}</Data></Cell>", Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
                //return string.Format("<Cell" + border + "><Data ss:Type=\"DateTime\">{0}</Data></Cell>", Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
            }

            if (data.ToString().Contains("http://")) // HyperLink column
            {

                if (string.IsNullOrEmpty(hyperLinkColumnValue))
                    hyperLinkColumnValue = data.ToString();
                return string.Format("<Cell ss:StyleID=\"s65\" ss:HRef=\"{0}\"><Data ss:Type=\"String\">" + replaceXmlChar(hyperLinkColumnValue) + "</Data></Cell>", replaceXmlChar(data.ToString()));
            }

            if (type.Name.Contains("String"))
            {
                return string.Format("<Cell " + border + "><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(data.ToString()));
            }


            decimal nad = 0;
            if (decimal.TryParse(cellData.ToString(), out nad))
            {
                return string.Format("<Cell" + border + "><Data ss:Type=\"Number\" ss:NumberFormat=\"@\" >{0}</Data></Cell>", data);
            }

            return string.Format("<Cell" + border + "><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(data.ToString()));
        }
        static string replaceXmlChar(string input)
        {
            input = input.Replace("&", "&amp");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            input = input.Replace("\"", "&quot;");
            input = input.Replace("'", "&apos;");
            return input;
        }
        #endregion

        /// <summary>
        /// Convert Data Set o Excel
        /// </summary>
        /// <param name="dsInput"></param>
        /// <param name="extension"></param>
        /// <param name="hasBorder">Add order around the coumns</param>
        /// <returns>Path of the created excel</returns>
        public static string ToExcel(DataSet dsInput, string extension = ".xls", bool hasBorder = true, ArrayList columnsTobeHidden = null, Dictionary<string, string> HyperLinkColumns = null, bool getFileName = false)
        {
            if (dsInput == null) return "";
            if (dsInput.Tables.Count == 0) return "";
            try
            {
                //used to create file name for excel
                //creating Full file name .. merging extension and fileName
                string filename = Guid.NewGuid().ToString() + extension;
                //getting XML string for Excel
                string excelXml = getExcelXml(dsInput, hasBorder, columnsTobeHidden, HyperLinkColumns);
                //creating Full file name .. merging extension and fileName
                //filename = filename + extension;
                //giving dfault file name to new name
                //string newFileName = filename;
                //default path of temp folder
                string excelTempPath = ExcelDocumentsRelativePath;
                //creating full path of file
                string path = FileSystem.ApplicationDirectory + excelTempPath + filename;
                ////checking file exists
                //if (FileSystem.fileExists(path))
                //{
                //    //if file exists create a new file
                //    newFileName = FileSystem.getFileName(FileSystem.ApplicationDirectory + excelTempPath, filename);
                //    //setting path with new name
                //    path = FileSystem.ApplicationDirectory + excelTempPath + newFileName;
                //}
                //writing XML to new file
                File.WriteAllText(path, excelXml);
                if (getFileName)
                {
                    return filename;
                }
                return path;
            }
            finally { if (dsInput != null) { dsInput.Dispose(); dsInput = null; } }
        }
        /// <summary>
        /// Convert Datatable to excel
        /// </summary>
        /// <param name="dataTbl"></param>
        /// <param name="extension"></param>
        /// <param name="hasBorder">Add order around the coumns</param>
        /// <returns>Path of the created excel</returns>
        public static string ToExcel(DataTable dataTbl, string extension = ".xls", bool hasBorder = true, bool getFileName = false)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.Clear();
                ds.Tables.Add(dataTbl);
                return ToExcel(ds, extension, hasBorder, getFileName: getFileName);
            }
            finally { if (ds != null) { ds.Dispose(); ds = null; } }
        }

        /// <summary>
        /// Convert List to excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="extension"></param>
        /// <param name="hasBorder">Add order around the coumns</param>
        /// <param name="columnsTobeHidden">Send exact column names from domain that needs to be hidden while exporting</param>
        /// <param name="HyperLinkColumns">Add the column names that are hyperlinks and then the text to be displayed in value field. Column name should match</param>
        /// <returns>Path of the created excel</returns>
        public static string ToExcel<T>(IList<T> source, string extension = ".xls", bool hasBorder = true, ArrayList columnsTobeHidden = null, Dictionary<string, string> HyperLinkColumns = null, bool getFileName = false)
        {
            DataSet dataset = new DataSet();
            try
            {
                dataset = source.ToDataSet();
                return ToExcel(dataset, extension, hasBorder, columnsTobeHidden: columnsTobeHidden, HyperLinkColumns: HyperLinkColumns, getFileName: getFileName);
            }
            finally { if (dataset != null) { dataset.Dispose(); dataset = null; } }
        }


        #region Private Code
        static DataSet ToDataSet<T>(this IList<T> source)
        {
            DataSet ds = new DataSet(typeof(T).Name);
            DataTable dataTable = new DataTable(typeof(T).Name);
            ds.Tables.Add(dataTable);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                string columnName = "";
                columnName = GetAttributeDisplayName(prop);
                if (string.IsNullOrEmpty(columnName))
                {
                    columnName = prop.Name;
                }
                dataTable.Columns.Add(columnName);
            }
            foreach (T item in source)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        static string GetAttributeDisplayName(PropertyInfo property)
        {
            try
            {
                var atts = property.GetCustomAttributes(
                    typeof(DisplayNameAttribute), true);
                if (atts.Length == 0)
                    return null;
                return (atts[0] as DisplayNameAttribute).DisplayName;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
