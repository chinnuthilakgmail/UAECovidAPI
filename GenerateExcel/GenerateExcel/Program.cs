using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;

namespace GenerateExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<Author> authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Joydip", LastName = "Kanjilal" },
                new Author { Id = 2, FirstName = "Steve", LastName = "Smith" },
                new Author { Id = 3, FirstName = "Anand", LastName = "Narayaswamy"}
            };

            DataTable dt = ToDataTable<Author>(authors);

           string fileName = DataTableToExcel(dt);

            ReadExcelFile(fileName);
        }

        private static void ReadExcelFile(string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excelApp =  new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelworkBook = excelApp.Workbooks.Open(fileName); ;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange; 

            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;

 
            for (int i = 1; i <= rowCount; i++)
            {
                //create new line
                Console.Write("\r\n");
                for (int j = 1; j <= colCount; j++)
                {

                    //write the console
                    if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j] != null)
                        Console.Write(excelRange.Cells[i, j].ToString() + "\t");
                }
            }

        
            //after reading, relaase the excel project
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            Console.ReadLine();
 
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static string DataTableToExcel(DataTable dt)
        {
            string fileName = string.Format(@"C:\Users\Chinnu\source\repos\UAECovidAPI\GenerateExcel\GenerateExcel\ExcelFiles\{0}", DateTime.Now.ToString("ddMMyyyyHH:mm") +".xlsx");
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            excel = new Microsoft.Office.Interop.Excel.Application();
            excelworkBook = excel.Workbooks.Add(Type.Missing);
            excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "Test work sheet";

            //excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[dt.Rows.Count, dt.Columns.Count]];
            //excelCellrange.EntireColumn.AutoFit();
            //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
            //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //border.Weight = 2d;

            // loop through each row and add values to our sheet
            int rowcount = 2;

            foreach (DataRow datarow in dt.Rows)
            {
                rowcount += 1;
                for (int i = 1; i <= dt.Columns.Count; i++)
                {
                    // on the first iteration we add the column headers
                    if (rowcount == 3)
                    {
                        excelSheet.Cells[2, i] = dt.Columns[i - 1].ColumnName;
                        excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                    }

                    excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                    //for alternate rows
                    if (rowcount > 3)
                    {
                        if (i == dt.Columns.Count)
                        {
                            if (rowcount % 2 == 0)
                            {
                                excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dt.Columns.Count]];
                               // FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                            }

                        }
                    }

                }

            }

            // now we resize the columns
            excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dt.Columns.Count]];
            excelCellrange.EntireColumn.AutoFit();
            Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;


            excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dt.Columns.Count]];
           // FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


            //now save the workbook and exit Excel


            excelworkBook.SaveAs(fileName); 
            excelworkBook.Close();
            excel.Quit();

            return fileName;
        }

        
    }

    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
