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
            Massive = 30,
            SuperSized = 50
        }

        enum MapType
        {
            Drylands = 1,
            Lakes = 2,
            Continents = 3,
            Archipelago = 4,
            WaterWorlds = 5,
            Mountains = 6
        }

        enum MapSquareType
        {
            Ocean = 1,
            Sea = 2,
            Land = 3,
            Mountain = 4
        }

        enum Direction
        {
            North = 1,
            South = 2,
            East = 3,
            West = 4
        }

        private PdfBrush MapSquareColor(MapSquareType squareType)
        {
            switch (squareType)
            {
                case MapSquareType.Ocean:
                    return PdfBrushes.CornflowerBlue;
                case MapSquareType.Sea:
                    return PdfBrushes.SkyBlue;
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

        private PdfDocument CreateMap(MapSize mapsize, CanvasSize canvassize, MapType maptype)
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
            grid = CreateGeography(grid, mapsize, maptype);
            grid.Draw(page, new PointF(10, 10));
            return doc;
        }

        private PdfGrid CreateGeography(PdfGrid grid, MapSize mapsize, MapType maptype)
        {
            switch (maptype)
            {
                case MapType.Drylands:
                    foreach (PdfGridRow row in grid.Rows)
                    {
                        row.Style.BackgroundBrush = MapSquareColor(MapSquareType.Land);
                    }
                    return grid;
                case MapType.WaterWorlds:
                    foreach (PdfGridRow row in grid.Rows)
                    {
                        row.Style.BackgroundBrush = MapSquareColor(MapSquareType.Ocean);
                    }
                    int islandCount = (int)mapsize / 4;
                    Debug.WriteLine("islandCount = " + islandCount);
                    for (int i = 0;i < islandCount;i++)
                    {
                        int startX = new Random().Next((int)mapsize - 1);
                        int startY = new Random().Next((int)mapsize - 1);
                        grid.Rows[startX].Cells[startY].Style.BackgroundBrush = MapSquareColor(MapSquareType.Land);
                        int islandSize = new Random().Next(1, (int)mapsize * 2);
                        int x = startX;
                        int y = startY;
                        for (int s = 0;s < islandSize;s++)
                        {
                            int chance = new Random().Next(1, 100);
                            if (chance > 50) { x = startX; y = startY; }
                            int d = new Random().Next(1, 4);
                            Direction direction = (Direction)d;
                            switch (direction)
                            {
                                case Direction.North:
                                    if (y != 0) y--;
                                    break;
                                case Direction.South:
                                    if (y != (int)mapsize - 1) y++;
                                    break;
                                case Direction.East:
                                    if (x != (int)mapsize - 1) x++;
                                    break;
                                case Direction.West:
                                    if (x != 0) x--;
                                    break;
                                default:
                                    throw new NotImplementedException();
                            }
                            int typeChance = new Random().Next(1, 100);
                            if (typeChance > 10)
                            {
                                grid.Rows[x].Cells[y].Style.BackgroundBrush = MapSquareColor(MapSquareType.Land);
                            }
                            else
                            {
                                grid.Rows[x].Cells[y].Style.BackgroundBrush = MapSquareColor(MapSquareType.Mountain);
                            }
                        }
                    }
                    return grid;
                default:
                    throw new NotImplementedException();
            }
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            //MapSize size;
            //Enum.TryParse<MapSize>(comboMapSize.SelectedValue.ToString(), out size);
            CreateMap(MapSize.Normal, CanvasSize.Default, MapType.WaterWorlds).SaveToFile(PDFLocation);
            Process.Start("cmd", "/c start msedge " + PDFLocation);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMap(MapSize.Normal, CanvasSize.Default, MapType.WaterWorlds).SaveToFile(PDFLocation);
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
