using Spire.Pdf.Graphics;
using Spire.Pdf.Grid;
using Spire.Pdf;
using System.Diagnostics;
using System;

namespace pdfScratcher
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        const string PDFLocation = "C:\\Users\\ruper\\OneDrive\\Desktop\\Output.pdf";

        enum MapSize
        {
            Tiny = 11,
            Small = 14,
            Normal = 16,
            Large = 18,
            Huge = 20,
            Massive = 30
        }

        enum MapType
        {
            Drylands = 1,
            Lakes = 2,
            Continents = 3,
            Archipelago = 4,
            WaterWorlds = 5
        }

        enum MapSquareType
        {
            Ocean = 1,
            Sea = 2,
            Land = 3,
            Mountain = 4
        }

        private PdfBrush MapSquareColor(MapSquareType squareType)
        {
            switch (squareType)
            {
                case MapSquareType.Ocean:
                    return PdfBrushes.SlateBlue;
                case MapSquareType.Sea:
                    return PdfBrushes.LightBlue;
                case MapSquareType.Land:
                    return PdfBrushes.Green;
                case MapSquareType.Mountain:
                    return PdfBrushes.Gray;
                default:
                    throw new NotImplementedException();
            }
        }

        enum CanvasSize
        {
            Default = 500
        }

        private PdfDocument CreateMap(MapSize mapsize, CanvasSize canvassize)
        {
            PdfDocument doc = new PdfDocument();
            PdfPageBase page = doc.Pages.Add();
            PdfGrid grid = new PdfGrid();
            grid.Style.CellPadding = new PdfPaddings(1, 1, 1, 1);
            grid.Columns.Add((int)mapsize);
            for (int i = 0; i < (int)mapsize; i++)
            {
                grid.Rows.Add();
            }
            foreach (PdfGridColumn col in grid.Columns)
            {
                col.Width = (int)canvassize / (int)mapsize;
            }
            foreach (PdfGridRow row in grid.Rows)
            {
                row.Height = (int)canvassize / (int)mapsize;
            }
            grid = CreateGeography(grid, mapsize);
            grid.Draw(page, new PointF(10, 10));
            return doc;
        }

        private PdfGrid CreateGeography(PdfGrid grid, MapSize mapsize)
        {
            foreach (PdfGridRow row in grid.Rows)
            {
                row.Style.BackgroundBrush = MapSquareColor(MapSquareType.Ocean);
            }

            int islandCount = (int)mapsize / 4;
            Debug.WriteLine("islandCount = " +  islandCount);

            int startX = new Random().Next((int)mapsize - 1);
            int startY = new Random().Next((int)mapsize - 1);

            grid.Rows[startX].Cells[startY].Style.BackgroundBrush = MapSquareColor(MapSquareType.Land);


            //grid.Rows[4].Cells[4].Style.BackgroundBrush = MapSquareColor(MapSquareType.Land);
            //grid.Rows[4].Cells[5].Style.BackgroundBrush = MapSquareColor(MapSquareType.Mountain);
            //grid.Rows[5].Cells[4].Style.BackgroundBrush = MapSquareColor(MapSquareType.Sea);
            //grid.Rows[5].Cells[5].Style.BackgroundBrush = MapSquareColor(MapSquareType.Sea);

            return grid;
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            //MapSize size;
            //Enum.TryParse<MapSize>(comboMapSize.SelectedValue.ToString(), out size);
            CreateMap(MapSize.Tiny, CanvasSize.Default).SaveToFile(PDFLocation);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGeneratePDF_Click((object)this, EventArgs.Empty);
            Process.Start("cmd", "/c start msedge " + PDFLocation);
            foreach (var item in Enum.GetValues(typeof(MapSize)))
            {
                comboMapSize.Items.Add(item);
            }
            comboMapSize.SelectedIndex = 0;
            Application.Exit();
        }
    }
}
