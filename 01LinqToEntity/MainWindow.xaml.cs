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
            #region 方法一，lambda实现合并查询，左关联（没匹配到置为null），转为实体队列
            var result = boards.Join(containers, c => c.PartNumber, b => b.PartNumber, (b, c) => new { b, c })
                .Where(w => w.c.ContainerType == w.b.ContainerType)
                .GroupJoin(pfeps, a => a.b.PartNumber, p => p.PartNumber, (a, p) => p.DefaultIfEmpty()
                .Select(z => new KanbanDTO
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
            #endregion 

            #region Linq
            //查询两个list的所有数据 linq func1
            var selectAllResult = from b in boards
                                  join c in containers
                                  on new { b.PartNumber, b.ContainerType } equals new { PartNumber = c.PartNumber, c.ContainerType }
                                  select new { PartNumber = b.PartNumber, c.ContainerType, BoardCode = b.BoardCode, Quantity = c.Quantity };



            //查询两个list的所有数据 linq func2
            var selectAllResult2 = from b in boards
                                   from c in containers
                                   where b.PartNumber == c.PartNumber && b.ContainerType == c.ContainerType
                                   select new { PartNumber = b.PartNumber, c.ContainerType, BoardCode = b.BoardCode, Quantity = c.Quantity };


            // 查询结果左关联pfeps
            var machingRowsLinq = from a in selectAllResult
                                  join p in pfeps
                                  //on a.Field<string>("PartNumber") equals p.Field<string>("PartNumber") into tmp //.NET CORE 找不到Field属性，直接通过字段关联
                                  on a.PartNumber equals p.PartNumber into tmp
                                  from ptmp in tmp.DefaultIfEmpty()
                                      //where ptmp == null //添加过滤条件
                                  select new KanbanDTO { PartNumber = a.PartNumber, ContainerType = a.ContainerType, BoardCode = a.BoardCode, Quantity = a.Quantity, UoM = ptmp == null ? "null" : ptmp.UoM, Description = ptmp == null ? "null" : ptmp.Description };
            #endregion



            #region Lambda
            //查询两个list所有数据 lambda (inner join)
            var selectAllRowsLambda = boards.Join(containers, b => b.PartNumber, c => c.PartNumber, (b, c) => new { b, c })
                .Where(w => w.c.ContainerType == w.b.ContainerType);

            //查询结果左关联pfeps
            var machingRowsLambda = selectAllRowsLambda
                .GroupJoin(pfeps,
                x => x.b.PartNumber,
                y => y.PartNumber,
                (x, y) => y.DefaultIfEmpty()
                .Select(z => new KanbanDTO
                {
                    PartNumber = x.b.PartNumber,
                    BoardCode = x.b.BoardCode,
                    ContainerType = x.b.ContainerType,
                    Quantity = x.c.Quantity,
                    UoM = z == null ? "null" : z.UoM,
                    Description = z == null ? "null" : z.Description
                })
                ).SelectMany(x => x);

            #endregion


            // var result =

            //ToDo : Print the results using lambda expression
            // Print Linq Result
            foreach (var item in machingRowsLinq)
            {
                lstResult.Items.Add(string.Format("{0}:{1}/{2}/{3}/{4}/{5}", item.BoardCode, item.PartNumber, item.ContainerType, item.Quantity, item.UoM, item.Description).ToString());
            }

            //Print Lambda Result
            foreach (var item in machingRowsLambda)
            {
                lstResult.Items.Add(string.Format("{0}:{1}/{2}/{3}/{4}/{5}", item.BoardCode, item.PartNumber, item.ContainerType, item.Quantity, item.UoM, item.Description).ToString());
            }


            // print the rusults using lambda expression
            machingRowsLambda.ToList().FindAll(i =>
            {
                lstResult.Items.Add(string.Format("{0}:{1}/{2}/{3}/{4}/{5}", i.BoardCode, i.PartNumber, i.ContainerType, i.Quantity, i.UoM, i.Description).ToString());
                return true;
            });

        }

    }
}
