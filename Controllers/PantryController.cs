﻿using PantryCenter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PantryCenter.Controllers
{
    public class PantryController : Controller
    {
        // GET: PantryController
       
        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://localhost:7050/api/PantryInventoryAPI";

            List<Shelves> shelves = new List<Shelves>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                var result = await response.Content.ReadAsStringAsync();
                shelves = JsonConvert.DeserializeObject<List<Shelves>>(result);
            }

            foreach (var shelf in shelves)
            {
                shelf.ItemName = shelf.ItemName?.ToLower();
                shelf.ItemType = shelf.ItemType?.ToLower();
            }


            return View(shelves);
        }

        // GET: PantryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PantryController/Create
        public ActionResult Create()
        {
            return View();
        }

        
        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Shelves shelves)
        {
            string apiUrl = "https://localhost:7050/api/PantryInventoryAPI";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(shelves), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(shelves);
        }




        // GET: PantryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PantryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PantryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PantryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
