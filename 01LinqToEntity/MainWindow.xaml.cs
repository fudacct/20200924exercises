using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace _01LinqToEntity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            DeliveryRoutes_DetailBoardCodes board1 = new DeliveryRoutes_DetailBoardCodes()
            { PartNumber = "33372748", BoardCode = "20001", ContainerType = "C" };
            DeliveryRoutes_DetailBoardCodes board2 = new DeliveryRoutes_DetailBoardCodes()
            { PartNumber = "33372756", BoardCode = "20002", ContainerType = "F" };
            DeliveryRoutes_DetailBoardCodes board3 = new DeliveryRoutes_DetailBoardCodes()
            { PartNumber = "33372750", BoardCode = "20003", ContainerType = "C" };

            DeliveryRoutes_PiecesContainer container1 = new DeliveryRoutes_PiecesContainer()
            { PartNumber = "33372748", ContainerType = "C", Quantity = new decimal(110.003) };
            DeliveryRoutes_PiecesContainer container2 = new DeliveryRoutes_PiecesContainer()
            { PartNumber = "33372756", ContainerType = "F", Quantity = new decimal(220.003) };
            DeliveryRoutes_PiecesContainer container3 = new DeliveryRoutes_PiecesContainer()
            { PartNumber = "33372755", ContainerType = "C", Quantity = new decimal(330.003) };

            PFEP_Master pfep1 = new PFEP_Master() { PartNumber = "33372759", UoM = "PC", Description = "SRS PIGTAIL" };
            PFEP_Master pfep2 = new PFEP_Master() { PartNumber = "33372748", UoM = "M", Description = "SRS" };
            PFEP_Master pfep3 = new PFEP_Master() { PartNumber = "33372757", UoM = "ROL", Description = "PIGTAIL" };

            List<DeliveryRoutes_DetailBoardCodes> boards = new List<DeliveryRoutes_DetailBoardCodes>() { board1, board2, board3 };
            List<DeliveryRoutes_PiecesContainer> containers = new List<DeliveryRoutes_PiecesContainer>() { container1, container2, container3 };
            List<PFEP_Master> pfeps = new List<PFEP_Master>() { pfep1, pfep2, pfep3 };

            //ToDo : add Linq

            var result = boards.Join(containers, c => c.PartNumber, b => b.PartNumber, (b, c) => new { b, c }).Where(w => w.c.ContainerType == w.b.ContainerType)
                .GroupJoin(pfeps, a => a.b.PartNumber, p => p.PartNumber, (a, p) => p.DefaultIfEmpty().Select(z => new KanbanDTO
                {
                    PartNumber = a.b.PartNumber,
                    BoardCode = a.b.BoardCode,
                    ContainerType = a.b.ContainerType,
                    Quantity = a.c.Quantity,
                    UoM = z == null ? "null" : z.UoM,
                    Description = z == null ? "null" : z.Description
                })).SelectMany(x => x).ToList();


            // var result = 

            //ToDo : Print the results using lambda expression
            foreach (var item in result)
            {
                lstResult.Items.Add(string.Format("{0}:{1}/{2}/{3}/{4}/{5}", item.BoardCode, item.PartNumber, item.ContainerType, item.Quantity, item.UoM, item.Description).ToString());
            }

        }

    }
}
