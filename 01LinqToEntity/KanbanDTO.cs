
namespace _01LinqToEntity
{
    public class KanbanDTO
    {
        /// <summary>
        /// DeliveryRoutes_DetailBoardCodes.PartNumber
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// DeliveryRoutes_DetailBoardCodes.BoardCode
        /// </summary>
        public string BoardCode { get; set; }
        /// <summary>
        /// DeliveryRoutes_DetailBoardCodes.ContainerType
        /// </summary>
        public string ContainerType { get; set; }
        /// <summary>
        /// DeliveryRoutes_PiecesContainer.Quantity
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// PFEP_Master.UoM
        /// </summary>
        public string UoM { get; set; }
        /// <summary>
        /// PFEP_Master.Description
        /// </summary>
        public string Description { get; set; }
    }
}
