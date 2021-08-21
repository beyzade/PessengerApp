using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PessengerApp.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using PessengerApp.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using PessengerApp.Cache;

namespace caching.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private int _pageSize;

        public HomeController(IConfiguration configuration, ApplicationDbContext context, ICacheService cacheService, IHtmlHelper htmlHelper)
        {
            this._configuration = configuration;
            this._context = context;
            this._cacheService = cacheService;
            this._htmlHelper = htmlHelper;
            this._pageSize = _configuration.GetValue<int>("PageSize");
        }

        public IActionResult Index(OptionType? optionType, int optionValue = 0, int startPage = 0)
        {
            if (optionType == null)
                optionType = OptionType.Status;

            ViewBag.OptionType = (int)optionType;
            ViewBag.OptionValue = optionValue;

            SetViewBagEnums(ViewBag);

            var fromPage = startPage * _pageSize;
            ViewBag.PageSize = _pageSize;
            ViewBag.NextPage = startPage + 1;
            ViewBag.PreviousPage = startPage - 1;
            
            List<Pessenger> pessengers = new List<Pessenger>();
            if (optionType != null)
            {
                LoadIntoCache(optionType.Value, optionValue, false);
                var psnList = _cacheService.Get<List<Pessenger>>($"{optionType.ToString()}_{optionValue}");
                if (psnList?.Count > 0)
                {
                    pessengers.AddRange(psnList);
                }
            }

            ViewBag.TotalRows = pessengers.Count;
            ViewBag.LastPage = (int)pessengers.Count / _pageSize;
            pessengers = pessengers.Skip(fromPage).Take(_pageSize).ToList();

            ViewBag.ShowMessage = optionType.HasValue;
            return View(pessengers);
        }

        private void LoadIntoCache(OptionType optionType, int optionValue, bool isRefresh = false)
        {
            if (isRefresh)
            {
                _cacheService.Remove($"{optionType.ToString()}_{optionValue}");
            }
            else
            {
                var list = _cacheService.Get<List<Pessenger>>($"{optionType.ToString()}_{optionValue}");
                if(list == null)
                {
                    isRefresh = true;
                }
            }

            if (isRefresh)
            {
                var list = optionType == OptionType.Status ? _context.Pessengers.Where(x => x.Status == (Status)optionValue).ToList():
                    _context.Pessengers.Where(x => x.Country == (Country)optionValue).ToList();

                if (list?.Count > 0)
                {
                    var timeToLeft = TimeSpan.FromMinutes(this._configuration.GetValue<int>($"CacheDuration:{optionType.ToString()}"));
                    _cacheService.Add($"{optionType.ToString()}_{optionValue}", list, timeToLeft);
                }
            }
        }

        [HttpGet]
        public IActionResult New()
        {
            SetViewBagEnums(ViewBag);
            return View(new Pessenger { IssueDate=DateTime.Today});
        }

        [HttpPost]
        public IActionResult New(Pessenger pessenger)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagEnums(ViewBag);
                return View(pessenger);
            }

            _context.Pessengers.Add(pessenger);
            _context.SaveChanges();

            var ttlForId = TimeSpan.FromMinutes(this._configuration.GetValue<int>("CacheDuration:Id"));
            _cacheService.Add($"Id_{pessenger.Id}", pessenger, ttlForId);

            LoadIntoCache(OptionType.Status, (int)pessenger.Status, true);
            LoadIntoCache(OptionType.Country, (int)pessenger.Country, true);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id, int currentPage)
        {
            SetViewBagEnums(ViewBag);
            var pessenger = _cacheService.Get<Pessenger>($"Id_{id}");
            if(pessenger == null)
            {
                pessenger = _context.Pessengers.FirstOrDefault(x => x.Id == id);
                var ttlForId = TimeSpan.FromMinutes(this._configuration.GetValue<int>("CacheDuration:Id"));
                _cacheService.Add($"Id_{id}", pessenger, ttlForId);
            }

            ViewBag.CurrentPage = currentPage;
            ViewBag.Genders = _htmlHelper.GetEnumSelectList<Gender>();
            ViewBag.DocumentTypes = _htmlHelper.GetEnumSelectList<DocumentType>();
            return View(pessenger);
        }

        [HttpPost]
        public IActionResult Edit(Pessenger pessenger, int currentPage)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagEnums(ViewBag);
                return View(pessenger);
            }

            _context.Pessengers.Update(pessenger);
            _context.SaveChanges();

            _cacheService.Remove($"Id_{pessenger.Id}");
            var ttlForId = TimeSpan.FromMinutes(this._configuration.GetValue<int>("CacheDuration:Id"));
            _cacheService.Add($"Id_{pessenger.Id}", pessenger, ttlForId);

            LoadIntoCache(OptionType.Status, (int)pessenger.Status, true);
            LoadIntoCache(OptionType.Country, (int)pessenger.Country, true);

            return RedirectToAction(nameof(Index), new { startPage = currentPage });
        }

        public IActionResult Delete(int id, int currentPage)
        {
            var pessenger = _context.Pessengers.FirstOrDefault(x => x.Id == id);
            _context.Pessengers.Remove(pessenger);
            _context.SaveChanges();

            _cacheService.Remove($"Id_{pessenger.Id}");
            var ttlForId = TimeSpan.FromMinutes(this._configuration.GetValue<int>("CacheDuration:Id"));
            _cacheService.Add($"Id_{pessenger.Id}", pessenger, ttlForId);

            LoadIntoCache(OptionType.Status, (int)pessenger.Status, true);
            LoadIntoCache(OptionType.Country, (int)pessenger.Country, true);

            return RedirectToAction(nameof(Index), new { startPage = currentPage });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetViewBagEnums(dynamic ViewBag)
        {
            ViewBag.OptionTypes = _htmlHelper.GetEnumSelectList<OptionType>();
            ViewBag.DocumentTypes = _htmlHelper.GetEnumSelectList<DocumentType>();
            ViewBag.Statuses = _htmlHelper.GetEnumSelectList<Status>();
            ViewBag.Countries = _htmlHelper.GetEnumSelectList<Country>();
            ViewBag.Genders = _htmlHelper.GetEnumSelectList<Gender>();
        }

        [HttpGet]
        public JsonResult GetOptionValues(OptionType? value)
        {
            var values = new Dictionary<int, string>();
            if (value == OptionType.Status)
            {
                values = Enum.GetValues(typeof(Status)).Cast<Status>().ToDictionary(t => (int)t, t => t.ToString());
            }
            else if (value == OptionType.Country)
            {
                values = Enum.GetValues(typeof(Country)).Cast<Country>().ToDictionary(t => (int)t, t => t.ToString());
            }

            return new JsonResult(values);

        }
    }
}
