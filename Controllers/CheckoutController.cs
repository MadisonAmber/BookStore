using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class CheckoutController : Controller
    {
        private IPurchaseRepository repo { get; set; }

        private Basket basket { get; set; }

        public CheckoutController(IPurchaseRepository x, Basket b)
        {
            repo = x;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }
        [HttpPost]
        public IActionResult Checkout(Purchase p)
        {
            if(basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }

            if(ModelState.IsValid)
            {
                p.Lines = basket.Items.ToArray();
                repo.SavePurchase(p);
                basket.ClearCart();
                return RedirectToPage("/PurchaseConfirmation");
            }
            else
            {
                return View();
            }
            
        }
    }
}
