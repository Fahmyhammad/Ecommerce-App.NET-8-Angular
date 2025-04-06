﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Entites.Product
{
    public class Product:BaseEntity<int>
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public decimal NewPrice {  get; set; }
        public decimal OldPrice {  get; set; }
        public virtual List<Photo> Photos { get; set; } 
        public int CategoryId {  get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
