using AutoMapper;
using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            List<Contact> contacts = _repository.Contact.GetAll().ToList();
            var itemList = _mapper.Map<List<ContactModel>>(contacts);
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
            string userName = HttpContext.Session.GetString("UserName");
            var existingUser = _repository.User.FindBy(x => x.Username == userName).FirstOrDefault();
            model.UserId = existingUser.Id.ToString();
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

    }
}
