using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SazBaki.Models;

namespace SazBaki.Controllers
{
    public class HomeController : Controller
    {
        SazBakiEntities db = new SazBakiEntities();
        int language_id=1;
        public ActionResult lang_id(int id, string ActionName/*, string languageAbbrevation*/)
        {

            Session["langId"] = id;
            //if (languageAbbrevation != null)
            //{
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageAbbrevation);
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageAbbrevation);
            //}
            //HttpCookie cookie = new HttpCookie("Home");
            //cookie.Value = languageAbbrevation;
            //Response.Cookies.Add(cookie);
            return RedirectToAction(ActionName);

        }
        
        public ActionResult Index()
        {
            if (Session["langId"] != null)
            {
                language_id = Convert.ToInt32(Session["langId"]);
                IndexWiewModel index = new IndexWiewModel();
                index._hometext = db.HomeContents.Where(s => s.home_lang_id == language_id).First();
                index._mpslider = db.MultipeSliders.Where(t => t.mp_lang_id == language_id).ToList();
                return View(index);
            }
            else
            {
                IndexWiewModel index = new IndexWiewModel();
                index._hometext = db.HomeContents.Where(s => s.home_lang_id == language_id).First();
                index._mpslider = db.MultipeSliders.Where(t => t.mp_lang_id == language_id).ToList();
                return View(index);
            }           
        }


        public ActionResult About()
        {
            if (Session["langId"] != null)
            {
                language_id = Convert.ToInt32(Session["langId"]);
                AboutViewModel about = new AboutViewModel();
                about._about = db.Abouts.Where(d => d.about_lang_id == language_id).First();
                return View(about);
            }
            else
            {
                AboutViewModel about = new AboutViewModel();
                about._about = db.Abouts.Where(d => d.about_lang_id == language_id).First();
                return View(about);
            }
        }

        public ActionResult Order()
        {
            if (Session["langId"] != null)
            {
                OrderViewModel order = new OrderViewModel();
                language_id = Convert.ToInt32(Session["langId"]);
                order._order = db.OrderTables.Where(o => o.order_lang_id == language_id).ToList();
                return View(order);
            }
            else
            {
                OrderViewModel order = new OrderViewModel();
                order._order = db.OrderTables.Where(o => o.order_lang_id == language_id).ToList();
                return View(order);
            }
        }

        public ActionResult Gallery()
        {
            GalleryViewModel gallery = new GalleryViewModel();
            gallery._gallery = db.Galleries.ToList();
            return View(gallery);
        }
        public ActionResult Contact()
        {
            ContactViewModel contact = new ContactViewModel();
            contact._contact = db.Contacts.First();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Contact(string asSoyad, string mail, string mailtext)
        {
            string toemail = db.Contacts.FirstOrDefault().contact_email;
            try
            {
                if (ModelState.IsValid)
                {
                    MailMessage mailmessage = new MailMessage();
                    SmtpClient connect = new SmtpClient("smpt.gmail.com", 587);
                    mailmessage.From = new MailAddress("sazbakiwebmaiil@gmail.com", "name", System.Text.Encoding.UTF8);
                    mailmessage.To.Add(toemail);
                    mailmessage.Subject = asSoyad + "  " + mail;
                    Random random = new Random();
                    mailmessage.Body = mailtext;
                    connect.Credentials = new NetworkCredential("sazbakiwebmaiil@gmail.com", "sazbaki123456789");
                    connect.EnableSsl = true;
                    connect.Host = "smtp.gmail.com";
                    connect.Send(mailmessage);
                    return RedirectToAction("Contact");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "There is a problem with mail";
            }
            return View();
        }
        
    }
}