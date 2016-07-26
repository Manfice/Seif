using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Seif.Models.SeifData;
using Seif.Repos;
using Seif.ViewModels;

namespace Seif.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IProducts _products;

        public CartController(IProducts products)
        {
            _products = products;
        }
        // GET: Cart
        public ActionResult Index(Cart cart, string returnUrl)
        {
            var model = new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public ActionResult Add(Cart cart,int pId, string returnUrl)
        {
            var p = _products.GetProduct(pId);
            cart.AddToCart(p,1);
            return RedirectToAction("Index","Cart", new {returnUrl});
        }

        public ActionResult RemoveFromCart(Cart cart,int pId, string returnUrl)
        {
            cart.RemoveLine(pId);
            return Redirect(returnUrl);
        }
        public ActionResult AddToCart(Cart cart, int pId,string returnUrl)
        {
            cart.AddToCart(_products.GetProduct(pId),1);
            return Redirect(returnUrl);
        }
        public ActionResult Remove(Cart cart, int pId, string returnUrl)
        {
            var p = _products.GetProduct(pId);
            cart.AddToCart(p, -1);
            return RedirectToAction("Index","Cart", returnUrl);
        }
        [Authorize]
        public ActionResult Thanks(Guid orId)
        {
            var model = _products.GetOrder(orId);
            return View(model);
        }

        public ActionResult CheckOrder(Guid id)
        {
            var model = _products.GetOrder(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeOrder(Cart cart,CartViewModel cartvm)
        {
            cartvm.Cart = cart;
            FormsAuthentication.SetAuthCookie(cartvm.Name,false);
            var i = _products.MakeOrder(cartvm);
            cart.ClearCart();
            var link = @"http://stavmet.pro" + Url.Action("CheckOrder","Cart",new {id=i});
            var order = _products.GetOrder(i);
            var sb = $"Ваш заказ №{order.Id} от {order.OrderDAte.Date.ToLongDateString()} с сайта stavmet.pro";
            await SendMyMailAsync(link, cartvm.EMail, sb);
            await SendMyMailAsync(link, "c592@yandex.ru", sb);
            return RedirectToAction("Thanks", "Cart", new {orId = i});
        }

        private async Task<string> SendMyMailAsync(string body, string to, string subject)
        {
            var fromAddress = new MailAddress("admin@id-racks.ru", "StavMet.pro - Интернет магазин");
            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "1q2w3eZX")
            };
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, errors) => true;
            using (var message = new MailMessage(fromAddress, new MailAddress(to))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
            return $"Письмо отправленно на адрес: {to}";
        }
    }
}