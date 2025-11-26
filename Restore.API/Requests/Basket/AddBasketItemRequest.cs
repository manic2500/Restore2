using System;
using System.ComponentModel.DataAnnotations;

namespace Restore.API.Requests.Basket;

public class BasketItemRequest
{
    //public Guid? BasketXid { get; set; }

    [Required(ErrorMessage = "The product id is required.")]
    public Guid ProductId { get; set; }


    [Required(ErrorMessage = "The quantity is required.")]
    //[Range(1, 99, ErrorMessage = "The quantity must be between 1 and 99.")]
    public int Quantity { get; set; }
}
