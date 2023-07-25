using AutoMapper;
using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoApp.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ContactController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = null;
        

            var sharedUser = GetSharedUserInfo();
            if (sharedUser != null)
            {
                contacts =  _repository.Contact.GetAll().Where(x=>x.UserId==sharedUser.UserId).ToList();
                var itemList = _mapper.Map<List<ContactModel>>(contacts);
              

            }
            return View(contacts);

        }

        public async  Task<IActionResult> Detail(string id)
        {
            Contact contact = await _repository.Contact.FindByIdAsync(Guid.Parse(id));
            var contactModel = _mapper.Map<ContactModel>(contact);
            return PartialView("_Detail", contactModel);
  

        }

        public IActionResult Create() {
            ContactModel model = new ContactModel();

            var sharedUser = GetSharedUserInfo();
            if (sharedUser != null)
            {
                model.UserId = sharedUser.UserId;
            }
            return View(model);  
        }

        [HttpPost]
        public IActionResult Create(ContactModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var contact = _mapper.Map<Contact>(model);
                _repository.Contact.CreateAsync(contact);
                _repository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            ContactModel model = new ContactModel();

            var sharedUser = GetSharedUserInfo();
            if (sharedUser != null)
            {
                var contact = await _repository.Contact.FindByAsync(c => c.Id == Guid.Parse(id) && c.UserId == sharedUser.UserId);
              
                model = _mapper.Map<ContactModel>(contact.FirstOrDefault());
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var sharedUser = GetSharedUserInfo();
                if (sharedUser != null)
                {

                    var contact = _mapper.Map<Contact>(model);
                    _repository.Contact.UpdateAsync(contact);
                    _repository.SaveAsync();
                    return RedirectToAction("Index");
                }

            }

        
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            ContactModel model = new ContactModel();

            var sharedUser = GetSharedUserInfo();
            if (sharedUser != null)
            {
                var contact = await _repository.Contact.FindByAsync(c => c.Id == Guid.Parse(id) && c.UserId == sharedUser.UserId);

                _repository.Contact.DeleteAsync(contact.FirstOrDefault());
                _repository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        private SharedUserModel GetSharedUserInfo() {

            SharedUserModel sharedUser = null;
            // Read the cookie value
            string jsonData = Request.Cookies["SessionUserData"];
            if (!string.IsNullOrEmpty(jsonData))
            { 
                // Deserialize the JSON data to the object
                sharedUser = JsonConvert.DeserializeObject<SharedUserModel>(jsonData);
            }
           
            return sharedUser;
        }

    }
}
