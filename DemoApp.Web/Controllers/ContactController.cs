using AutoMapper;
using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoApp.Web.Controllers
{
    public class ContactController : BaseController
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
            // Create an empty list of ContactModel
            List<ContactModel> itemList = new List<ContactModel>();

            // Get the user information from the cookie
            var sharedUser = UserFromCookie;
            if (sharedUser != null)
            {
                // Retrieve contacts associated with the current user from the database
                var contacts = await _repository.Contact.FindByAsync(x => x.UserId == sharedUser.UserId);

                // Map the contacts to ContactModel using the _mapper
                itemList = _mapper.Map<List<ContactModel>>(contacts);
            }

            // Pass the itemList to the view for display
            return View(itemList);
        }




        public IActionResult Create()
        {
            // Initialize a new ContactModel
            var model = new ContactModel();

            // Get the user information from the cookie
            var sharedUser = UserFromCookie;

            // Check if the user information is available
            if (sharedUser != null)
            {
                // If the user information is available, set the UserId property of the model
                model.UserId = sharedUser.UserId;
            }

            // Pass the model to the view for data entry
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(ContactModel model)
        {
            // Remove "Id" from ModelState 
            ModelState.Remove("Id");

            // Check if the model data is valid based on the defined validation rules
            if (ModelState.IsValid)
            {
                // Map the ContactModel to a Contact entity using the mapper
                var contact = _mapper.Map<Contact>(model);

                try
                {
                    // Create the contact asynchronously in the repository
                    _repository.Contact.CreateAsync(contact);

                    // Save the changes asynchronously to the database
                    _repository.SaveAsync();

                    // Redirect to the "Index" action on successful creation
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle any exception that occurs during contact creation or saving
                    // Log the exception or show an error message to the user
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the contact. Please try again later.");
                }
            }

            // If ModelState is not valid, return the same view with validation errors
            return View();
        }


        public async Task<IActionResult> Edit(string id)
        {
            ContactModel model = new ContactModel();

            var sharedUser = UserFromCookie;
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
                var sharedUser = UserFromCookie;
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

            var sharedUser = UserFromCookie;
            if (sharedUser != null)
            {
                var contact = await _repository.Contact.FindByAsync(c => c.Id == Guid.Parse(id) && c.UserId == sharedUser.UserId);

                _repository.Contact.DeleteAsync(contact.FirstOrDefault());
                _repository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Detail(string id)
        {
            Contact contact = await _repository.Contact.FindByIdAsync(Guid.Parse(id));
            var contactModel = _mapper.Map<ContactModel>(contact);
            return PartialView("_Detail", contactModel);


        }

    }
}
