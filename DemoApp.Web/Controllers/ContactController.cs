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
        private readonly ILogger<ContactController> _logger;
        public ContactController(IRepositoryWrapper repository, IMapper mapper, ILogger<ContactController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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
                var contacts = _repository.Contact.GetAll().Where(x => x.UserId == sharedUser.UserId);

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

                    TempData["success"] = "Contact added succecfully";
                    // Redirect to the "Index" action on successful creation
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle any exception that occurs during contact creation or saving
                    // Log the exception or show an error message to the user
                    _logger.LogError("Contact->Create: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the contact. Please try again later.");
                }
            }

            // If ModelState is not valid, return the same view with validation errors
            return View();
        }


        public async Task<IActionResult> Edit(string id)
        {
            var sharedUser = UserFromCookie;
            if (sharedUser == null)
            {
                // Handle the case when the user is not authenticated or not found.
                _logger.LogWarning("Contact->Edit:  Handle the case when the user is not authenticated or not found.");
                return Unauthorized();
            }

            Guid contactId = Guid.Parse(id);
            var contact = await _repository.Contact.FindByAsync(c => c.Id == contactId && c.UserId == sharedUser.UserId);
            var contactModel = _mapper.Map<ContactModel>(contact.SingleOrDefault());

            if (contactModel == null)
            {
                // Handle the case when the contact is not found for the given user.
                _logger.LogWarning("Contact->Edit: Handle the case when the contact is not found for the given user.");
                return NotFound();
            }

            return View(contactModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactModel model)
        {
            // Check if the model data passed from the form is valid
            if (ModelState.IsValid)
            {
                // Get the shared user from the cookie 
                var sharedUser = UserFromCookie;

                // Make sure we have a valid user
                if (sharedUser != null)
                {
                    // Map the ContactModel to a Contact entity using AutoMapper
                    var contact = _mapper.Map<Contact>(model);
             
                    // await the UpdateAsync method to ensure the contact is updated in the repository.
                    await _repository.Contact.UpdateAsync(contact);

                    // Await the SaveAsync method to ensure the changes are persisted to the database.
                    await _repository.SaveAsync();

                    TempData["success"] = "Contact updated succecfully";
                    // Redirect to the "Index" action of the controller after the update is successful.
                    return RedirectToAction("Index");
                }
            }

            // If the model data is invalid or the user is not valid, return the view with the provided model.
            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var sharedUser = UserFromCookie;

            if (sharedUser == null)
            {
                // Handle the case when the user is not authenticated or not found.
                _logger.LogWarning("Contact->Delete: Handle the case when the user is not authenticated or not found.");
                return Unauthorized();
            }

            Guid contactId = Guid.Parse(id);
            var contact = await _repository.Contact.FindByAsync(c => c.Id == contactId && c.UserId == sharedUser.UserId);

            if (!contact.Any())
            {
                // Handle the case when the contact is not found for the given user.
                _logger.LogWarning("Contact->Delete: Handle the case when the contact is not found for the given user..");
                return NotFound();
            }

            //  DeleteAsync and SaveAsync are asynchronous methods in the repository.
            await _repository.Contact.DeleteAsync(contact.SingleOrDefault());
            await _repository.SaveAsync();

            TempData["success"] = "Contact deleted succecfully";

            // Redirect to the "Index" action of the controller after the deletion is successful.
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Detail(string id)
        {
            if (!Guid.TryParse(id, out Guid contactId))
            {
                _logger.LogWarning("Contact->Detail: Handle the case when the provided ID is not a valid GUID.");
                // Handle the case when the provided ID is not a valid GUID.
                return BadRequest();
            }

            Contact contact = await _repository.Contact.FindByIdAsync(contactId);

            if (contact == null)
            {
                _logger.LogWarning("Contact->Detail:  Handle the case when the contact is not found for the given ID.");
                // Handle the case when the contact is not found for the given ID.
                return NotFound();
            }

            var contactModel = _mapper.Map<ContactModel>(contact);
            return PartialView("_Detail", contactModel);
        }


    }
}
