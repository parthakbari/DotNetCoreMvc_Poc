using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HegicMvc_Poc.Models
{
    public class ModelVariant
    {
        /// <summary>
        /// Make ID
        /// </summary>
        public Int32 MakeId { get; set; }

        /// <summary>
        /// Model ID
        /// </summary>
        public Int32 ModelId { get; set; }

        /// <summary>
        /// Model Name
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Variant Name
        /// </summary>
        public string VariantName { get; set; }

        /// <summary>
        /// Fuel Type
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// Comprensive Restricted 
        /// </summary>
        public Int32 ComprehensiveRestricted { get; set; }

        /// <summary>
        /// Liability Restricted 
        /// </summary>
        public Int32 LiabilityRestricted { get; set; }

        /// <summary>
        /// Display Model Sequence Number
        /// </summary>
        public Int32 ModelDisplaySequenceNo { get; set; }

        /// <summary>
        /// Display Variant Sequence Nuber 
        /// </summary>
        public Int32 VariantDisplaySequenceNo { get; set; }

        /// <summary>
        /// Gets or sets the model image URL.
        /// </summary>
        /// <value>
        /// The model image URL.
        /// </value>
        public string ModelImageUrl { get; set; }

        /// <summary>
        /// Seting Capacity
        /// </summary>
        public int SetingCapacity { get; set; }

        /// <summary>
        /// Get or set Fast Lane Id
        /// </summary>
        public decimal FastLaneId { get; set; }

        /// <summary>
        /// Gets or sets the is negative model.
        /// </summary>
        /// <value>
        /// The is negative model.
        /// </value>
        public int IsNegativeModel { get; set; }
    }
}
