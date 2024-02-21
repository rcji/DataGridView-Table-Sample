using GemBox.Spreadsheet.WinFormsUtilities;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osorio___Week_4_Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

       
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }  
            public int Birthyear { get; set; }

        }


        List<Person> people = new List<Person>();

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
            "XLS files (*.xls, *.xlt)|*.xls;*.xlt|" +
            "XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm|" +
            "ODS files (*.ods, *.ots)|*.ods;*.ots|" +
            "CSV files (*.csv, *.tsv)|*.csv;*.tsv|" +
            "HTML files (*.html, *.htm)|*.html;*.htm";
            openFileDialog.FilterIndex = 2;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var workbook = ExcelFile.Load(openFileDialog.FileName);
                var worksheet = workbook.Worksheets.ActiveWorksheet;
                // From ExcelFile to DataGridView.
                DataGridViewConverter.ExportToDataGridView(
                worksheet,
                this.dataGridView1,
                new ExportToDataGridViewOptions() { ColumnHeaders = true }); ;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter =
            "XLS (*.xls)|*.xls|" +
            "XLT (*.xlt)|*.xlt|" +
            "XLSX (*.xlsx)|*.xlsx|" +
            "XLSM (*.xlsm)|*.xlsm|" +
            "XLTX (*.xltx)|*.xltx|" +
            "XLTM (*.xltm)|*.xltm|" +
            "ODS (*.ods)|*.ods|" +
            "OTS (*.ots)|*.ots|" +
            "CSV (*.csv)|*.csv|" +
            "TSV (*.tsv)|*.tsv|" +
            "HTML (*.html)|*.html|" +
            "MHTML (.mhtml)|*.mhtml|" +
            "PDF (*.pdf)|*.pdf|" +
            "XPS (*.xps)|*.xps|" +
            "BMP (*.bmp)|*.bmp|" +
            "GIF (*.gif)|*.gif|" +
            "JPEG (*.jpg)|*.jpg|" +
            "PNG (*.png)|*.png|" +
            "TIFF (*.tif)|*.tif|" +
            "WMP (*.wdp)|*.wdp";
            saveFileDialog.FilterIndex = 3;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("Sheet1");
                // From DataGridView to ExcelFile.
                DataGridViewConverter.ImportFromDataGridView(
                worksheet,
                this.dataGridView1,
                new ImportFromDataGridViewOptions() { ColumnHeaders = true });
                workbook.Save(saveFileDialog.FileName);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            people.Add(new Person { Name = textBoxName.Text, 
                                    Age = int.Parse(textBoxAge.Text),  
                                    Gender = textBoxGender.Text, 
                                    Birthyear = int.Parse(textBoxBirthyear.Text)});

            //int rowCount = people.Count;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = people;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;

            people.RemoveAt(selectedIndex);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = people;  
        }
    }
}
