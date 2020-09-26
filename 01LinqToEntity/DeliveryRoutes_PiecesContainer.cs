using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01LinqToEntity
{
    public class DeliveryRoutes_PiecesContainer
    {
        /// <summary>
        /// DeliveryRoutes_PiecesContainer.PartNumber
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// DeliveryRoutes_PiecesContainer.ContainerType
        /// </summary>
        public string ContainerType { get; set; }
        /// <summary>
        /// DeliveryRoutes_PiecesContainer.Quantity
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
